namespace Kantar.Randstad.GPS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRandstadEmployee_RemoveManyValidators : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RandstadEmployees", "FirstName", c => c.String());
            AlterColumn("dbo.RandstadEmployees", "LastName", c => c.String());
            AlterColumn("dbo.RandstadEmployees", "Gender", c => c.String());
            AlterColumn("dbo.RandstadEmployees", "FunctionGroup", c => c.String());
            AlterColumn("dbo.RandstadEmployees", "FunctionalArea", c => c.String());
            AlterColumn("dbo.RandstadEmployees", "Level1", c => c.String());
            AlterColumn("dbo.RandstadEmployees", "LevelManager", c => c.String());
            AlterColumn("dbo.RandstadEmployees", "Country", c => c.String());
            AlterColumn("dbo.RandstadEmployees", "NameOfOperatingCompany", c => c.String());
            AlterColumn("dbo.RandstadEmployees", "Language", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RandstadEmployees", "Language", c => c.String(nullable: false));
            AlterColumn("dbo.RandstadEmployees", "NameOfOperatingCompany", c => c.String(nullable: false));
            AlterColumn("dbo.RandstadEmployees", "Country", c => c.String(nullable: false));
            AlterColumn("dbo.RandstadEmployees", "LevelManager", c => c.String(nullable: false));
            AlterColumn("dbo.RandstadEmployees", "Level1", c => c.String(nullable: false));
            AlterColumn("dbo.RandstadEmployees", "FunctionalArea", c => c.String(nullable: false));
            AlterColumn("dbo.RandstadEmployees", "FunctionGroup", c => c.String(nullable: false));
            AlterColumn("dbo.RandstadEmployees", "Gender", c => c.String(nullable: false));
            AlterColumn("dbo.RandstadEmployees", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.RandstadEmployees", "FirstName", c => c.String(nullable: false));
        }
    }
}
