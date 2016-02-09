namespace AMF.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategorySkillRelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "NextEvent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "NextEvent");
        }
    }
}
