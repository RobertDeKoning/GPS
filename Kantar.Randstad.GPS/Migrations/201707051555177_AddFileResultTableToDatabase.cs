namespace Kantar.Randstad.GPS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileResultTableToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false),
                        UserName = c.String(),
                        Country = c.String(),
                        NumSucces = c.String(),
                        NumReject = c.String(),
                        OriginalFile = c.Binary(),
                        ResultFile = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FileResults");
        }
    }
}
