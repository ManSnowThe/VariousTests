namespace VariousTests.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VariousTest2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsersCompletedTest",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        TestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.TestId })
                .ForeignKey("dbo.UserProfiles", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.VarTests", t => t.TestId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersCompletedTest", "TestId", "dbo.VarTests");
            DropForeignKey("dbo.UsersCompletedTest", "UserId", "dbo.UserProfiles");
            DropIndex("dbo.UsersCompletedTest", new[] { "TestId" });
            DropIndex("dbo.UsersCompletedTest", new[] { "UserId" });
            DropTable("dbo.UsersCompletedTest");
        }
    }
}
