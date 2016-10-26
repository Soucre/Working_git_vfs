using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SwipeJob.Model.Extra;
using SwipeJob.Utility;

namespace SwipeJob.Model
{
    public class Education
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public EducationLevel EducationLevel { get; set; }

        public string EducationLevelText => EducationLevel != 0 ? EducationLevel.GetDisplayName() : "";

        public Guid FieldOfStudyId { get; set; }

        public Industry FieldOfStudy { get; set; }

        public string Certification { get; set; }

        public string MinimumGrade { get; set; }

        public ExperienceLevel ExperienceLevel { get; set; }

        public string ExperienceLevelText => ExperienceLevel != 0 ? ExperienceLevel.GetDisplayName() : "";

        public Guid JobId { get; set; }

        public Job Job { get; set; }
    }
}
