using Kantar.Randstad.GPS.Entities;
using Kantar.Randstad.GPS.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kantar.Randstad.GPS.Models
{
    public class ExcelFileUploadModel
    {
        [ExcelFileUploadValidation(new[] { "xls", "xlsx" })]
        public HttpPostedFileBase ExcelFile { get; set; }
        [ScaffoldColumn(false)]
        public string FileName { get { return ExcelFile.FileName; } private set { value = ExcelFile.FileName; } }
        [Required]
        public string Country { get; set; }
        [ScaffoldColumn(false)]
        public IEnumerable<string> Countries { get; set; }
        [Required]
        public int Year { get; set; }
        [ScaffoldColumn(false)]
        public IEnumerable<int> Years { get; set; }
    }
}