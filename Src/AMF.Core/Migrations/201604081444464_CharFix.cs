namespace AMF.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CharFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Character_Id", "dbo.Characters");
            DropIndex("dbo.Categories", new[] { "Character_Id" });
            CreateTable(
                "dbo.Category_Character",
                c => new
                    {
                        Character_Id = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Character_Id, t.Category_Id })
                .ForeignKey("dbo.Characters", t => t.Character_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Character_Id)
                .Index(t => t.Category_Id);
            
            DropColumn("dbo.Categories", "Character_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Character_Id", c => c.Int());
            DropForeignKey("dbo.Category_Character", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Category_Character", "Character_Id", "dbo.Characters");
            DropIndex("dbo.Category_Character", new[] { "Category_Id" });
            DropIndex("dbo.Category_Character", new[] { "Character_Id" });
            DropTable("dbo.Category_Character");
            CreateIndex("dbo.Categories", "Character_Id");
            AddForeignKey("dbo.Categories", "Character_Id", "dbo.Characters", "Id");
        }
    }
}
