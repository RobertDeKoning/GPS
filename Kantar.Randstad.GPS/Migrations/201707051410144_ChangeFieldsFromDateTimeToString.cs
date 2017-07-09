namespace Kantar.Randstad.GPS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFieldsFromDateTimeToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RandstadEmployees", "DateOfBirth", c => c.String());
            AlterColumn("dbo.RandstadEmployees", "StartDateOfEmployment", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RandstadEmployees", "StartDateOfEmployment", c => c.DateTime());
            AlterColumn("dbo.RandstadEmployees", "DateOfBirth", c => c.DateTime());
        }
    }
}
