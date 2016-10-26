using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SwipeJob.Core;
using SwipeJob.Model;
using SwipeJob.Model.Extra;
using SwipeJob.Utility;

namespace SwipeJob.Web.ApiControllers
{
    [RoutePrefix("api/common")]
    public class ApiCommonController : ApiBaseController
    {
        [Route("job-seeker/profile-photo")]
        [HttpPost]
        public async Task<ApiJsonResult> UploadJobSeekerProfilePhoto()
        {
            try
            {
                Guid userId = GetCurrentUserId();
                MultipartMemoryStreamProvider provider = await Request.Content.ReadAsMultipartAsync();
                if (provider.Contents.Count >= 1)
                {
                    var stream = provider.Contents[0];
                    var fileBytes = await stream.ReadAsByteArrayAsync();
                    await new CommonManager(userId).UpdateJobSeekerProfilePhoto(fileBytes);
                    return new ApiJsonResult { Success = true, Data = fileBytes };
                }
                return new ApiJsonResult { Success = true, Data = "FILE_NOT_EXISTED" };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("job-seeker/upload-more-document")]
        [HttpPost]
        public async Task<ApiJsonResult> UploadMoreDocument()
        {
            try
            {
                Guid userId = GetCurrentUserId();
                MultipartMemoryStreamProvider provider = await Request.Content.ReadAsMultipartAsync();
                if (provider.Contents.Count >= 1)
                {
                    var stream = provider.Contents[0];
                    var fileBytes = await stream.ReadAsByteArrayAsync();
                    await new CommonManager(userId).UpdateJobSeekerMoreDoc(fileBytes,stream.Headers.ContentType.ToString());
                    return new ApiJsonResult { Success = true, Data = fileBytes };
                }
                return new ApiJsonResult { Success = true, Data = "FILE_NOT_EXISTED" };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("employer/logo-photo")]
        [HttpPost]
        public async Task<ApiJsonResult> UploadEmployerLogoPhoto()
        {
            try
            {
                Guid userId = GetCurrentUserId();
                MultipartMemoryStreamProvider provider = await Request.Content.ReadAsMultipartAsync();
                if (provider.Contents.Count >= 1)
                {
                    var stream = provider.Contents[0];
                    var fileBytes = await stream.ReadAsByteArrayAsync();
                    await new CommonManager(userId).UpdateEmployerLogoPhoto(fileBytes);
                    return new ApiJsonResult { Success = true, Data = fileBytes };
                }
                return new ApiJsonResult { Success = true, Data = "FILE_NOT_EXISTED" };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("experience-level")]
        [HttpGet]
        public async Task<ApiJsonResult> ExperienceLevel()
        {
            return new ApiJsonResult { Success = true, Data = EnumExtensions.GetObjectList<ExperienceLevel>() };
        }

        [Route("education-level")]
        [HttpGet]
        public async Task<ApiJsonResult> EducationLevel()
        {
            return new ApiJsonResult { Success = true, Data = EnumExtensions.GetObjectList<EducationLevel>() };
        }

        [Route("gender")]
        [HttpGet]
        public async Task<ApiJsonResult> Gender()
        {
            return new ApiJsonResult { Success = true, Data = EnumExtensions.GetObjectList<Gender>() };
        }

        [Route("gender-required")]
        [HttpGet]
        public async Task<ApiJsonResult> GenderRequired()
        {
            return new ApiJsonResult { Success = true, Data = EnumExtensions.GetObjectList<GenderRequired>() };
        }

        [Route("applicant-status")]
        [HttpGet]
        public async Task<ApiJsonResult> ApplicantStatus()
        {
            return new ApiJsonResult { Success = true, Data = EnumExtensions.GetObjectList<ApplicantStatus>() };
        }

        [Route("experience-years")]
        [HttpGet]
        public async Task<ApiJsonResult> ExperienceYear()
        {
            return new ApiJsonResult { Success = true, Data = EnumExtensions.GetObjectList<ExperienceYear>() };
        }

        [Route("frequency")]
        [HttpGet]
        public async Task<ApiJsonResult> Frequency()
        {
            return new ApiJsonResult { Success = true, Data = EnumExtensions.GetObjectList<Frequency>() };
        }

        [Route("job-type")]
        [HttpGet]
        public async Task<ApiJsonResult> JobType()
        {
            return new ApiJsonResult { Success = true, Data = EnumExtensions.GetObjectList<JobType>() };
        }

        [Route("nric-type")]
        [HttpGet]
        public async Task<ApiJsonResult> NRICType()
        {
            return new ApiJsonResult { Success = true, Data = EnumExtensions.GetObjectList<NRICType>() };
        }

        [Route("nationnal-service-status")]
        [HttpGet]
        public async Task<ApiJsonResult> NationnalServiceStatus()
        {
            return new ApiJsonResult { Success = true, Data = EnumExtensions.GetObjectList<NationnalServiceStatus>() };
        }

        [Route("industries")]
        [HttpGet]
        public async Task<ApiJsonResult> GetAllIndustries()
        {
            try
            {
                List<Industry> industries = await new CommonManager().GetAllIndustry();
                return new ApiJsonResult { Success = true, Data = industries };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("locations")]
        [HttpGet]
        public async Task<ApiJsonResult> GetAllLocation()
        {
            try
            {
                List<Location> locations = await new CommonManager().GetAllLocation();
                return new ApiJsonResult { Success = true, Data = locations };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("languages")]
        [HttpGet]
        public async Task<ApiJsonResult> GetAllLanguages()
        {
            try
            {
                List<Language> languages = await new CommonManager().GetAllLanguages();
                return new ApiJsonResult { Success = true, Data = languages };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("language/{id}")]
        [HttpGet]
        public async Task<ApiJsonResult> GetLanguageById(Guid id)
        {
            try
            {
              Language language = await new CommonManager().GetLaguageById(id);
                return new ApiJsonResult { Success = true, Data = language };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }
    }
}
