namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versiyon6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ogrencis", "ogrenci_resimurl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ogrencis", "ogrenci_resimurl", c => c.String(nullable: false));
        }
    }
}
