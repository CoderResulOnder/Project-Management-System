namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deneme1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.yazilimmuhs",
                c => new
                    {
                        projeid = c.Int(nullable: false, identity: true),
                        adi = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.projeid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.yazilimmuhs");
        }
    }
}
