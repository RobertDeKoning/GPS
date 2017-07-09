using Kantar.Randstad.GPS.Entities;
using System.Data.Entity;

namespace Kantar.Randstad.GPS
{
    public class RandstadContext : DbContext
    {
        public RandstadContext() : base("RandstadDatabase")
        {
            Database.CommandTimeout = 180;
        }

        public DbSet<RandstadEmployee> RandstadEmployees { get; set; }
        public DbSet<FileResult> FileResults { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<AgeCategory> AgeCategories { get; set; }
        public DbSet<YearsOfServiceCategory> YearsOfServiceCategories { get; set; }
        public DbSet<FunctionGroup> FunctionGroups { get; set; }

    }
}