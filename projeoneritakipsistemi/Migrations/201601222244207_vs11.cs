namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vs11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.begenmes", "proje_proje_id", c => c.Int());
            CreateIndex("dbo.begenmes", "proje_proje_id");
            AddForeignKey("dbo.begenmes", "proje_proje_id", "dbo.projes", "proje_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.begenmes", "proje_proje_id", "dbo.projes");
            DropIndex("dbo.begenmes", new[] { "proje_proje_id" });
            DropColumn("dbo.begenmes", "proje_proje_id");
        }
    }
}
