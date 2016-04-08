namespace AMF.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CharacterErrors : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Character_Id", "dbo.Characters");
            DropIndex("dbo.Categories", new[] { "Character_Id" });
            RenameColumn(table: "dbo.CharacterSkills", name: "Character_Id", newName: "Skills_Character");
            RenameIndex(table: "dbo.CharacterSkills", name: "IX_Character_Id", newName: "IX_Skills_Character");
            CreateTable(
                "dbo.CharacterCategories",
                c => new
                    {
                        Category_Character = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Character, t.Category_Id })
                .ForeignKey("dbo.Characters", t => t.Category_Character, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Category_Character)
                .Index(t => t.Category_Id);
            
            AddColumn("dbo.Characters", "Skill_Id", c => c.Int());
            CreateIndex("dbo.Characters", "Skill_Id");
            AddForeignKey("dbo.Characters", "Skill_Id", "dbo.Skills", "Id");
            DropColumn("dbo.Categories", "Character_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Character_Id", c => c.Int());
            DropForeignKey("dbo.Characters", "Skill_Id", "dbo.Skills");
            DropForeignKey("dbo.CharacterCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.CharacterCategories", "Category_Character", "dbo.Characters");
            DropIndex("dbo.CharacterCategories", new[] { "Category_Id" });
            DropIndex("dbo.CharacterCategories", new[] { "Category_Character" });
            DropIndex("dbo.Characters", new[] { "Skill_Id" });
            DropColumn("dbo.Characters", "Skill_Id");
            DropTable("dbo.CharacterCategories");
            RenameIndex(table: "dbo.CharacterSkills", name: "IX_Skills_Character", newName: "IX_Character_Id");
            RenameColumn(table: "dbo.CharacterSkills", name: "Skills_Character", newName: "Character_Id");
            CreateIndex("dbo.Categories", "Character_Id");
            AddForeignKey("dbo.Categories", "Character_Id", "dbo.Characters", "Id");
        }
    }
}
