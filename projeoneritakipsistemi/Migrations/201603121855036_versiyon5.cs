namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versiyon5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ogrencis", "ogrenci_resimurl", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ogrencis", "ogrenci_resimurl");
        }
    }
}
