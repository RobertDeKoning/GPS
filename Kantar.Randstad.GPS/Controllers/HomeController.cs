using CsvHelper;
using CsvHelper.Excel;
using Kantar.Randstad.GPS.Entities;
using Kantar.Randstad.GPS.Helpers;
using Kantar.Randstad.GPS.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kantar.Randstad.GPS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private RandstadContext dbContext = new RandstadContext();
        private string downloadpath = @"C:\Users\robert.dekoning\Documents\visual studio 2015\Projects\NewRandstad\Downloads\";
        private string resultspath = @"C:\Users\robert.dekoning\Documents\visual studio 2015\Projects\NewRandstad\Downloads\";
        [HttpGet]
        public ActionResult Index()
        {
            var model = new ExcelFileUploadModel
            {
                Years = new List<int> { 2017, 2016 },
                Countries = dbContext.Countries.Select(x => x.CountryName).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ExcelFileUploadModel file)
        {
            var fileResult = new Entities.FileResult();
            if (ModelState.IsValid)
            {
                string fileName = file.FileName;
                var fileDownloadLocation = $"{downloadpath}{fileName}";
                var fileResultLocation = $"{resultspath}{fileName}";
                file.ExcelFile.SaveAs(fileDownloadLocation);
                fileResult.UserName = User.Identity.Name;
                fileResult.Country = file.Country;
                fileResult.FileName = fileName;

                using (var csvReader = new CsvReader(new ExcelParser(fileDownloadLocation)))
                {
                    using (var writer = new CsvWriter(new ExcelSerializer(fileResultLocation)))
                    {
                        csvReader.Configuration.RegisterClassMap<ExcelDataMappingToEmployee>();
                        csvReader.Configuration.HasHeaderRecord = true;
                        csvReader.Configuration.IgnoreHeaderWhiteSpace = true;
                        csvReader.Configuration.IsHeaderCaseSensitive = false;
                        csvReader.Configuration.WillThrowOnMissingField = false;
                        csvReader.Configuration.ReadingExceptionCallback = (exception, row) => { Debug.WriteLine($"Error in row {row.Row}: {exception.Message}"); };

                        var records = csvReader.GetRecords<RandstadEmployee>().ToList();
                        foreach (var record in records)
                        {
                            record.FileName = file.FileName;
                            record.SurveyYear = file.Year;
                            record.UploadedBy = User.Identity.Name;
                        }

                        if (file.Year != 2016)
                        {
                            var removeList = dbContext.RandstadEmployees.Where(r => r.Country == file.Country && r.SurveyYear == file.Year).ToList();
                            dbContext.RandstadEmployees.RemoveRange(removeList);
                            ValidateRecords.FindInvalidRecords(records);
                            //ValidateRecords.GenerateWarningsForChangedRecords(records);
                            dbContext.RandstadEmployees.AddRange(records.Where(r => r.Valid));

                        }
                        else
                        {
                            var removeList = dbContext.RandstadEmployees.Where(r => r.SurveyYear == file.Year).ToList();
                            dbContext.RandstadEmployees.RemoveRange(removeList);
                            dbContext.RandstadEmployees.AddRange(records.Where(r => r.Valid));
                        }

                        fileResult.NumSuccess = records.Where(r => r.Valid).Count();
                        fileResult.NumReject = records.Count() - fileResult.NumSuccess;

                        writer.Configuration.HasHeaderRecord = true;
                        writer.Configuration.RegisterClassMap<RandstadEmployeeMappingToExcel>();
                        writer.WriteHeader<RandstadExcelHeader>();
                        writer.WriteRecords(records);
                    }
                    fileResult.OriginalFile = System.IO.File.ReadAllBytes(fileDownloadLocation);
                    fileResult.ResultFile = System.IO.File.ReadAllBytes(fileResultLocation);
                    fileResult.CreatedDateTime = DateTime.Now;
                    dbContext.FileResults.Add(fileResult);
                }
                dbContext.SaveChanges();
                return RedirectToAction("Details", "File", new { id = fileResult.Id });
            }
            var model = new ExcelFileUploadModel
            {
                Years = new List<int> { 2016, 2017 },
                Countries = dbContext.Countries.Select(x => x.CountryName).ToList()
            };
            return View(model);
        }
    }
}