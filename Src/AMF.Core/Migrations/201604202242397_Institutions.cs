namespace AMF.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Institutions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Institutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Archived = c.DateTime(),
                        Leader_Id = c.Int(),
                        Nature_Id = c.Int(),
                        Year_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Characters", t => t.Leader_Id)
                .ForeignKey("dbo.Natures", t => t.Nature_Id)
                .ForeignKey("dbo.Years", t => t.Year_Id)
                .Index(t => t.Leader_Id)
                .Index(t => t.Nature_Id)
                .Index(t => t.Year_Id);
            
            CreateTable(
                "dbo.Natures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Archived = c.DateTime(),
                        Legacy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LegacyTrees", t => t.Legacy_Id)
                .Index(t => t.Legacy_Id);
            
            CreateTable(
                "dbo.Milestones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Cost = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Archived = c.DateTime(),
                        Nature_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Natures", t => t.Nature_Id)
                .Index(t => t.Nature_Id);
            
            AddColumn("dbo.LegacySkills", "Institution_Id", c => c.Int());
            AddColumn("dbo.Characters", "Institution_Id", c => c.Int());
            AddColumn("dbo.Characters", "Institution_Id1", c => c.Int());
            CreateIndex("dbo.LegacySkills", "Institution_Id");
            CreateIndex("dbo.Characters", "Institution_Id");
            CreateIndex("dbo.Characters", "Institution_Id1");
            AddForeignKey("dbo.Characters", "Institution_Id", "dbo.Institutions", "Id");
            AddForeignKey("dbo.LegacySkills", "Institution_Id", "dbo.Institutions", "Id");
            AddForeignKey("dbo.Characters", "Institution_Id1", "dbo.Institutions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Characters", "Institution_Id1", "dbo.Institutions");
            DropForeignKey("dbo.Institutions", "Year_Id", "dbo.Years");
            DropForeignKey("dbo.Institutions", "Nature_Id", "dbo.Natures");
            DropForeignKey("dbo.Milestones", "Nature_Id", "dbo.Natures");
            DropForeignKey("dbo.Natures", "Legacy_Id", "dbo.LegacyTrees");
            DropForeignKey("dbo.LegacySkills", "Institution_Id", "dbo.Institutions");
            DropForeignKey("dbo.Institutions", "Leader_Id", "dbo.Characters");
            DropForeignKey("dbo.Characters", "Institution_Id", "dbo.Institutions");
            DropIndex("dbo.Milestones", new[] { "Nature_Id" });
            DropIndex("dbo.Natures", new[] { "Legacy_Id" });
            DropIndex("dbo.Institutions", new[] { "Year_Id" });
            DropIndex("dbo.Institutions", new[] { "Nature_Id" });
            DropIndex("dbo.Institutions", new[] { "Leader_Id" });
            DropIndex("dbo.Characters", new[] { "Institution_Id1" });
            DropIndex("dbo.Characters", new[] { "Institution_Id" });
            DropIndex("dbo.LegacySkills", new[] { "Institution_Id" });
            DropColumn("dbo.Characters", "Institution_Id1");
            DropColumn("dbo.Characters", "Institution_Id");
            DropColumn("dbo.LegacySkills", "Institution_Id");
            DropTable("dbo.Milestones");
            DropTable("dbo.Natures");
            DropTable("dbo.Institutions");
        }
    }
}
