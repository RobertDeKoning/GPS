namespace Kantar.Randstad.GPS.Migrations
{
    using Entities;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RandstadContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RandstadContext context)
        {
            if (!context.Countries.Any())
            {
                List<string> countries = new List<string>
                {
                    "Argentina",
                    "Australia",
                    "Austria",
                    "Belgium",
                    "Brazil",
                    "Canada",
                    "Chile",
                    "China",
                    "Czech Republic",
                    "Denmark",
                    "France",
                    "Germany",
                    "Greece",
                    "Hong Kong",
                    "Hungary",
                    "India",
                    "Italy",
                    "Japan",
                    "Luxembourg",
                    "Malaysia",
                    "México",
                    "Netherlands",
                    "New Zealand",
                    "Norway",
                    "Poland",
                    "Portugal",
                    "Romania",
                    "Singapore",
                    "Spain",
                    "Sweden",
                    "Switzerland",
                    "Turkey",
                    "United Kingdom",
                    "United States"
                };
                foreach (var country in countries)
                {
                    context.Countries.Add(new Country { CountryName = country });
                }
            }
            if (!context.Languages.Any())
            {
                List<string> languages = new List<string>
                {
                    "En",
                    "Nl",
                    "Zh",
                    "De",
                    "Da",
                    "Es",
                    "Fr",
                    "It",
                    "Ja",
                    "Pl",
                    "Sv",
                    "Tr",
                    "Cs",
                    "Nn",
                    "Pt",
                    "Sv"
                };
                foreach (var language in languages)
                {
                    context.Languages.Add(new Language { Lang = language });
                }
            }

            if (!context.YearsOfServiceCategories.Any())
            {
                List<YearsOfServiceCategory> yearsOfServiceCats = new List<YearsOfServiceCategory>
                {
                    new YearsOfServiceCategory { YearsOfServiceCat = "0-1", MinYears = 0 },
                    new YearsOfServiceCategory { YearsOfServiceCat = "1-2", MinYears = 1 },
                    new YearsOfServiceCategory { YearsOfServiceCat = "2-5", MinYears = 2 },
                    new YearsOfServiceCategory { YearsOfServiceCat = "5-10", MinYears = 5 },
                    new YearsOfServiceCategory { YearsOfServiceCat = "10+", MinYears = 10 }
                };
                foreach (var yearsOfServiceCat in yearsOfServiceCats)
                {
                    context.YearsOfServiceCategories.Add(yearsOfServiceCat);
                }
            }

            if (!context.AgeCategories.Any())
            {
                List<AgeCategory> ageCategories = new List<AgeCategory>
                {
                    new AgeCategory { AgeCat = "18-25", MinAge = 18 },
                    new AgeCategory { AgeCat = "25-30", MinAge = 25 },
                    new AgeCategory { AgeCat = "30-35", MinAge = 30 },
                    new AgeCategory { AgeCat = "35-40", MinAge = 35 },
                    new AgeCategory { AgeCat = "40-50", MinAge = 40 },
                    new AgeCategory { AgeCat = "50+", MinAge = 50 }
                };
                foreach (var ageCategory in ageCategories)
                {
                    context.AgeCategories.Add(ageCategory);
                }
            }

            if (!context.FunctionGroups.Any())
            {
                List<FunctionGroup> functionGroups = new List<FunctionGroup>
                {
                    new FunctionGroup { FunctionGroupCat = "10"},
                    new FunctionGroup { FunctionGroupCat = "20"},
                    new FunctionGroup { FunctionGroupCat = "21"},
                    new FunctionGroup { FunctionGroupCat = "22"},
                    new FunctionGroup { FunctionGroupCat = "30"},
                    new FunctionGroup { FunctionGroupCat = "40"},
                };
                foreach (var functionGroup in functionGroups)
                {
                    context.FunctionGroups.Add(functionGroup);
                }
            }

            context.SaveChanges();
        }
    }
}
