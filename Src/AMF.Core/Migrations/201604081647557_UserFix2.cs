namespace AMF.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserFix2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Animateurs", newName: "Users");
            AddColumn("dbo.Users", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.Players");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Username = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        DateOfBirth = c.DateTime(),
                        CreationDate = c.DateTime(nullable: false),
                        Archived = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Users", "Discriminator");
            RenameTable(name: "dbo.Users", newName: "Animateurs");
        }
    }
}
