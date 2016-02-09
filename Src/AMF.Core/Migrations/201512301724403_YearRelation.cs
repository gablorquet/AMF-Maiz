namespace AMF.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YearRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Year_Id", "dbo.Years");
            DropForeignKey("dbo.Races", "Year_Id", "dbo.Years");
            DropIndex("dbo.Categories", new[] { "Year_Id" });
            DropIndex("dbo.Races", new[] { "Year_Id" });
            CreateTable(
                "dbo.year_category",
                c => new
                    {
                        year_id = c.Int(nullable: false),
                        category_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.year_id, t.category_id })
                .ForeignKey("dbo.Years", t => t.year_id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.category_id, cascadeDelete: true)
                .Index(t => t.year_id)
                .Index(t => t.category_id);
            
            CreateTable(
                "dbo.year_race",
                c => new
                    {
                        year_id = c.Int(nullable: false),
                        race_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.year_id, t.race_id })
                .ForeignKey("dbo.Years", t => t.year_id, cascadeDelete: true)
                .ForeignKey("dbo.Races", t => t.race_id, cascadeDelete: true)
                .Index(t => t.year_id)
                .Index(t => t.race_id);
            
            DropColumn("dbo.Categories", "Year_Id");
            DropColumn("dbo.Races", "Year_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Races", "Year_Id", c => c.Int());
            AddColumn("dbo.Categories", "Year_Id", c => c.Int());
            DropForeignKey("dbo.year_race", "race_id", "dbo.Races");
            DropForeignKey("dbo.year_race", "year_id", "dbo.Years");
            DropForeignKey("dbo.year_category", "category_id", "dbo.Categories");
            DropForeignKey("dbo.year_category", "year_id", "dbo.Years");
            DropIndex("dbo.year_race", new[] { "race_id" });
            DropIndex("dbo.year_race", new[] { "year_id" });
            DropIndex("dbo.year_category", new[] { "category_id" });
            DropIndex("dbo.year_category", new[] { "year_id" });
            DropTable("dbo.year_race");
            DropTable("dbo.year_category");
            CreateIndex("dbo.Races", "Year_Id");
            CreateIndex("dbo.Categories", "Year_Id");
            AddForeignKey("dbo.Races", "Year_Id", "dbo.Years", "Id");
            AddForeignKey("dbo.Categories", "Year_Id", "dbo.Years", "Id");
        }
    }
}
