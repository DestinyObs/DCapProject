using DCaptialProject.ENUMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DCaptialProject.Models
{
    public class ProgramModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "ProgramTitle is required")]
        public string ProgramTitle { get; set; }

        public string Summary { get; set; }

        [Required(ErrorMessage = "ProgramDescription is required")]
        public string ProgramDescription { get; set; }

        public List<Skills> ApplicantSkills { get; set; } = new List<Skills>();

        public List<string> Benefits { get; set; } = new List<string>();

        public List<string> ApplicationCriteria { get; set; } = new List<string>();

        [Required(ErrorMessage = "ProgramType is required")]
        public ProgramType ProgramType { get; set; }

        [Required(ErrorMessage = "ProgramStart is required")]
        public DateTime ProgramStart { get; set; }

        [Required(ErrorMessage = "ApplicationOpen is required")]
        public DateTime ApplicationOpen { get; set; }

        [Required(ErrorMessage = "ApplicationClose is required")]
        public DateTime ApplicationClose { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        public string Duration { get; set; }

        public List<ProgramLocation> ProgramLocations { get; set; } = new List<ProgramLocation>();

        [Required(ErrorMessage = "FullyRemote is required")]
        public bool FullyRemote { get; set; }

        [Required(ErrorMessage = "MinQualifications is required")]
        public MinQualification MinQualifications { get; set; }

        [Required(ErrorMessage = "MaxApplications is required")]
        public int MaxApplications { get; set; }
    }

    public class ProgramLocation
    {
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
    }

}
