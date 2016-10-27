using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SwipeJob.Model;
using SwipeJob.Model.ApiRequset;
using SwipeJob.Model.EF;
using SwipeJob.Model.Extra;
using SwipeJob.Utility.Exceptions;
using System.Linq;
using SwipeJob.Utility;
using System.Data.Entity;

namespace SwipeJob.Core
{
    public class JobManager : BaseManager
    {
        public JobManager() { }

        public JobManager(Guid userId) : base(userId)
        {
        }

        public async Task CreateNewJob(CreateEditJobParams createEditJobParams)
        {
            Utils.CheckNullOrEmpty(new List<string> { "JobName", "JobType", "JobDescription", "EducationLevel", "FieldOfStudy", "Language", "Certification", "MinimumGrade", "IsStartWorkImmediately", "ExperienceLevel", "StartWorkingAt", "EndWorkingAt", "StartDate", "HoursPerDay", "DayPerWeek", "DayPerMonth", "GenderRequired", "MinSalary", "MaxSalary", "IsSalaryIncludeMealAndBreakTime" }, createEditJobParams.JobName, createEditJobParams.JobType, createEditJobParams.JobDescription, createEditJobParams.Location, createEditJobParams.EducationLevel, createEditJobParams.FieldOfStudy, createEditJobParams.Language, createEditJobParams.Certification, createEditJobParams.MinimumGrade, createEditJobParams.IsStartWorkImmediately, createEditJobParams.ExperienceLevel, createEditJobParams.StartWorkingAt, createEditJobParams.EndWorkingAt, createEditJobParams.StartDate, createEditJobParams.HoursPerDay, createEditJobParams.DayPerWeek, createEditJobParams.DayPerMonth, createEditJobParams.GenderRequired, createEditJobParams.MinSalary, createEditJobParams.MaxSalary, createEditJobParams.IsSalaryIncludeMealAndBreakTime);
            using (AppDbContext context = new AppDbContext())
            {
                User user = await GetCurrentUser(context);

                if (user.UserType == UserType.Employer)
                {
                    Job job = new Job
                    {
                        Id = Guid.NewGuid(),
                        EmployerId = _userId,
                        LanguageId = createEditJobParams.Language.Id,
                        JobName = createEditJobParams.JobName,
                        JobType = createEditJobParams.JobType,
                        MinSalary = createEditJobParams.MinSalary,
                        MaxSalary = createEditJobParams.MaxSalary,
                        ExtraSalary = createEditJobParams.ExtraSalary,
                        StartDate = createEditJobParams.StartDate,
                        EndDate = createEditJobParams.EndDate,
                        JobStartDate = createEditJobParams.JobStartDate,
                        JobDescription = createEditJobParams.JobDescription,
                        IsStartWorkImmediately = createEditJobParams.IsStartWorkImmediately,
                        StartWorkingAt = createEditJobParams.StartWorkingAt,
                        EndWorkingAt = createEditJobParams.EndWorkingAt,
                        AvailableDate = DateTime.UtcNow,
                        HoursPerDay = createEditJobParams.HoursPerDay,
                        DayPerWeek = createEditJobParams.DayPerWeek,
                        DayPerMonth = createEditJobParams.DayPerMonth,
                        GenderRequired = createEditJobParams.GenderRequired,
                        IsSalaryIncludeMealAndBreakTime = createEditJobParams.IsSalaryIncludeMealAndBreakTime,
                        CreatedDateUtc = DateTime.UtcNow,
                        UpdatedDateUtc = DateTime.UtcNow,
                        LocationId = createEditJobParams.Location.Id
                    };
                    context.Jobs.Add(job);

                    Education education = new Education
                    {
                        Id = Guid.NewGuid(),
                        EducationLevel = createEditJobParams.EducationLevel,
                        FieldOfStudyId = createEditJobParams.FieldOfStudy.Id,
                        Certification = createEditJobParams.Certification,
                        MinimumGrade = createEditJobParams.MinimumGrade,
                        ExperienceLevel = createEditJobParams.ExperienceLevel,
                        JobId = job.Id
                    };
                    context.Educations.Add(education);

                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new UserException(ErrorCode.NO_PERMISSION.ToString());
                }
            }
        }

        public async Task UpdateJob(CreateEditJobParams createEditJobParams)
        {
            Utils.CheckNullOrEmpty(new List<string> { "JobId", "JobName", "JobType", "JobDescription", "EducationLevel", "FieldOfStudy", "Language", "Certification", "MinimumGrade", "IsStartWorkImmediately", "ExperienceLevel", "StartWorkingAt", "EndWorkingAt", "StartDate", "HoursPerDay", "DayPerWeek", "DayPerMonth", "GenderRequired", "MinSalary", "MaxSalary", "IsSalaryIncludeMealAndBreakTime" }, createEditJobParams.JobID, createEditJobParams.JobName, createEditJobParams.JobType, createEditJobParams.JobDescription, createEditJobParams.Location, createEditJobParams.EducationLevel, createEditJobParams.FieldOfStudy, createEditJobParams.Language, createEditJobParams.Certification, createEditJobParams.MinimumGrade, createEditJobParams.IsStartWorkImmediately, createEditJobParams.ExperienceLevel, createEditJobParams.StartWorkingAt, createEditJobParams.EndWorkingAt, createEditJobParams.StartDate, createEditJobParams.HoursPerDay, createEditJobParams.DayPerWeek, createEditJobParams.DayPerMonth, createEditJobParams.GenderRequired, createEditJobParams.MinSalary, createEditJobParams.MaxSalary, createEditJobParams.IsSalaryIncludeMealAndBreakTime);

            using (AppDbContext context = new AppDbContext())
            {
                Job job = await context.Jobs.FirstOrDefaultAsync(p => p.Id == createEditJobParams.JobID);
                Education education = await context.Educations.FirstOrDefaultAsync(p => p.JobId == createEditJobParams.JobID);

                if (job == null || education == null)
                {
                    throw new UserException(ErrorCode.INVALID_SESSION.ToString());
                }

                job.LanguageId = createEditJobParams.Language.Id;
                job.JobName = createEditJobParams.JobName;
                job.JobType = createEditJobParams.JobType;
                job.MinSalary = createEditJobParams.MinSalary;
                job.MaxSalary = createEditJobParams.MaxSalary;
                job.ExtraSalary = createEditJobParams.ExtraSalary;
                job.StartDate = createEditJobParams.StartDate;
                job.EndDate = createEditJobParams.EndDate;
                job.JobStartDate = createEditJobParams.JobStartDate;
                job.JobDescription = createEditJobParams.JobDescription;
                job.IsStartWorkImmediately = createEditJobParams.IsStartWorkImmediately;
                job.StartWorkingAt = createEditJobParams.StartWorkingAt;
                job.EndWorkingAt = createEditJobParams.EndWorkingAt;
                job.HoursPerDay = createEditJobParams.HoursPerDay;
                job.DayPerWeek = createEditJobParams.DayPerWeek;
                job.DayPerMonth = createEditJobParams.DayPerMonth;
                job.GenderRequired = createEditJobParams.GenderRequired;
                job.IsSalaryIncludeMealAndBreakTime = createEditJobParams.IsSalaryIncludeMealAndBreakTime;
                job.UpdatedDateUtc = DateTime.UtcNow;
                job.LocationId = createEditJobParams.Location.Id;

                education.EducationLevel = createEditJobParams.EducationLevel;
                education.FieldOfStudyId = createEditJobParams.FieldOfStudy.Id;
                education.Certification = createEditJobParams.Certification;
                education.MinimumGrade = createEditJobParams.MinimumGrade;
                education.ExperienceLevel = createEditJobParams.ExperienceLevel;

                await context.SaveChangesAsync();
            }

        }

        public async Task<Tuple<List<Job>, int, int>> GetAllJobs(GetAllJobsParams getAllJobsParams)
        {
            using (AppDbContext context = new AppDbContext())
            {
                User user = await GetCurrentUser(context);

                if (user.UserType == UserType.Employer)
                {
                    var query = context.Jobs.AsQueryable();
                    query = query.Where(x => x.EmployerId == user.Id);

                    int totalItems = await query.CountAsync();
                    int totalPages = totalItems / getAllJobsParams.PageSize;
                    if (totalItems % getAllJobsParams.PageSize > 0)
                    {
                        totalPages++;
                    }

                    List<Job> jobs = await query.OrderByDescending(p => p.JobName).Skip(getAllJobsParams.PageIndex * getAllJobsParams.PageSize).Take(getAllJobsParams.PageSize).ToListAsync();
                    return new Tuple<List<Job>, int, int>(jobs, totalPages, totalItems);
                }
                else
                {
                    throw new UserException(ErrorCode.NO_PERMISSION.ToString());
                }
            }
        }

        public async Task<List<Job>> GetEmployerLatestJob(Guid id)
        {
            Utils.CheckNullOrEmpty(new List<string> { "id" }, id);
            using (AppDbContext context = new AppDbContext())
            {
                List<Job> jobs = await context.Jobs.Include(p => p.Language).Include(p => p.Location).Include(p => p.Employer).Where(x => x.EmployerId == id).OrderByDescending(p => p.CreatedDateUtc).Take(5).ToListAsync();
                return jobs;
            }
        }

        public async Task<Job> GetJob(Guid jobId)
        {
            Utils.CheckNullOrEmpty(new List<string> { "JobID" }, jobId);

            using (AppDbContext context = new AppDbContext())
            {
                Job job = await context.Jobs.Include(p => p.Language).Include(p => p.Location).Include(p => p.Employer).Where(p => p.Id == jobId).FirstOrDefaultAsync();
                return job;
            }
        }

        public async Task<List<Education>> GetEducation(Guid jobId)
        {
            Utils.CheckNullOrEmpty(new List<string> { "JobID" }, jobId);

            using (AppDbContext context = new AppDbContext())
            {
                List<Education> educations = await context.Educations.Include(x => x.FieldOfStudy).Where(x => x.JobId == jobId).ToListAsync();
                return educations;
            }
        }

        public async Task<Tuple<List<Job>, int, int>> Search(SearchJobParams param)
        {
            using (AppDbContext context = new AppDbContext())
            {
                var query = context.Jobs.Include(x => x.Employer).Include(x => x.Educations).Include(x => x.Location).AsQueryable();

                if (!string.IsNullOrWhiteSpace(param.JobTitle))
                {
                    query = query.Where(x => x.JobName.Contains(param.JobTitle) || x.Employer.CompanyName.Contains(param.JobTitle));
                }

                if (param.Location != null)
                {
                    query = query.Where(x => x.LocationId == param.Location.Id);
                }

                if (param.Category != null)
                {
                    query = query.Where(x => x.Educations.Any(p => p.FieldOfStudyId == param.Category.Id));
                }

                if (param.JobType != 0)
                {
                    query = query.Where(x => x.JobType == param.JobType);
                }

                if (param.MinSalary > 0)
                {
                    query = query.Where(x => x.MinSalary >= param.MinSalary);
                }

                if (param.MaxSalary > 0)
                {
                    query = query.Where(x => x.MaxSalary <= param.MaxSalary);
                }

                int totalItems = await query.CountAsync();
                int totalPages = totalItems / param.PageSize;
                if (totalItems % param.PageSize > 0)
                {
                    totalPages++;
                }

                List<Job> result = await query.OrderByDescending(p => p.JobName).Skip(param.PageIndex * param.PageSize).Take(param.PageSize).ToListAsync();

                return new Tuple<List<Job>, int, int>(result, totalPages, totalItems);
            }
        }

        public async Task<Tuple<List<SearchJobSeekerApplicantRepone>, int, int>> SearchApplicant(SearchJobSeekerApplicantParams param)
        {
            using (AppDbContext context = new AppDbContext())
            {
                await GetCurrentUser(context);
                var query = context.Applicants.Include(x => x.Job).Include(x => x.Job.Employer).Include(x => x.Job.Location).Include(x => x.Job.Educations).Where(x => x.JobSeekerId == _userId && x.ApplicantStatus == param.ApplicantStatus).AsQueryable();

                int totalItems = await query.CountAsync();
                int totalPages = totalItems / param.PageSize;
                if (totalItems % param.PageSize > 0)
                {
                    totalPages++;
                }

                var result = await query.OrderByDescending(p => p.Job.JobName).Skip(param.PageIndex * param.PageSize).Take(param.PageSize).ToListAsync();
                List<SearchJobSeekerApplicantRepone> searchResult = new List<SearchJobSeekerApplicantRepone>();
                searchResult.AddRange(result.Select(x => new SearchJobSeekerApplicantRepone
                {
                    Id = x.Id,
                    JobId = x.JobId,
                    LocationId = x.Job.LocationId,
                    ApplicantStatus = x.ApplicantStatus,
                    CompanyName = x.Job.Employer.CompanyName,
                    Logo = x.Job.Employer.Logo,
                    Location = x.Job.Location,
                    JobType = x.Job.JobType,
                    JobName = x.Job.JobName,
                    MinSalary = x.Job.MinSalary,
                    MaxSalary = x.Job.MaxSalary,
                    EndDate = x.Job.EndDate,
                    StartDate = x.Job.StartDate,
                    UserId = x.JobSeekerId
                }));
                return new Tuple<List<SearchJobSeekerApplicantRepone>, int, int>(searchResult, totalPages, totalItems);
            }
        }

        public async Task UpdateApplicant(UpdateApplicantParam param)
        {
            Utils.CheckNullOrEmpty(new List<string> { "Id" }, param.Id);

            using (AppDbContext context = new AppDbContext())
            {
                await GetCurrentUser(context);
                Applicant applicant = await context.Applicants.FirstOrDefaultAsync(x => x.Id == param.Id);

                applicant.ApplicantStatus = param.ApplicantStatus;
                await context.SaveChangesAsync();
            }
        }

        public async Task<ApplicantStatus> CheckJObApplied(Guid jobId)
        {
            Utils.CheckNullOrEmpty(new List<string> { "Id" }, jobId);
            using (AppDbContext context = new AppDbContext())
            {
                User user = await GetCurrentUser(context);
                if (user == null)
                {
                    throw new UserException(ErrorCode.NO_PERMISSION.ToString());
                }

                Applicant applicant = await context.Applicants.FirstOrDefaultAsync(p => p.JobSeekerId == user.Id && p.JobId == jobId);
                if (applicant == null)
                {
                    return 0;
                }
                return applicant.ApplicantStatus;
            }
    }

    public async Task ApplyJob(Guid jobID)
    {
        Utils.CheckNullOrEmpty(new List<string> { "Id" }, jobID);

        using (AppDbContext context = new AppDbContext())
        {
            User user = await GetCurrentUser(context);

            if (user.UserType == UserType.JobSeeker)
            {
                Applicant applicant = await context.Applicants.FirstOrDefaultAsync(p => p.JobSeekerId == user.Id && p.JobId == jobID);
                Job job = await context.Jobs.Include(p => p.Employer.User).FirstOrDefaultAsync(p => p.Id == jobID);
                JobSeeker jobSeeker = await context.JobSeekers.FirstOrDefaultAsync(p => p.UserId == user.Id);


                if (applicant != null)
                {
                    if (applicant.ApplicantStatus == ApplicantStatus.Applied)
                    {
                        throw new UserException(ErrorCode.JOB_ALREADY_APPLIED.ToString());
                    }
                    else
                    {
                        applicant.ApplicantStatus = ApplicantStatus.Applied;
                        await context.SaveChangesAsync();
                        await EmailDelivery.SendJobSeekerAppliedJob(job.Employer.User.Email, jobSeeker.FullName, job.JobName, user.Id);
                    }
                }
                else
                {
                    Applicant newApplicant = new Applicant
                    {
                        Id = Guid.NewGuid(),
                        JobSeekerId = user.Id,
                        JobId = jobID,
                        ApplicantStatus = ApplicantStatus.Applied,
                        CreatedDateUtc = DateTime.UtcNow,
                        UpdatedDateUtc = DateTime.UtcNow
                    };
                    context.Applicants.Add(newApplicant);
                    await context.SaveChangesAsync();
                    await EmailDelivery.SendJobSeekerAppliedJob(job.Employer.User.Email, jobSeeker.FullName, job.JobName, user.Id);
                }
            }
            else
            {
                throw new UserException(ErrorCode.NO_PERMISSION.ToString());
            }
        }
    }
    public async Task SaveJob(Guid jobID)
    {
        Utils.CheckNullOrEmpty(new List<string> { "Id", "ApplicantStatus" }, jobID);

        using (AppDbContext context = new AppDbContext())
        {
            User user = await GetCurrentUser(context);

            if (user.UserType == UserType.JobSeeker)
            {
                Applicant applicant = await context.Applicants.FirstOrDefaultAsync(p => p.JobSeekerId == user.Id && p.JobId == jobID);

                if (applicant != null)
                {
                    if (applicant.ApplicantStatus == ApplicantStatus.Applied)
                    {
                        throw new UserException(ErrorCode.JOB_ALREADY_APPLIED.ToString());
                    }
                    else if (applicant.ApplicantStatus == ApplicantStatus.Saved)
                    {
                        throw new UserException(ErrorCode.JOB_ALREADY_SAVED.ToString());
                    }
                    else
                    {
                        applicant.ApplicantStatus = ApplicantStatus.Saved;
                        await context.SaveChangesAsync();
                    }
                }
                else
                {
                    Applicant newApplicant = new Applicant
                    {
                        Id = Guid.NewGuid(),
                        JobSeekerId = user.Id,
                        JobId = jobID,
                        ApplicantStatus = ApplicantStatus.Saved,
                        CreatedDateUtc = DateTime.UtcNow,
                        UpdatedDateUtc = DateTime.UtcNow
                    };
                    context.Applicants.Add(newApplicant);

                    await context.SaveChangesAsync();
                }
            }
            else
            {
                throw new UserException(ErrorCode.NO_PERMISSION.ToString());
            }
        }
    }
}
}
