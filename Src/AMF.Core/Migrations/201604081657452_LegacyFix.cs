namespace AMF.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LegacyFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LegacyTrees", "Legacy_Id", "dbo.Legacies");
            DropForeignKey("dbo.LegacySkills", "Legacy_Id", "dbo.Legacies");
            DropIndex("dbo.LegacyTrees", new[] { "Legacy_Id" });
            DropIndex("dbo.LegacySkills", new[] { "Legacy_Id" });
            CreateTable(
                "dbo.Legacy_Legacy_Tree",
                c => new
                    {
                        Legacy_Id = c.Int(nullable: false),
                        LegacyTree_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Legacy_Id, t.LegacyTree_Id })
                .ForeignKey("dbo.Legacies", t => t.Legacy_Id, cascadeDelete: true)
                .ForeignKey("dbo.LegacyTrees", t => t.LegacyTree_Id, cascadeDelete: true)
                .Index(t => t.Legacy_Id)
                .Index(t => t.LegacyTree_Id);
            
            CreateTable(
                "dbo.Legacy_Legacy_Skill",
                c => new
                    {
                        Legacy_Id = c.Int(nullable: false),
                        LegacySkill_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Legacy_Id, t.LegacySkill_Id })
                .ForeignKey("dbo.Legacies", t => t.Legacy_Id, cascadeDelete: true)
                .ForeignKey("dbo.LegacySkills", t => t.LegacySkill_Id, cascadeDelete: true)
                .Index(t => t.Legacy_Id)
                .Index(t => t.LegacySkill_Id);
            
            DropColumn("dbo.LegacyTrees", "Legacy_Id");
            DropColumn("dbo.LegacySkills", "Legacy_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LegacySkills", "Legacy_Id", c => c.Int());
            AddColumn("dbo.LegacyTrees", "Legacy_Id", c => c.Int());
            DropForeignKey("dbo.Legacy_Legacy_Skill", "LegacySkill_Id", "dbo.LegacySkills");
            DropForeignKey("dbo.Legacy_Legacy_Skill", "Legacy_Id", "dbo.Legacies");
            DropForeignKey("dbo.Legacy_Legacy_Tree", "LegacyTree_Id", "dbo.LegacyTrees");
            DropForeignKey("dbo.Legacy_Legacy_Tree", "Legacy_Id", "dbo.Legacies");
            DropIndex("dbo.Legacy_Legacy_Skill", new[] { "LegacySkill_Id" });
            DropIndex("dbo.Legacy_Legacy_Skill", new[] { "Legacy_Id" });
            DropIndex("dbo.Legacy_Legacy_Tree", new[] { "LegacyTree_Id" });
            DropIndex("dbo.Legacy_Legacy_Tree", new[] { "Legacy_Id" });
            DropTable("dbo.Legacy_Legacy_Skill");
            DropTable("dbo.Legacy_Legacy_Tree");
            CreateIndex("dbo.LegacySkills", "Legacy_Id");
            CreateIndex("dbo.LegacyTrees", "Legacy_Id");
            AddForeignKey("dbo.LegacySkills", "Legacy_Id", "dbo.Legacies", "Id");
            AddForeignKey("dbo.LegacyTrees", "Legacy_Id", "dbo.Legacies", "Id");
        }
    }
}
