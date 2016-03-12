namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versiyon4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.projeogrencis", "proje_proje_id", "dbo.projes");
            DropForeignKey("dbo.projeogrencis", "ogrenci_ogrenci_id", "dbo.ogrencis");
            DropIndex("dbo.projeogrencis", new[] { "proje_proje_id" });
            DropIndex("dbo.projeogrencis", new[] { "ogrenci_ogrenci_id" });
            RenameColumn(table: "dbo.projes", name: "akademisyen_akademisyen_id", newName: "akademisyen_id");
            RenameColumn(table: "dbo.projes", name: "bolum_bolum_id", newName: "bolum_id");
            RenameColumn(table: "dbo.projes", name: "diger_kullanicilar_diger_kullanicilar_id", newName: "diger_kullanicilar_id");
            RenameIndex(table: "dbo.projes", name: "IX_akademisyen_akademisyen_id", newName: "IX_akademisyen_id");
            RenameIndex(table: "dbo.projes", name: "IX_bolum_bolum_id", newName: "IX_bolum_id");
            RenameIndex(table: "dbo.projes", name: "IX_diger_kullanicilar_diger_kullanicilar_id", newName: "IX_diger_kullanicilar_id");
            AddColumn("dbo.projes", "ogrenci_id", c => c.Int());
            CreateIndex("dbo.projes", "ogrenci_id");
            AddForeignKey("dbo.projes", "ogrenci_id", "dbo.ogrencis", "ogrenci_id");
            DropColumn("dbo.projes", "proje_akademisyen_id");
            DropColumn("dbo.projes", "proje_bolum_id");
            DropColumn("dbo.projes", "proje_diger_kul_id");
            DropTable("dbo.projeogrencis");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.projeogrencis",
                c => new
                    {
                        proje_proje_id = c.Int(nullable: false),
                        ogrenci_ogrenci_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.proje_proje_id, t.ogrenci_ogrenci_id });
            
            AddColumn("dbo.projes", "proje_diger_kul_id", c => c.Int());
            AddColumn("dbo.projes", "proje_bolum_id", c => c.Int());
            AddColumn("dbo.projes", "proje_akademisyen_id", c => c.Int());
            DropForeignKey("dbo.projes", "ogrenci_id", "dbo.ogrencis");
            DropIndex("dbo.projes", new[] { "ogrenci_id" });
            DropColumn("dbo.projes", "ogrenci_id");
            RenameIndex(table: "dbo.projes", name: "IX_diger_kullanicilar_id", newName: "IX_diger_kullanicilar_diger_kullanicilar_id");
            RenameIndex(table: "dbo.projes", name: "IX_bolum_id", newName: "IX_bolum_bolum_id");
            RenameIndex(table: "dbo.projes", name: "IX_akademisyen_id", newName: "IX_akademisyen_akademisyen_id");
            RenameColumn(table: "dbo.projes", name: "diger_kullanicilar_id", newName: "diger_kullanicilar_diger_kullanicilar_id");
            RenameColumn(table: "dbo.projes", name: "bolum_id", newName: "bolum_bolum_id");
            RenameColumn(table: "dbo.projes", name: "akademisyen_id", newName: "akademisyen_akademisyen_id");
            CreateIndex("dbo.projeogrencis", "ogrenci_ogrenci_id");
            CreateIndex("dbo.projeogrencis", "proje_proje_id");
            AddForeignKey("dbo.projeogrencis", "ogrenci_ogrenci_id", "dbo.ogrencis", "ogrenci_id", cascadeDelete: true);
            AddForeignKey("dbo.projeogrencis", "proje_proje_id", "dbo.projes", "proje_id", cascadeDelete: true);
        }
    }
}
