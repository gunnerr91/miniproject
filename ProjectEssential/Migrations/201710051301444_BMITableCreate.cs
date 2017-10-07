namespace ProjectEssential.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BMITableCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BMIs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Height = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Unit = c.Int(nullable: false),
                        BMI = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BMIs");
        }
    }
}
