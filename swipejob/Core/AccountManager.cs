using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Facebook;
using SwipeJob.Model;
using SwipeJob.Model.ApiRequset;
using SwipeJob.Model.EF;
using SwipeJob.Model.Extra;
using SwipeJob.Utility;
using SwipeJob.Utility.Exceptions;
using System.Net;
using SwipeJob.Data;

namespace SwipeJob.Core
{
    public class AccountManager : BaseManager
    {
        public AccountManager()
        {

        }

        public AccountManager(Guid userId) : base(userId)
        {

        }

        public async Task<User> GetUser(Guid id)
        {
            using (AppDbContext context = new AppDbContext())
            {
                await GetCurrentUser(context);
                User user = await context.Users.Include(x => x.JobSeeker).Include(x => x.Employer).Include(x => x.JobSeeker.Language).FirstOrDefaultAsync(p => p.Id == id);
                return user;
            }
        }
        public CurrentUser GetCurrentUser(Guid id)
        {
            using (var dac = new UserDac())
            {
                //await GetCurrentUser(context);
                if (id == null) {
                    return null;
                }
                dac.Id = id;
                User user = dac.GetUserById();

                //int appliedJob = await context.Applicants.Where(p => p.ApplicantStatus == ApplicantStatus.Applied&& p.JobSeekerId==id).CountAsync();
                //int deletedJob = await context.Applicants.Where(p => p.ApplicantStatus == ApplicantStatus.Deleted && p.JobSeekerId == id).CountAsync();
                //int savedJob = await context.Applicants.Where(p => p.ApplicantStatus == ApplicantStatus.Saved && p.JobSeekerId == id).CountAsync();
                //int totalPostJob = await context.Jobs.Where(p => p.EmployerId == user.Id).CountAsync();
                return new CurrentUser
                {
                    User = user,
                    AppliedJob = 1,
                    DeletedJob = 1,
                    SavedJob = 1,
                    TotalPostJob = 1
                };
            }
        }

        public async Task<List<CompanyHisotry>> GetCompanytHistory(Guid id)
        {
            using (AppDbContext context = new AppDbContext())
            {
                await GetCurrentUser(context);
                List<CompanyHisotry> companyHisotries = await context.CompanyHisotries.Where(x => x.JobSeekerId == id).ToListAsync();
                return companyHisotries;
            }
        }

        public async Task<User> Login(LoginParams loginParams)
        {
            Utils.CheckNullOrEmpty(new List<string> { "Email", "Token" }, loginParams.Email, loginParams.Token);

            using (AppDbContext context = new AppDbContext())
            {
                User user = await context.Users.FirstOrDefaultAsync(p => p.Email == loginParams.Email &&
                                                                         p.AccountType == loginParams.AccountType &&
                                                                         p.UserType == loginParams.UserType);

                if (user == null)
                {
                    throw new UserException(ErrorCode.INVALID.ToString());
                }

                if (!user.IsActivated)
                {
                    throw new UserException(ErrorCode.USER_NOT_VERIFIED_YET.ToString());
                }

                if (loginParams.AccountType == AccountType.Email)
                {
                    if (!UtilsCryptography.VerifyBCryptPassword(loginParams.Token, user.Password))
                    {
                        throw new UserException(ErrorCode.INVALID.ToString());
                    }
                }
                else if (loginParams.AccountType == AccountType.Facebook)
                {
                    try
                    {
                        FacebookClient facebookClient = new FacebookClient(loginParams.Token);
                        await facebookClient.GetTaskAsync("me?fields=id");
                    }
                    catch (Exception)
                    {
                        throw new UserException(ErrorCode.FACEBOOK_INVALID.ToString());
                    }
                }
                else
                {
                    try
                    {
                        string query = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + loginParams.Token;
                        HttpClient client = new HttpClient();
                        await client.GetStringAsync(query);
                    }
                    catch (Exception)
                    {
                        throw new UserException(ErrorCode.GOOGLE_INVALID.ToString());
                    }
                }

                return user;
            }
        }

        public async Task<User> RegisterJobseeker(RegisterJobseekerParams registerJobseekerParams)
        {
            Utils.CheckNullOrEmpty(new List<string> { "Email", "Token", "AccountType" }, registerJobseekerParams.Email, registerJobseekerParams.Token, registerJobseekerParams.AccountType);

            if (!Utils.IsEmail(registerJobseekerParams.Email))
            {
                throw new UserException(ErrorCode.EMAIL_INVALID.ToString());
            }

            using (AppDbContext context = new AppDbContext())
            {
                if (await context.Users.AnyAsync(p => p.Email == registerJobseekerParams.Email))
                {
                    throw new UserException(ErrorCode.EMAIL_IN_USED.ToString());
                }

                byte[] imageBytes = null;
                bool activated = true;
                string confirmationCode = string.Empty;
                if (registerJobseekerParams.AccountType == AccountType.Email)
                {
                    activated = false;
                    confirmationCode = Guid.NewGuid().ToString();
                    registerJobseekerParams.Token = UtilsCryptography.GenerateBCryptHash(registerJobseekerParams.Token);

                }
                else if (registerJobseekerParams.AccountType == AccountType.Facebook)
                {
                    try
                    {
                        FacebookClient facebookClient = new FacebookClient(registerJobseekerParams.Token);
                        await facebookClient.GetTaskAsync("me?fields=id");

                        WebClient webClient = new WebClient();
                        imageBytes = webClient.DownloadData(registerJobseekerParams.AvatarPath);
                        registerJobseekerParams.Token = null;
                    }
                    catch (Exception)
                    {
                        throw new UserException(ErrorCode.FACEBOOK_INVALID.ToString());
                    }

                }
                else
                {
                    try
                    {
                        string query = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + registerJobseekerParams.Token;
                        HttpClient client = new HttpClient();
                        await client.GetStringAsync(query);

                        WebClient webClient = new WebClient();
                        imageBytes = webClient.DownloadData(registerJobseekerParams.AvatarPath);
                        registerJobseekerParams.Token = null;
                    }
                    catch (Exception)
                    {
                        throw new UserException(ErrorCode.GOOGLE_INVALID.ToString());
                    }
                }

                User user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = registerJobseekerParams.Email,
                    Password = registerJobseekerParams.Token,
                    AccountType = registerJobseekerParams.AccountType,
                    UserType = UserType.JobSeeker,
                    RegisteredDateUtc = DateTime.UtcNow,
                    IsActivated = activated,
                    ConfirmationCode = confirmationCode
                };
                context.Users.Add(user);

                Language defaultLanguage = await context.Languages.FirstOrDefaultAsync(p => p.Name == "English - United States");


                if (registerJobseekerParams.AccountType != AccountType.Email)
                {
                    registerJobseekerParams.DayOfBirthUtc = new DateTime(2000, 1, 1);
                    registerJobseekerParams.NRICType = NRICType.Citizen;
                }

                JobSeeker jobSeeker = new JobSeeker
                {
                    UserId = user.Id,
                    Avartar = imageBytes,
                    FullName = registerJobseekerParams.FullName,
                    Gender = Gender.Male,
                    NRICNumber = registerJobseekerParams.NRICNumber,
                    DateOfBirth = registerJobseekerParams.DayOfBirthUtc,
                    NRICType = registerJobseekerParams.NRICType,
                    ExperienceYear = ExperienceYear.Student,
                    LanguageId = defaultLanguage.Id,
                    CanNegotiation = true,
                    CreatedDateUtc = DateTime.UtcNow,
                    UpdatedDateUtc = DateTime.UtcNow
                };
                context.JobSeekers.Add(jobSeeker);

                await context.SaveChangesAsync();

                if (user.IsActivated)
                {
                    await EmailDelivery.SendJobSeekerRegisterCompleted(registerJobseekerParams.Email);
                }
                else
                {
                    await EmailDelivery.SendJobSeekerRegisterActivation(registerJobseekerParams.Email, confirmationCode);
                    return null;
                }

                return user;
            }
        }

        public async Task<bool> RegisterEmployer(RegisterEmployerParam registerEmployerParam)
        {
            if (!Utils.IsEmail(registerEmployerParam.Email))
            {
                throw new UserException(ErrorCode.EMAIL_INVALID.ToString());
            }

            using (AppDbContext context = new AppDbContext())
            {
                if (await context.Users.AnyAsync(p => p.Email == registerEmployerParam.Email))
                {
                    throw new UserException(ErrorCode.EMAIL_IN_USED.ToString());
                }

                Random generator = new Random();
                string password = generator.Next(100000, 999999).ToString();

                User user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = registerEmployerParam.Email,
                    Password = UtilsCryptography.GenerateBCryptHash(password),
                    AccountType = AccountType.Email,
                    UserType = UserType.Employer,
                    RegisteredDateUtc = DateTime.UtcNow,
                    IsActivated = true,
                    ConfirmationCode = ""
                };
                context.Users.Add(user);

                Employer employer = new Employer
                {
                    CompanyName = registerEmployerParam.CompanyName,
                    CompanyRegistrationNumber = registerEmployerParam.CompanyRegistrationNumber,
                    ContactName = registerEmployerParam.ContactName,
                    PhoneNumber = registerEmployerParam.ContactNumber,
                    NatureOfBusiness = registerEmployerParam.NaturalOfBusiness,
                    OverView = registerEmployerParam.Message,
                    CreatedDateUtc = DateTime.UtcNow,
                    UpdatedDateUtc = DateTime.UtcNow,
                    UserId = user.Id,
                };
                context.Employers.Add(employer);

                await context.SaveChangesAsync();
                await EmailDelivery.SendEmployerRegisterCompleted(registerEmployerParam.Email, password);

                return true;
            }
        }

        public async Task<User> ActiveJobSeekerAccount(string code)
        {
            Utils.CheckNullOrEmpty(new List<string> { "Activation Code" }, code);

            using (AppDbContext context = new AppDbContext())
            {
                User user = await context.Users.FirstOrDefaultAsync(p => p.ConfirmationCode == code &&
                                                                         p.IsActivated == false);

                if (user == null)
                {
                    throw new UserException(ErrorCode.INVALID.ToString());
                }

                user.IsActivated = true;
                user.ConfirmationCode = null;
                await context.SaveChangesAsync();

                return user;
            }
        }

        public async Task ForgotPassword(string email)
        {
            Utils.CheckNullOrEmpty(new List<string> { "Email" }, email);

            using (AppDbContext context = new AppDbContext())
            {
                User user = await context.Users.FirstOrDefaultAsync(p => p.Email == email);
                if (user == null)
                {
                    throw new UserException(ErrorCode.EMAIL_INVALID.ToString());
                }

                user.ConfirmationCode = Guid.NewGuid().ToString();
                await context.SaveChangesAsync();
                await EmailDelivery.SendForgotPasswordRequest(email, user.ConfirmationCode);
            }
        }

        public async Task ResetPassword(ResetPasswordParams resetPasswordParams)
        {
            Utils.CheckNullOrEmpty(new List<string> { "ConfirmationCode", "Password" }, resetPasswordParams.ConfirmationCode, resetPasswordParams.Password);
            using (AppDbContext context = new AppDbContext())
            {
                User user = await context.Users.FirstOrDefaultAsync(p => p.ConfirmationCode == resetPasswordParams.ConfirmationCode);
                if (user == null)
                {
                    throw new UserException(ErrorCode.INVALID.ToString());
                }

                user.ConfirmationCode = null;
                user.Password = UtilsCryptography.GenerateBCryptHash(resetPasswordParams.Password);
                await context.SaveChangesAsync();
            }
        }

        public async Task ChangePassword(ChangePasswordParams changePasswordParams)
        {
            Utils.CheckNullOrEmpty(new List<string> { "CurrentPassword", "NewPassword" }, changePasswordParams.CurrentPassword, changePasswordParams.NewPassword);


            using (AppDbContext context = new AppDbContext())
            {
                User user = await context.Users.FirstOrDefaultAsync(p => p.Id == changePasswordParams.UserId);
                if (user == null)
                {
                    throw new UserException(ErrorCode.INVALID.ToString());
                }

                if (!UtilsCryptography.VerifyBCryptPassword(changePasswordParams.CurrentPassword, user.Password))
                {
                    throw new UserException(ErrorCode.CURRENT_PASSWORD_INCORRECT.ToString());
                }

                user.Password = UtilsCryptography.GenerateBCryptHash(changePasswordParams.NewPassword);
                await context.SaveChangesAsync();
            }
        }

        //job seeker profile
        public async Task UpdateJobSeekerProfile(JobSeekerProfileUpdatedPersonalInfoParams param)
        {
            Utils.CheckNullOrEmpty(new List<string> { "DateOfBirth", "Email", "FullName", "Gender", "NRICNumber", "NRICType", "NationalServiceStatus", "UserId", "PostalCode", "PhoneNumber" }, param.DateOfBirth, param.Email, param.FullName, param.Gender, param.NRICNumber, param.NRICType, param.NationalServiceStatus, param.UserId, param.PostalCode, param.PhoneNumber);

            using (AppDbContext context = new AppDbContext())
            {
                User user = await context.Users.FirstOrDefaultAsync(p => p.Id == param.UserId);
                JobSeeker jobSeeker = await context.JobSeekers.FirstOrDefaultAsync(p => p.UserId == param.UserId);

                if (jobSeeker == null || user == null)
                {
                    throw new UserException(ErrorCode.INVALID_SESSION.ToString());
                }

                user.Email = param.Email;
                jobSeeker.FullName = param.FullName;
                jobSeeker.Gender = param.Gender;
                jobSeeker.NationalServiceStatus = param.NationalServiceStatus;
                jobSeeker.DateOfBirth = param.DateOfBirth;
                jobSeeker.NRICNumber = param.NRICNumber;
                jobSeeker.NRICType = param.NRICType;
                jobSeeker.Race = param.Race;
                jobSeeker.Religions = param.Religions;
                jobSeeker.PhoneNumber = param.PhoneNumber;
                jobSeeker.MobileNumber = param.MobileNumber;
                jobSeeker.PostalCode = param.PostalCode;
                jobSeeker.Address = param.Address;
                jobSeeker.UpdatedDateUtc = DateTime.UtcNow;

                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateJobSeekerProfile(JobSeekerProfileUpdatedExperienceLevelParams param)
        {
            Utils.CheckNullOrEmpty(new List<string> { "ExperienceYear", "HighestEducation", "ExpectedPosition", "ExpectedLocation", "ExpectedJobCategory", "ExpectedSalary", "UserId" },
                param.ExperienceYear, param.HighestEducation, param.ExpectedPosition, param.ExpectedLocation, param.ExpectedJobCategory, param.ExpectedSalary, param.UserId);

            using (AppDbContext context = new AppDbContext())
            {
                User user = await context.Users.FirstOrDefaultAsync(p => p.Id == param.UserId);
                JobSeeker jobSeeker = await context.JobSeekers.FirstOrDefaultAsync(p => p.UserId == param.UserId);

                if (jobSeeker == null || user == null)
                {
                    throw new UserException(ErrorCode.INVALID_SESSION.ToString());
                }

                jobSeeker.ExperienceYear = param.ExperienceYear;
                jobSeeker.HighestEducation = param.HighestEducation;
                jobSeeker.ExpectedPosition = param.ExpectedPosition;
                jobSeeker.ExpectedLocation = param.ExpectedLocation;
                jobSeeker.ExpectedJobCategory = param.ExpectedJobCategory;
                jobSeeker.ExpectedSalary = param.ExpectedSalary;
                jobSeeker.CanNegotiation = param.CanNegotiation;
                jobSeeker.LanguageId = param.Language.Id;
                jobSeeker.UpdatedDateUtc = DateTime.UtcNow;

                foreach (CompanyHisotry companyHistory in param.CompanyHistories)
                {
                    var company = await context.CompanyHisotries.FirstOrDefaultAsync(x => x.Id == companyHistory.Id);
                    if (company == null)
                    {
                        var newItem = new CompanyHisotry
                        {
                            JobSeekerId = jobSeeker.UserId,
                            CompanyName = companyHistory.CompanyName,
                            Position = companyHistory.Position,
                            From = companyHistory.From,
                            To = companyHistory.To,
                            CreatedDateUtc = DateTime.UtcNow,
                            UpdatedDateUtc = DateTime.UtcNow
                        };
                        context.CompanyHisotries.Add(newItem);
                    }
                    else
                    {
                        company.CompanyName = companyHistory.CompanyName;
                        company.Position = companyHistory.Position;
                        company.From = companyHistory.From;
                        company.To = companyHistory.To;
                        company.UpdatedDateUtc = DateTime.UtcNow;
                    }
                }
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateJobSeekerProfile(JobSeekerProfileUpdatedInterestInParams param)
        {
            using (AppDbContext context = new AppDbContext())
            {
                JobSeeker jobSeeker = await context.JobSeekers.FirstOrDefaultAsync(p => p.UserId == param.UserId);
                if (jobSeeker == null)
                {
                    throw new UserException(ErrorCode.INVALID_SESSION.ToString());
                }

                jobSeeker.InterestIn = param.InterestIn;
                jobSeeker.UpdatedDateUtc = DateTime.UtcNow;
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateJobSeekerProfile(JobSeekerProfileUpdatedAdditionInfoParam param)
        {
            using (AppDbContext context = new AppDbContext())
            {
                JobSeeker jobSeeker = await context.JobSeekers.FirstOrDefaultAsync(p => p.UserId == param.UserId);
                if (jobSeeker == null)
                {
                    throw new UserException(ErrorCode.INVALID_SESSION.ToString());
                }

                jobSeeker.AdditionalInfo = param.AdditionalInfo;
                jobSeeker.UpdatedDateUtc = DateTime.UtcNow;
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateEmployerProfile(EmployerProfileUpdatedCompanyInfoParams employerProfileUpdatedCompanyInfoParams)
        {
            Utils.CheckNullOrEmpty(new List<string> { "UserId", "CompanyName", "CompanyRegistrationNumber", "Address", "WebLink", "ContactName", "PhoneNumber", "NatureOfBusiness" }, employerProfileUpdatedCompanyInfoParams.UserId, employerProfileUpdatedCompanyInfoParams.CompanyName, employerProfileUpdatedCompanyInfoParams.CompanyRegistrationNumber, employerProfileUpdatedCompanyInfoParams.Address, employerProfileUpdatedCompanyInfoParams.WebLink, employerProfileUpdatedCompanyInfoParams.ContactName, employerProfileUpdatedCompanyInfoParams.PhoneNumber, employerProfileUpdatedCompanyInfoParams.NatureOfBusiness);

            using (AppDbContext context = new AppDbContext())
            {
                User user = await context.Users.FirstOrDefaultAsync(p => p.Id == employerProfileUpdatedCompanyInfoParams.UserId);
                Employer employer = await context.Employers.FirstOrDefaultAsync(p => p.UserId == employerProfileUpdatedCompanyInfoParams.UserId);

                if (employer == null || user == null)
                {
                    throw new UserException(ErrorCode.INVALID_SESSION.ToString());
                }

                employer.CompanyName = employerProfileUpdatedCompanyInfoParams.CompanyName;
                employer.CompanyRegistrationNumber = employerProfileUpdatedCompanyInfoParams.CompanyRegistrationNumber;
                employer.Address = employerProfileUpdatedCompanyInfoParams.Address;
                employer.WebLink = employerProfileUpdatedCompanyInfoParams.WebLink;
                employer.ContactName = employerProfileUpdatedCompanyInfoParams.ContactName;
                employer.PhoneNumber = employerProfileUpdatedCompanyInfoParams.PhoneNumber;
                employer.NatureOfBusiness = employerProfileUpdatedCompanyInfoParams.NatureOfBusiness;

                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateEmployerProfile(EmployerProfileUpdatedOverviewParams employerProfileUpdatedOverviewParams)
        {
            using (AppDbContext context = new AppDbContext())
            {
                Employer employer = await context.Employers.FirstOrDefaultAsync(p => p.UserId == employerProfileUpdatedOverviewParams.UserId);
                if (employer == null)
                {
                    throw new UserException(ErrorCode.INVALID_SESSION.ToString());
                }

                employer.OverView = employerProfileUpdatedOverviewParams.OverView;
                await context.SaveChangesAsync();
            }
        }
    }
}
