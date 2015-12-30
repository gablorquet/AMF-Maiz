namespace AMF.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Username = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Archived = c.DateTime(),
                        DateOfBirth = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsMastery = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Archived = c.DateTime(),
                        Character_Id = c.Int(),
                        Year_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Characters", t => t.Character_Id)
                .ForeignKey("dbo.Years", t => t.Year_Id)
                .Index(t => t.Character_Id)
                .Index(t => t.Year_Id);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Bonus = c.Int(nullable: false),
                        IsLegacy = c.Boolean(nullable: false),
                        IsPassive = c.Boolean(nullable: false),
                        ArmorRestricted = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Archived = c.DateTime(),
                        Category_Id = c.Int(),
                        Race_Id = c.Int(),
                        Character_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Races", t => t.Race_Id)
                .ForeignKey("dbo.Characters", t => t.Character_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Race_Id)
                .Index(t => t.Character_Id);
            
            CreateTable(
                "dbo.Spells",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsMinor = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Archived = c.DateTime(),
                        Skill_Id = c.Int(),
                        Character_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Skills", t => t.Skill_Id)
                .ForeignKey("dbo.Characters", t => t.Character_Id)
                .Index(t => t.Skill_Id)
                .Index(t => t.Character_Id);
            
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Experience = c.Int(nullable: false),
                        Title = c.String(),
                        Influence = c.Int(nullable: false),
                        PresetRessource = c.Int(),
                        CreationDate = c.DateTime(nullable: false),
                        Archived = c.DateTime(),
                        Legacy_Id = c.Int(),
                        Player_Id = c.Int(),
                        Race_Id = c.Int(),
                        Year_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Legacies", t => t.Legacy_Id)
                .ForeignKey("dbo.Users", t => t.Player_Id)
                .ForeignKey("dbo.Races", t => t.Race_Id)
                .ForeignKey("dbo.Years", t => t.Year_Id)
                .Index(t => t.Legacy_Id)
                .Index(t => t.Player_Id)
                .Index(t => t.Race_Id)
                .Index(t => t.Year_Id);
            
            CreateTable(
                "dbo.Legacies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        Archived = c.DateTime(),
                        Scenario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Scenarios", t => t.Scenario_Id)
                .Index(t => t.Scenario_Id);
            
            CreateTable(
                "dbo.LegacyTrees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        Archived = c.DateTime(),
                        Legacy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Legacies", t => t.Legacy_Id)
                .Index(t => t.Legacy_Id);
            
            CreateTable(
                "dbo.LegacySkills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        Archived = c.DateTime(),
                        LegacyTree_Id = c.Int(),
                        Legacy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LegacyTrees", t => t.LegacyTree_Id)
                .ForeignKey("dbo.Legacies", t => t.Legacy_Id)
                .Index(t => t.LegacyTree_Id)
                .Index(t => t.Legacy_Id);
            
            CreateTable(
                "dbo.Scenarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        Archived = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Years",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Current = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Archived = c.DateTime(),
                        Scenario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Scenarios", t => t.Scenario_Id)
                .Index(t => t.Scenario_Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        EventNumber = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Archived = c.DateTime(),
                        Year_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Years", t => t.Year_Id)
                .Index(t => t.Year_Id);
            
            CreateTable(
                "dbo.Races",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Archived = c.DateTime(),
                        Year_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Years", t => t.Year_Id)
                .Index(t => t.Year_Id);
            
            CreateTable(
                "dbo.Throphies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RewardSelected = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Archived = c.DateTime(),
                        Recipient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Characters", t => t.Recipient_Id)
                .Index(t => t.Recipient_Id);
            
            CreateTable(
                "dbo.skill_prerequisites",
                c => new
                    {
                        parent_id = c.Int(nullable: false),
                        child_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.parent_id, t.child_id })
                .ForeignKey("dbo.Skills", t => t.parent_id)
                .ForeignKey("dbo.Skills", t => t.child_id)
                .Index(t => t.parent_id)
                .Index(t => t.child_id);
            
            CreateTable(
                "dbo.EventCharacters",
                c => new
                    {
                        Event_Id = c.Int(nullable: false),
                        Character_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Event_Id, t.Character_Id })
                .ForeignKey("dbo.Events", t => t.Event_Id, cascadeDelete: true)
                .ForeignKey("dbo.Characters", t => t.Character_Id, cascadeDelete: true)
                .Index(t => t.Event_Id)
                .Index(t => t.Character_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Characters", "Year_Id", "dbo.Years");
            DropForeignKey("dbo.Throphies", "Recipient_Id", "dbo.Characters");
            DropForeignKey("dbo.Spells", "Character_Id", "dbo.Characters");
            DropForeignKey("dbo.Skills", "Character_Id", "dbo.Characters");
            DropForeignKey("dbo.Characters", "Race_Id", "dbo.Races");
            DropForeignKey("dbo.Characters", "Player_Id", "dbo.Users");
            DropForeignKey("dbo.Characters", "Legacy_Id", "dbo.Legacies");
            DropForeignKey("dbo.Legacies", "Scenario_Id", "dbo.Scenarios");
            DropForeignKey("dbo.Years", "Scenario_Id", "dbo.Scenarios");
            DropForeignKey("dbo.Races", "Year_Id", "dbo.Years");
            DropForeignKey("dbo.Skills", "Race_Id", "dbo.Races");
            DropForeignKey("dbo.Categories", "Year_Id", "dbo.Years");
            DropForeignKey("dbo.Events", "Year_Id", "dbo.Years");
            DropForeignKey("dbo.EventCharacters", "Character_Id", "dbo.Characters");
            DropForeignKey("dbo.EventCharacters", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.LegacySkills", "Legacy_Id", "dbo.Legacies");
            DropForeignKey("dbo.LegacyTrees", "Legacy_Id", "dbo.Legacies");
            DropForeignKey("dbo.LegacySkills", "LegacyTree_Id", "dbo.LegacyTrees");
            DropForeignKey("dbo.Categories", "Character_Id", "dbo.Characters");
            DropForeignKey("dbo.Skills", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Spells", "Skill_Id", "dbo.Skills");
            DropForeignKey("dbo.skill_prerequisites", "child_id", "dbo.Skills");
            DropForeignKey("dbo.skill_prerequisites", "parent_id", "dbo.Skills");
            DropIndex("dbo.EventCharacters", new[] { "Character_Id" });
            DropIndex("dbo.EventCharacters", new[] { "Event_Id" });
            DropIndex("dbo.skill_prerequisites", new[] { "child_id" });
            DropIndex("dbo.skill_prerequisites", new[] { "parent_id" });
            DropIndex("dbo.Throphies", new[] { "Recipient_Id" });
            DropIndex("dbo.Races", new[] { "Year_Id" });
            DropIndex("dbo.Events", new[] { "Year_Id" });
            DropIndex("dbo.Years", new[] { "Scenario_Id" });
            DropIndex("dbo.LegacySkills", new[] { "Legacy_Id" });
            DropIndex("dbo.LegacySkills", new[] { "LegacyTree_Id" });
            DropIndex("dbo.LegacyTrees", new[] { "Legacy_Id" });
            DropIndex("dbo.Legacies", new[] { "Scenario_Id" });
            DropIndex("dbo.Characters", new[] { "Year_Id" });
            DropIndex("dbo.Characters", new[] { "Race_Id" });
            DropIndex("dbo.Characters", new[] { "Player_Id" });
            DropIndex("dbo.Characters", new[] { "Legacy_Id" });
            DropIndex("dbo.Spells", new[] { "Character_Id" });
            DropIndex("dbo.Spells", new[] { "Skill_Id" });
            DropIndex("dbo.Skills", new[] { "Character_Id" });
            DropIndex("dbo.Skills", new[] { "Race_Id" });
            DropIndex("dbo.Skills", new[] { "Category_Id" });
            DropIndex("dbo.Categories", new[] { "Year_Id" });
            DropIndex("dbo.Categories", new[] { "Character_Id" });
            DropTable("dbo.EventCharacters");
            DropTable("dbo.skill_prerequisites");
            DropTable("dbo.Throphies");
            DropTable("dbo.Races");
            DropTable("dbo.Events");
            DropTable("dbo.Years");
            DropTable("dbo.Scenarios");
            DropTable("dbo.LegacySkills");
            DropTable("dbo.LegacyTrees");
            DropTable("dbo.Legacies");
            DropTable("dbo.Characters");
            DropTable("dbo.Spells");
            DropTable("dbo.Skills");
            DropTable("dbo.Categories");
            DropTable("dbo.Users");
        }
    }
}
