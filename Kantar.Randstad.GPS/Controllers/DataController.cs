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
                ViewBag.CurrentFilter = email;
                randstadEmployees = randstadEmployees.Where(x => x.EmailAddress.ToLower().Contains(email.ToLower()));
            }
            if (!string.IsNullOrEmpty(year))
            {
                var yearAsInt = int.Parse(year);
                randstadEmployees = randstadEmployees.Where(x => x.SurveyYear.Equals(yearAsInt));
                ViewBag.YearSelected = year;
            }
            if (!string.IsNullOrEmpty(country))
            {
                randstadEmployees = randstadEmployees.Where(x => x.Country.ToLower().Equals(country.ToLower()));
            }
            randstadEmployees = randstadEmployees.OrderBy(x => x.Country);
            if (ViewBag.CountrySelectionList == null)
            {
                List<string> countries = new List<string> { string.Empty };
                countries.AddRange(dbContext.Countries.Select(x => x.CountryName));
                ViewBag.CountrySelectionList = countries;
            }
            if (ViewBag.YearSelectionList == null)
            {
                ViewBag.YearSelectionList = new List<string> { string.Empty, "2016", "2017" };
                ViewBag.YearSelecteded = string.Empty;
            }
            ViewBag.YearSelectionList = ViewBag.YearSelectionList;
            ViewBag.YearSelected = ViewBag.YearSelected;
            ViewBag.CountrySelected = ViewBag.CountrySelected;

            ViewBag.CurrentFilter = ViewBag.CurrentFilter;
            
            return View(randstadEmployees.ToPagedList(page,100));
        }
    }
}