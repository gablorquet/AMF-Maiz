namespace AMF.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CharacterErrors2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Characters", "Skill_Id", "dbo.Skills");
            DropIndex("dbo.Characters", new[] { "Skill_Id" });
            RenameColumn(table: "dbo.CharacterSkills", name: "Skills_Character", newName: "Character_Id");
            RenameIndex(table: "dbo.CharacterSkills", name: "IX_Skills_Character", newName: "IX_Character_Id");
            DropColumn("dbo.Characters", "Skill_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Characters", "Skill_Id", c => c.Int());
            RenameIndex(table: "dbo.CharacterSkills", name: "IX_Character_Id", newName: "IX_Skills_Character");
            RenameColumn(table: "dbo.CharacterSkills", name: "Character_Id", newName: "Skills_Character");
            CreateIndex("dbo.Characters", "Skill_Id");
            AddForeignKey("dbo.Characters", "Skill_Id", "dbo.Skills", "Id");
        }
    }
}
