namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versiyon7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.projes", "proje_adi", c => c.String());
            AlterColumn("dbo.projes", "proje_teslim_tarihi", c => c.DateTime());
            AlterColumn("dbo.projes", "proje_durumu", c => c.String());
            AlterColumn("dbo.projes", "proje_aciklamasi", c => c.String());
            AlterColumn("dbo.projes", "proje_turu", c => c.String());
            AlterColumn("dbo.projes", "proje_kisi_siniri", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.projes", "proje_kisi_siniri", c => c.Int(nullable: false));
            AlterColumn("dbo.projes", "proje_turu", c => c.String(nullable: false));
            AlterColumn("dbo.projes", "proje_aciklamasi", c => c.String(nullable: false));
            AlterColumn("dbo.projes", "proje_durumu", c => c.String(nullable: false));
            AlterColumn("dbo.projes", "proje_teslim_tarihi", c => c.DateTime(nullable: false));
            AlterColumn("dbo.projes", "proje_adi", c => c.String(nullable: false));
        }
    }
}
