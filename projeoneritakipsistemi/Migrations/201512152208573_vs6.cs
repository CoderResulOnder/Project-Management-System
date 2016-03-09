namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vs6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.bolums", "bolum_adi", c => c.String(nullable: false));
            AlterColumn("dbo.bolums", "bolum_adresi", c => c.String(nullable: false));
            AlterColumn("dbo.bolums", "bolum_tel", c => c.String(nullable: false));
            AlterColumn("dbo.bolums", "bolum_faks", c => c.String(nullable: false));
            AlterColumn("dbo.bolums", "bolum_kodu", c => c.String(nullable: false));
            AlterColumn("dbo.fakultes", "fakulte_adi", c => c.String(nullable: false));
            AlterColumn("dbo.fakultes", "fakulte_tel", c => c.String(nullable: false));
            AlterColumn("dbo.fakultes", "fakulte_faks", c => c.String(nullable: false));
            AlterColumn("dbo.ogrencis", "ogrenci_adi", c => c.String(nullable: false));
            AlterColumn("dbo.ogrencis", "ogrenci_soyadi", c => c.String(nullable: false));
            AlterColumn("dbo.ogrencis", "ogrenci_no", c => c.String(nullable: false));
            AlterColumn("dbo.ogrencis", "ogrenci_adresi", c => c.String(nullable: false));
            AlterColumn("dbo.ogrencis", "ogrenci_tel", c => c.String(nullable: false));
            AlterColumn("dbo.ogrencis", "ogrenci_tc", c => c.String(nullable: false));
            AlterColumn("dbo.grups", "grup_adi", c => c.String(nullable: false));
            AlterColumn("dbo.grups", "grup_uye_sayisi", c => c.Int(nullable: false));
            AlterColumn("dbo.projes", "proje_adi", c => c.String(nullable: false));
            AlterColumn("dbo.projes", "proje_teslim_tarihi", c => c.DateTime(nullable: false));
            AlterColumn("dbo.projes", "proje_durumu", c => c.String(nullable: false));
            AlterColumn("dbo.projes", "proje_aciklamasi", c => c.String(nullable: false));
            AlterColumn("dbo.projes", "proje_turu", c => c.String(nullable: false));
            AlterColumn("dbo.projes", "proje_kisi_siniri", c => c.Int(nullable: false));
            AlterColumn("dbo.diger_kullanicilar", "diger_kullanicilar_adi", c => c.String(nullable: false));
            AlterColumn("dbo.diger_kullanicilar", "diger_kullanicilar_soyadi", c => c.String(nullable: false));
            AlterColumn("dbo.diger_kullanicilar", "diger_kullanicilar_unvan", c => c.String(nullable: false));
            AlterColumn("dbo.diger_kullanicilar", "diger_kullanicilar_uzmanlik_alani", c => c.String(nullable: false));
            AlterColumn("dbo.diger_kullanicilar", "diger_kullanicilar_acıklama", c => c.String(nullable: false));
            AlterColumn("dbo.kaynaks", "kaynak_name", c => c.String(nullable: false));
            AlterColumn("dbo.kaynaks", "kaynak_aciklamasi", c => c.String(nullable: false));
            AlterColumn("dbo.yorums", "yorum_icerigi", c => c.String(nullable: false));
            AlterColumn("dbo.yorums", "yorumu_yapan_statu", c => c.String(nullable: false));
            AlterColumn("dbo.universites", "universite_adi", c => c.String(nullable: false));
            AlterColumn("dbo.universites", "universite_adresi", c => c.String(nullable: false));
            AlterColumn("dbo.universites", "universite_faks", c => c.String(nullable: false));
            AlterColumn("dbo.universites", "universite_tel", c => c.String(nullable: false));
            AlterColumn("dbo.universites", "universite_sehir", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.universites", "universite_sehir", c => c.String());
            AlterColumn("dbo.universites", "universite_tel", c => c.String());
            AlterColumn("dbo.universites", "universite_faks", c => c.String());
            AlterColumn("dbo.universites", "universite_adresi", c => c.String());
            AlterColumn("dbo.universites", "universite_adi", c => c.String());
            AlterColumn("dbo.yorums", "yorumu_yapan_statu", c => c.String());
            AlterColumn("dbo.yorums", "yorum_icerigi", c => c.String());
            AlterColumn("dbo.kaynaks", "kaynak_aciklamasi", c => c.String());
            AlterColumn("dbo.kaynaks", "kaynak_name", c => c.String());
            AlterColumn("dbo.diger_kullanicilar", "diger_kullanicilar_acıklama", c => c.String());
            AlterColumn("dbo.diger_kullanicilar", "diger_kullanicilar_uzmanlik_alani", c => c.String());
            AlterColumn("dbo.diger_kullanicilar", "diger_kullanicilar_unvan", c => c.String());
            AlterColumn("dbo.diger_kullanicilar", "diger_kullanicilar_soyadi", c => c.String());
            AlterColumn("dbo.diger_kullanicilar", "diger_kullanicilar_adi", c => c.String());
            AlterColumn("dbo.projes", "proje_kisi_siniri", c => c.Int());
            AlterColumn("dbo.projes", "proje_turu", c => c.String());
            AlterColumn("dbo.projes", "proje_aciklamasi", c => c.String());
            AlterColumn("dbo.projes", "proje_durumu", c => c.String());
            AlterColumn("dbo.projes", "proje_teslim_tarihi", c => c.DateTime());
            AlterColumn("dbo.projes", "proje_adi", c => c.String());
            AlterColumn("dbo.grups", "grup_uye_sayisi", c => c.Int());
            AlterColumn("dbo.grups", "grup_adi", c => c.String());
            AlterColumn("dbo.ogrencis", "ogrenci_tc", c => c.String());
            AlterColumn("dbo.ogrencis", "ogrenci_tel", c => c.String());
            AlterColumn("dbo.ogrencis", "ogrenci_adresi", c => c.String());
            AlterColumn("dbo.ogrencis", "ogrenci_no", c => c.String());
            AlterColumn("dbo.ogrencis", "ogrenci_soyadi", c => c.String());
            AlterColumn("dbo.ogrencis", "ogrenci_adi", c => c.String());
            AlterColumn("dbo.fakultes", "fakulte_faks", c => c.String());
            AlterColumn("dbo.fakultes", "fakulte_tel", c => c.String());
            AlterColumn("dbo.fakultes", "fakulte_adi", c => c.String());
            AlterColumn("dbo.bolums", "bolum_kodu", c => c.String());
            AlterColumn("dbo.bolums", "bolum_faks", c => c.String());
            AlterColumn("dbo.bolums", "bolum_tel", c => c.String());
            AlterColumn("dbo.bolums", "bolum_adresi", c => c.String());
            AlterColumn("dbo.bolums", "bolum_adi", c => c.String());
        }
    }
}
