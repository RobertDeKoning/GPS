using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Globalization;
using Kantar.Randstad.GPS.Entities;

namespace Kantar.Randstad.GPS.Controllers
{
    [Authorize]
    public class DataController : Controller
    {
        private RandstadContext dbContext = new RandstadContext();
        private CultureInfo culture = CultureInfo.CurrentCulture;
        
        // GET: Data
        public ActionResult Index(string year, string country, string email, int page = 1, string sortOrder = "Asc")
        {
            IQueryable<RandstadEmployee> randstadEmployees = dbContext.RandstadEmployees;
            if (!string.IsNullOrEmpty(email))
            {
                page = 1;
                randstadEmployees = randstadEmployees.Where(x => x.EmailAddress.ToLower().Contains(email.ToLower()));
            }
            if (!string.IsNullOrEmpty(year))
            {
                var yearAsInt = int.Parse(year);
                page = 1;
                randstadEmployees = randstadEmployees.Where(x => x.SurveyYear.Equals(yearAsInt));
                ViewBag.YearSelected = year;
            }
            if (!string.IsNullOrEmpty(country))
            {
                page = 1;
                randstadEmployees = randstadEmployees.Where(x => x.Country.ToLower().Equals(country.ToLower()));
            }
            randstadEmployees = randstadEmployees.OrderBy(x => x.Country);
            if (ViewBag.CountrySelect == null)
            {
                List<string> countries = new List<string> { string.Empty };
                countries.AddRange(dbContext.Countries.Select(x => x.CountryName));
                ViewBag.CountrySelect = countries;
            }
            if (ViewBag.YearSelectionList == null)
            {
                ViewBag.YearSelectionList = new List<string> { string.Empty, "2016", "2017" };
                ViewBag.YearSelected = string.Empty;
            }
            ViewBag.YearSelectionList = ViewBag.YearSelectionList;
            ViewBag.YearSelected = ViewBag.YearSelected;
            ViewBag.CountrySelect = ViewBag.CountrySelect;

            ViewBag.CurrentFilter = email;
            
            return View(randstadEmployees.ToPagedList(page,100));
        }
    }
}