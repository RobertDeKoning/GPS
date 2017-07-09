using CsvHelper.Configuration;
using Kantar.Randstad.GPS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kantar.Randstad.GPS.Models
{
    public sealed class RandstadEmployeeMappingToExcel : CsvClassMap<RandstadEmployee>
    {
        public RandstadEmployeeMappingToExcel()
        {
            Map(m => m.EmailAddress);
            Map(m => m.FirstName);
            Map(m => m.LastName);
            Map(m => m.Gender);
            Map(m => m.DateOfBirth);
            Map(m => m.StartDateOfEmployment);
            Map(m => m.FunctionGroup);
            Map(m => m.FunctionalArea);
            Map(m => m.Level1);
            Map(m => m.Level2);
            Map(m => m.Level3);
            Map(m => m.Level4);
            Map(m => m.Level5);
            Map(m => m.Level6);
            Map(m => m.Level7);
            Map(m => m.Level8);
            Map(m => m.Level9);
            Map(m => m.LevelManager);
            Map(m => m.ReportingLine);
            Map(m => m.Country);
            Map(m => m.NameOfOperatingCompany);
            Map(m => m.Language);
            Map(m => m.AdditionalParameterA);
            Map(m => m.AdditionalParameterB);
            Map(m => m.AdditionalParameterC);
            Map(m => m.AdditionalParameterD);
            Map(m => m.AdditionalParameterE);
            Map(m => m.AdditionalParameterF);
            Map(m => m.AdditionalParameterG);
            Map(m => m.Feedback);
        }
    }
}