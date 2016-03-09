namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vs8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.begenmes",
                c => new
                    {
                        begenmeid = c.Int(nullable: false, identity: true),
                        projeid = c.Int(),
                        begenenid = c.Int(),
                    })
                .PrimaryKey(t => t.begenmeid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.begenmes");
        }
    }
}
