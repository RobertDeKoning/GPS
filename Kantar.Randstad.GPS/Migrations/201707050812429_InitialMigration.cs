namespace Kantar.Randstad.GPS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgeCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MinAge = c.Int(nullable: false),
                        AgeCat = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FunctionGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FunctionGroupCat = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Lang = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RandstadEmployees",
                c => new
                    {
                        SurveyYear = c.Int(nullable: false),
                        EmailAddress = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        Age = c.String(),
                        DateOfBirth = c.DateTime(),
                        YearsOfService = c.String(),
                        StartDateOfEmployment = c.DateTime(),
                        FunctionGroup = c.String(nullable: false),
                        FunctionalArea = c.String(nullable: false),
                        Level1 = c.String(nullable: false),
                        Level2 = c.String(),
                        Level3 = c.String(),
                        Level4 = c.String(),
                        Level5 = c.String(),
                        Level6 = c.String(),
                        Level7 = c.String(),
                        Level8 = c.String(),
                        Level9 = c.String(),
                        LevelManager = c.String(nullable: false),
                        ReportingLine = c.String(),
                        Country = c.String(nullable: false),
                        NameOfOperatingCompany = c.String(nullable: false),
                        AdditionalParameterA = c.String(),
                        AdditionalParameterB = c.String(),
                        AdditionalParameterC = c.String(),
                        AdditionalParameterD = c.String(),
                        AdditionalParameterE = c.String(),
                        AdditionalParameterF = c.String(),
                        AdditionalParameterG = c.String(),
                        Language = c.String(nullable: false),
                        CreatedDateTimeUtc = c.DateTime(nullable: false),
                        UploadedBy = c.String(),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => new { t.SurveyYear, t.EmailAddress });
            
            CreateTable(
                "dbo.YearsOfServiceCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MinYears = c.Int(nullable: false),
                        YearsOfServiceCat = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.YearsOfServiceCategories");
            DropTable("dbo.RandstadEmployees");
            DropTable("dbo.Languages");
            DropTable("dbo.FunctionGroups");
            DropTable("dbo.Countries");
            DropTable("dbo.AgeCategories");
        }
    }
}
