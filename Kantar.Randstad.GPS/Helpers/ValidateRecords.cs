using Kantar.Randstad.GPS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Kantar.Randstad.GPS.Helpers
{
    public static class ValidateRecords
    {
        private static string[] Genders = new[] {"M","F"};
        private static List<string> FunctionGroups;
        private static List<string> Countries;
        private static List<string> Languages;
        private static EmailAddressAttribute emailValid = new EmailAddressAttribute();
        private static RegularExpressionAttribute RexExpEmail = new RegularExpressionAttribute(@"^[a-zA-Z\.@]+$");
        private static RegularExpressionAttribute RegExpFuncArea = new RegularExpressionAttribute(@"\A(X\.(0[1-9]|10))$");
        private static RegularExpressionAttribute RegExpNames = new RegularExpressionAttribute("\\D+");

        static ValidateRecords()
        {
            using (var dbContext = new RandstadContext())
            {
                FunctionGroups = dbContext.FunctionGroups.Select(x => x.FunctionGroupCat).ToList();
                Countries = dbContext.Countries.Select(x => x.CountryName).ToList();
                Languages = dbContext.Languages.Select(x => x.Lang).ToList();
            }
        }

        public static void FindInvalidRecords(List<RandstadEmployee> records)
        {
            foreach(var record in records)
            {
                List<string> errors = new List<string>();
                errors.AddEmailErrors(record);
                errors.AddNamesErrors(record);
                errors.AddGenderErrors(record);
                errors.AddDateOfBirthErrors(record);
                errors.AddStartdateOfEmploymentErrors(record);
                errors.AddFunctionGroupErrors(record);
                errors.AddFunctionalAreaErrors(record);
                errors.AddLevel1Errors(record);
                errors.AddLevelSequenceErrors(record);
                errors.AddCountryErrors(record);
                errors.AddNameOfOperatingCountryErrors(record);
                errors.AddLanguageErrors(record);
                if (errors.Any())
                {
                    record.Valid = false;
                    record.Feedback = $"Errors found: {string.Join("; ",errors)}.";
                }
            }
        }

        internal static void GenerateWarningsForChangedRecords(List<RandstadEmployee> employees, RandstadContext dbContext)
        {
            foreach (var employee in employees)
            {
                List<string> warnings = new List<string>();
                try {
                    var employeePreviousYear = dbContext.RandstadEmployees.Find(employee.SurveyYear - 1, employee.EmailAddress);
                    if (employeePreviousYear == null)
                    {
                        warnings.Add("New person found, not in database of last year");
                    }
                    warnings.AddNamesChangedWarnings(employee, employeePreviousYear);
                    warnings.AddGenderChangedWarnings(employee, employeePreviousYear);
                    warnings.AddLevelChangedWarnings(employee, employeePreviousYear);
                    warnings.AddReportingLineWarnings(employees, employee, employeePreviousYear);
                    warnings.AddCountryChangedWarnings(employee, employeePreviousYear);
                    warnings.AddLanguageChangedWarnings(employee, employeePreviousYear);
                }
                catch (Exception exception)
                {
                    warnings.Add($"Error thrown, records could not be resolved. Message: {exception.Message}");
                }
                finally {
                    if (warnings.Any())
                    {
                        employee.Feedback += $"Warnings found: {string.Join("; ", warnings)}.";
                    }
                }
            }
        }



        private static List<string> AddLanguageChangedWarnings(this List<string> warnings, RandstadEmployee employee, RandstadEmployee employeePreviousYear)
        {
            if (!employee.Language.Equals(employeePreviousYear.Language))
            {
                warnings.Add($"Country changed: {employeePreviousYear.SurveyYear} '{employeePreviousYear.Language}', {employee.SurveyYear} '{employee.Language}'");
            }
            return warnings;
        }

        private static List<string> AddCountryChangedWarnings(this List<string> warnings, RandstadEmployee employee, RandstadEmployee employeePreviousYear)
        {
            if (!employee.Country.Equals(employeePreviousYear.Country))
            {
                warnings.Add($"Country changed: {employeePreviousYear.SurveyYear} '{employeePreviousYear.Country}', {employee.SurveyYear} '{employee.Country}'");
            }
            return warnings;
        }

        private static List<string> AddReportingLineWarnings(this List<string> warnings, List<RandstadEmployee> employees, RandstadEmployee employee, RandstadEmployee employeePreviousYear)
        {
            if(!employees.Where(x=>$"{x.FirstName} {x.LastName}".Equals(employee.ReportingLine)).Any() || employees.Where(x => x.EmailAddress.Equals(employee.ReportingLine)).Any())
            {
                warnings.Add($"Reporting line '{employee.ReportingLine}' does not exact match a name or email in the file");
            }
            if (!employee.ReportingLine.Equals(employeePreviousYear.ReportingLine))
            {
                warnings.Add($"Reporting line changed: {employeePreviousYear.SurveyYear} '{employeePreviousYear.ReportingLine}', {employee.SurveyYear} '{employee.ReportingLine}'");
            }
            return warnings;
        }

        private static List<string> AddNamesChangedWarnings(this List<string> warnings, RandstadEmployee employee, RandstadEmployee employeePreviousYear)
        {
            if (!employee.FirstName.Equals(employeePreviousYear.FirstName))
            {
                warnings.Add($"First name changed: {employeePreviousYear.SurveyYear} '{employeePreviousYear.FirstName}', {employee.SurveyYear} '{employee.FirstName}'");
            }
            if (!employee.LastName.Equals(employeePreviousYear.LastName))
            {
                warnings.Add($"Last name changed: {employeePreviousYear.SurveyYear} '{employeePreviousYear.LastName}', {employee.SurveyYear} '{employee.LastName}'");
            }
            return warnings;
        }

        private static List<string> AddGenderChangedWarnings(this List<string> warnings, RandstadEmployee employee, RandstadEmployee employeePreviousYear)
        {
            if (!employee.Gender.Equals(employeePreviousYear.Gender))
            {
                warnings.Add($"Gender changed: {employeePreviousYear.SurveyYear} '{employeePreviousYear.Gender}', {employee.SurveyYear} '{employee.Gender}'");
            }
            return warnings;
        }

        private static List<string> AddLevelChangedWarnings(this List<string> warnings, RandstadEmployee employee, RandstadEmployee employeePreviousYear)
        {
            var fields = employee.GetType().GetProperties().Where(x => x.Name.Contains("Level") && x.Name.Length == 6).Select(x => x.Name).OrderBy(q => q).ToList();
            foreach (var field in fields)
            {
                var employeeLevelValue = employee.GetType().GetProperty(field).GetValue(employee, null) as string;
                var employeePreviousYearLevelValue = employeePreviousYear.GetType().GetProperty(field).GetValue(employeePreviousYear, null) as string;
                if (!employeeLevelValue.Equals(employeePreviousYearLevelValue))
                {
                    warnings.Add($"{field} changed: {employeePreviousYear.SurveyYear} '{employeeLevelValue}', {employee.SurveyYear} '{employeePreviousYearLevelValue}'");
                }
            }
            return warnings;
        }

        private static List<string> AddLevelSequenceErrors(this List<string> errors, RandstadEmployee record)
        {
            var fields = record.GetType().GetProperties().Where(x => x.Name.Contains("Level") && x.Name.Length == 6).Select(x=>x.Name).OrderByDescending(q => q).ToList();
            var expectation = false;
            foreach(var field in fields)
            {
                var propertyValue = record.GetType().GetProperty(field).GetValue(record, null) as string;
                if (expectation && string.IsNullOrEmpty(propertyValue))
                {
                    errors.Add($"{field} empty, while next level is filled");
                    expectation = false;
                }
                else if(!expectation && !string.IsNullOrEmpty(propertyValue))
                {
                    expectation = true;
                }
            }
            return errors;
        }

        private static List<string> AddNameOfOperatingCountryErrors(this List<string> errors, RandstadEmployee record)
        {
            var field = record.NameOfOperatingCompany;
            if (string.IsNullOrEmpty(field))
            {
                errors.Add("Level1 is empty");
            }
            return errors;
        }

        

        private static List<string> AddLevel1Errors(this List<string> errors, RandstadEmployee record)
        {
            var field = record.Level1;
            if (string.IsNullOrEmpty(field))
            {
                errors.Add("Level1 is empty");
            }
            return errors;
        }

        private static List<string> AddNamesErrors(this List<string> errors, RandstadEmployee record)
        {
            var fields = new[] { record.FirstName, record.LastName };
            foreach (var field in fields)
            {
                if (string.IsNullOrEmpty(field))
                {
                    errors.Add("Name is empty");
                }
                else if (!RegExpNames.IsValid(field))
                {
                    errors.Add($"Name '{field}' contains invalid characters");
                }
            }
            return errors;
        }

        private static List<string> AddFunctionalAreaErrors(this List<string> errors, RandstadEmployee record)
        {
            var field = record.FunctionalArea;
            if (string.IsNullOrEmpty(field))
            {
                errors.Add("Functional area is empty");
            }
            else if (!RegExpFuncArea.IsValid(field))
            {
                errors.Add($"Invalid Functional Area '{field}', use X.01 till X.10 only");
            }
            return errors;
        }

        private static List<string> AddStartdateOfEmploymentErrors(this List<string> errors, RandstadEmployee record)
        {
            DateTime dummyDate;
            if (string.IsNullOrEmpty(record.StartDateOfEmployment))
            {
                errors.Add("Start date of employment is empty");
            }
            else if (!DateTime.TryParseExact(record.StartDateOfEmployment, "dd-MM-yyyy", new CultureInfo("nl-NL"), DateTimeStyles.None, out dummyDate))
            {
                errors.Add($"Invalid Start date of employment '{record.StartDateOfEmployment}', use notation 'dd-MM-yyyy' only");
            }
            return errors;
        }

        private static List<string> AddDateOfBirthErrors(this List<string> errors, RandstadEmployee record)
        {
            DateTime dummyDate;
            if (string.IsNullOrEmpty(record.DateOfBirth))
            {
                errors.Add("Date of birth is empty");
            }
            else if (!DateTime.TryParseExact(record.DateOfBirth,"dd-MM-yyyy", new CultureInfo("nl-NL") , DateTimeStyles.None, out dummyDate))
            {
                errors.Add($"Invalid date of birth '{record.DateOfBirth}', use notation 'dd-MM-yyyy' only");
            }
            return errors;
        }

        private static List<string> AddFunctionGroupErrors(this List<string> errors, RandstadEmployee record)
        {
            if (string.IsNullOrEmpty(record.FunctionGroup))
            {
                errors.Add("FunctionGroup is empty");
            }
            else if (!FunctionGroups.Contains(record.FunctionGroup))
            {
                errors.Add($"Invalid function group '{record.FunctionGroup}' found");
            }
            return errors;
        }

        private static List<string> AddGenderErrors(this List<string> errors, RandstadEmployee record)
        {
            if (string.IsNullOrEmpty(record.Gender))
            {
                errors.Add("Gender is empty");
            }
            else if (!Genders.Contains(record.Gender))
            {
                errors.Add($"Invalid gender '{record.Gender}' found");
            }
            return errors;
        }

        private static List<string> AddEmailErrors(this List<string> errors, RandstadEmployee record)
        {
            var field = record.EmailAddress;
            if (string.IsNullOrEmpty(field))
            {
                errors.Add("Email address is empty");
            }
            else if (!emailValid.IsValid(field))
            {
                errors.Add($"Invalid email address '{field}' found");
            }
            else if (!RexExpEmail.IsValid(field))
            {
                errors.Add($"Invalid characters found in email address '{field}'");
            }
            return errors;
        }
        private static List<string> AddCountryErrors(this List<string> errors, RandstadEmployee record)
        {
            var field = record.Country;
            if (string.IsNullOrEmpty(field))
            {
                errors.Add("Country is empty");
            }
            else if (!Countries.Contains(field))
            {
                errors.Add($"Invalid country '{field}' found");
            }
            return errors;
        }
        private static List<string> AddLanguageErrors(this List<string> errors, RandstadEmployee record)
        {
            var field = record.Language;
            if (string.IsNullOrEmpty(field))
            {
                errors.Add("Language is empty");
            }
            else if (!Languages.Contains(field))
            {
                errors.Add($"Invalid language '{field}' found");
            }
            return errors;
        }
        //private static List<string> AddFieldIsEmptyError()
        //{

        //}
    }
}