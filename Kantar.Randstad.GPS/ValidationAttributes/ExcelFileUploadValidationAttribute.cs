using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace Kantar.Randstad.GPS.ValidationAttributes
{
    public class ExcelFileUploadValidationAttribute : ValidationAttribute
    {
        private string[] AllowedExtensions { get; set; }

        public ExcelFileUploadValidationAttribute(params string[] fileExtensions)
        {
            AllowedExtensions = fileExtensions.ToArray();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;

            if (file == null)
            {
                return new ValidationResult("No file supplied");
            }
            if (!AllowedExtensions.Any())
            {
                return ValidationResult.Success;
            }
            if (AllowedExtensions.Any(ext => file.FileName.EndsWith(ext, StringComparison.CurrentCultureIgnoreCase)))
            {
                return ValidationResult.Success;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append($"Wrong file type, only submit {AllowedExtensions[0]}");
            for (int i = 1, listLength = AllowedExtensions.Length; i < listLength; i++)
            {
                if (i == listLength - 1)
                {
                    sb.Append($" or {AllowedExtensions[i]}");
                    continue;
                }
                sb.Append($", {AllowedExtensions[i]}");
            }
            sb.Append(".");
            return new ValidationResult(sb.ToString(), new[] { validationContext.MemberName });
        }
    }
}