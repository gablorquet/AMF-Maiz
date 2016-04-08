namespace AMF.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CharacterCategories", "Category_Character", "dbo.Characters");
            DropForeignKey("dbo.CharacterCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Events", "Year_Id", "dbo.Years");
            DropIndex("dbo.Events", new[] { "Year_Id" });
            DropIndex("dbo.CharacterCategories", new[] { "Category_Character" });
            DropIndex("dbo.CharacterCategories", new[] { "Category_Id" });
            CreateTable(
                "dbo.Year_Event",
                c => new
                    {
                        Year_Id = c.Int(nullable: false),
                        Event_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Year_Id, t.Event_Id })
                .ForeignKey("dbo.Years", t => t.Year_Id, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_Id, cascadeDelete: true)
                .Index(t => t.Year_Id)
                .Index(t => t.Event_Id);
            
            AddColumn("dbo.Categories", "Character_Id", c => c.Int());
            CreateIndex("dbo.Categories", "Character_Id");
            AddForeignKey("dbo.Categories", "Character_Id", "dbo.Characters", "Id");
            DropColumn("dbo.Events", "Year_Id");
            DropTable("dbo.CharacterCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CharacterCategories",
                c => new
                    {
                        Category_Character = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Character, t.Category_Id });
            
            AddColumn("dbo.Events", "Year_Id", c => c.Int());
            DropForeignKey("dbo.Year_Event", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Year_Event", "Year_Id", "dbo.Years");
            DropForeignKey("dbo.Categories", "Character_Id", "dbo.Characters");
            DropIndex("dbo.Year_Event", new[] { "Event_Id" });
            DropIndex("dbo.Year_Event", new[] { "Year_Id" });
            DropIndex("dbo.Categories", new[] { "Character_Id" });
            DropColumn("dbo.Categories", "Character_Id");
            DropTable("dbo.Year_Event");
            CreateIndex("dbo.CharacterCategories", "Category_Id");
            CreateIndex("dbo.CharacterCategories", "Category_Character");
            CreateIndex("dbo.Events", "Year_Id");
            AddForeignKey("dbo.Events", "Year_Id", "dbo.Years", "Id");
            AddForeignKey("dbo.CharacterCategories", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CharacterCategories", "Category_Character", "dbo.Characters", "Id", cascadeDelete: true);
        }
    }
}
