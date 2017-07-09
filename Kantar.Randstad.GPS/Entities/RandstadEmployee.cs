using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kantar.Randstad.GPS.Entities
{
    public class RandstadEmployee
    {
        [Key, Column(Order = 0)]
        public int SurveyYear { get; set; }
        [Key, Column(Order = 1)]
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string DateOfBirth { get; set; }
        public string YearsOfService { get; set; }
        public string StartDateOfEmployment { get; set; }
        public string FunctionGroup { get; set; }
        public string FunctionalArea { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string Level4 { get; set; }
        public string Level5 { get; set; }
        public string Level6 { get; set; }
        public string Level7 { get; set; }
        public string Level8 { get; set; }
        public string Level9 { get; set; }
        public string LevelManager { get; set; }
        public string ReportingLine { get; set; }
        public string Country { get; set; }
        public string NameOfOperatingCompany { get; set; }
        public string AdditionalParameterA { get; set; }
        public string AdditionalParameterB { get; set; }
        public string AdditionalParameterC { get; set; }
        public string AdditionalParameterD { get; set; }
        public string AdditionalParameterE { get; set; }
        public string AdditionalParameterF { get; set; }
        public string AdditionalParameterG { get; set; }
        public string Language { get; set; }
        [NotMapped]
        public string Feedback { get; set; } = "Correct";
        [NotMapped]
        public bool Valid { get; set; } = true;
        public DateTime CreatedDateTimeUtc { get { return DateTime.Now; } private set { } }
        public string UploadedBy { get; set; }
        public string FileName { get; set; }
    }
}