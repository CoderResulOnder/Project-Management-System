namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versiyon1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.projes", "ogrenci_ogrenci_id", "dbo.ogrencis");
            AddColumn("dbo.ogrencis", "proje_proje_id", c => c.Int());
            AddColumn("dbo.projes", "ogrenci_ogrenci_id1", c => c.Int());
            CreateIndex("dbo.ogrencis", "proje_proje_id");
            CreateIndex("dbo.projes", "ogrenci_ogrenci_id1");
            AddForeignKey("dbo.ogrencis", "proje_proje_id", "dbo.projes", "proje_id");
            AddForeignKey("dbo.projes", "ogrenci_ogrenci_id1", "dbo.ogrencis", "ogrenci_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.projes", "ogrenci_ogrenci_id1", "dbo.ogrencis");
            DropForeignKey("dbo.ogrencis", "proje_proje_id", "dbo.projes");
            DropIndex("dbo.projes", new[] { "ogrenci_ogrenci_id1" });
            DropIndex("dbo.ogrencis", new[] { "proje_proje_id" });
            DropColumn("dbo.projes", "ogrenci_ogrenci_id1");
            DropColumn("dbo.ogrencis", "proje_proje_id");
            AddForeignKey("dbo.projes", "ogrenci_ogrenci_id", "dbo.ogrencis", "ogrenci_id");
        }
    }
}
