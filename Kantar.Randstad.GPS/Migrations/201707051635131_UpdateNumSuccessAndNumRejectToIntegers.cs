namespace Kantar.Randstad.GPS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNumSuccessAndNumRejectToIntegers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileResults", "NumSuccess", c => c.Int(nullable: false));
            AlterColumn("dbo.FileResults", "NumReject", c => c.Int(nullable: false));
            DropColumn("dbo.FileResults", "NumSucces");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FileResults", "NumSucces", c => c.String());
            AlterColumn("dbo.FileResults", "NumReject", c => c.String());
            DropColumn("dbo.FileResults", "NumSuccess");
        }
    }
}
