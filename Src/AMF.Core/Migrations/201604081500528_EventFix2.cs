namespace AMF.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventFix2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Year_Event", "Year_Id", "dbo.Years");
            DropForeignKey("dbo.Year_Event", "Event_Id", "dbo.Events");
            DropIndex("dbo.Year_Event", new[] { "Year_Id" });
            DropIndex("dbo.Year_Event", new[] { "Event_Id" });
            AddColumn("dbo.Events", "Year_Id", c => c.Int());
            CreateIndex("dbo.Events", "Year_Id");
            AddForeignKey("dbo.Events", "Year_Id", "dbo.Years", "Id");
            DropTable("dbo.Year_Event");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Year_Event",
                c => new
                    {
                        Year_Id = c.Int(nullable: false),
                        Event_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Year_Id, t.Event_Id });
            
            DropForeignKey("dbo.Events", "Year_Id", "dbo.Years");
            DropIndex("dbo.Events", new[] { "Year_Id" });
            DropColumn("dbo.Events", "Year_Id");
            CreateIndex("dbo.Year_Event", "Event_Id");
            CreateIndex("dbo.Year_Event", "Year_Id");
            AddForeignKey("dbo.Year_Event", "Event_Id", "dbo.Events", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Year_Event", "Year_Id", "dbo.Years", "Id", cascadeDelete: true);
        }
    }
}
