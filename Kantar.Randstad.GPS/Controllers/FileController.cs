using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kantar.Randstad.GPS.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private RandstadContext dbContext = new RandstadContext();
        // GET: File
        public ActionResult Index()
        {
            var fileResults = dbContext.FileResults;
            return View(fileResults);
        }

        public ActionResult Details(int id)
        {
            var fileResult = dbContext.FileResults.Find(id);
            return View(fileResult);
        }
        public ActionResult GetFile(int id, string fileType)
        {
            var fileResult = dbContext.FileResults.Find(id);
            return new FileStreamResult(new MemoryStream(fileResult.GetType().GetProperty(fileType).GetValue(fileResult, null) as byte[]),
                "application/vnd.ms-excel")
            {
                FileDownloadName = $"{fileType}_{fileResult.FileName}"
            };
        }
    }
}