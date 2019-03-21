namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RocktifyDBv1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Registrations",
                c => new
                    {
                        RegistrationId = c.Int(nullable: false),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Username = c.String(),
                        Email = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RegistrationId)
                .ForeignKey("dbo.Users", t => t.RegistrationId, cascadeDelete: true)
                .Index(t => t.RegistrationId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UserAccounts",
                c => new
                    {
                        UserAccountId = c.Int(nullable: false),
                        Password = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserAccountId)
                .ForeignKey("dbo.Users", t => t.UserAccountId, cascadeDelete: true)
                .Index(t => t.UserAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAccounts", "UserAccountId", "dbo.Users");
            DropForeignKey("dbo.Registrations", "RegistrationId", "dbo.Users");
            DropIndex("dbo.UserAccounts", new[] { "UserAccountId" });
            DropIndex("dbo.Registrations", new[] { "RegistrationId" });
            DropTable("dbo.UserAccounts");
            DropTable("dbo.Users");
            DropTable("dbo.Registrations");
        }
    }
}
