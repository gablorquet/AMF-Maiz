namespace AMF.Core.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UserFix : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "Animateurs");
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
            
            DropColumn("dbo.Animateurs", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Animateurs", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.Players");
            RenameTable(name: "dbo.Animateurs", newName: "Users");
        }
    }
}
