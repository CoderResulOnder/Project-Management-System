namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vs1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.akademisyens",
                c => new
                    {
                        akademisyen_id = c.Int(nullable: false, identity: true),
                        akademisyen_adi = c.String(),
                        akademisyen_soyadi = c.String(),
                        akademisyen_tc = c.String(),
                        akademisyen_tel = c.String(),
                        akademisyen_sitesi = c.String(),
                        akademisyen_mail = c.String(),
                        akademisyen_parola = c.String(),
                        akademisyen_kullanici_adi = c.String(),
                        akademisyen_unvani = c.String(),
                        akademisyen_uzmanlik_alani = c.String(),
                        akademisyen_oda_no = c.Int(),
                        aka_bolum_id = c.Int(),
                        bolum_bolum_id = c.Int(),
                    })
                .PrimaryKey(t => t.akademisyen_id)
                .ForeignKey("dbo.bolums", t => t.bolum_bolum_id)
                .Index(t => t.bolum_bolum_id);
            
            CreateTable(
                "dbo.bolums",
                c => new
                    {
                        bolum_id = c.Int(nullable: false, identity: true),
                        bolum_adi = c.String(),
                        bolum_adresi = c.String(),
                        bolum_tel = c.String(),
                        bolum_faks = c.String(),
                        bolum_kodu = c.String(),
                        bolum_sitesi = c.String(),
                        bol_fakulte_id = c.Int(),
                        fakulte_fakulte_id = c.Int(),
                    })
                .PrimaryKey(t => t.bolum_id)
                .ForeignKey("dbo.fakultes", t => t.fakulte_fakulte_id)
                .Index(t => t.fakulte_fakulte_id);
            
            CreateTable(
                "dbo.fakultes",
                c => new
                    {
                        fakulte_id = c.Int(nullable: false, identity: true),
                        fakulte_adi = c.String(),
                        fakulte_tel = c.String(),
                        fakulte_faks = c.String(),
                        fak_uni_id = c.Int(),
                        universite_universite_id = c.Int(),
                    })
                .PrimaryKey(t => t.fakulte_id)
                .ForeignKey("dbo.universites", t => t.universite_universite_id)
                .Index(t => t.universite_universite_id);
            
            CreateTable(
                "dbo.ogrencis",
                c => new
                    {
                        ogrenci_id = c.Int(nullable: false, identity: true),
                        ogrenci_adi = c.String(),
                        ogrenci_soyadi = c.String(),
                        ogrenci_no = c.String(),
                        ogrenci_adresi = c.String(),
                        ogrenci_tel = c.String(),
                        ogrenci_email = c.String(),
                        ogrenci_tc = c.String(),
                        ogrenci_kullanici_adi = c.String(),
                        ogrenci_parola = c.String(),
                        ogrenci_sinif = c.Int(),
                        calısma_grub_id = c.Int(),
                        bolum_id = c.Int(),
                        fakulte_id = c.Int(),
                        kayit_tarihi = c.DateTime(),
                        grup_grup_id = c.Int(),
                    })
                .PrimaryKey(t => t.ogrenci_id)
                .ForeignKey("dbo.bolums", t => t.bolum_id)
                .ForeignKey("dbo.fakultes", t => t.fakulte_id)
                .ForeignKey("dbo.grups", t => t.grup_grup_id)
                .Index(t => t.bolum_id)
                .Index(t => t.fakulte_id)
                .Index(t => t.grup_grup_id);
            
            CreateTable(
                "dbo.grups",
                c => new
                    {
                        grup_id = c.Int(nullable: false, identity: true),
                        grup_adi = c.String(),
                        grup_uye_sayisi = c.Int(),
                        grup_proje_id = c.Int(),
                        grub_akademisyen_id = c.Int(),
                        akademisyen_akademisyen_id = c.Int(),
                        proje_proje_id = c.Int(),
                    })
                .PrimaryKey(t => t.grup_id)
                .ForeignKey("dbo.akademisyens", t => t.akademisyen_akademisyen_id)
                .ForeignKey("dbo.projes", t => t.proje_proje_id)
                .Index(t => t.akademisyen_akademisyen_id)
                .Index(t => t.proje_proje_id);
            
            CreateTable(
                "dbo.projes",
                c => new
                    {
                        proje_id = c.Int(nullable: false, identity: true),
                        proje_adi = c.String(),
                        proje_begeni_sayisi = c.Int(),
                        proje_teslim_tarihi = c.DateTime(),
                        proje_durumu = c.String(),
                        proje_aciklamasi = c.String(),
                        proje_turu = c.String(),
                        proje_kisi_siniri = c.Int(),
                        proje_ogrenci_id = c.Int(),
                        proje_akademisyen_id = c.Int(),
                        proje_bolum_id = c.Int(),
                        proje_diger_kul_id = c.Int(),
                        proje_yayin_tarihi = c.DateTime(),
                        akademisyen_akademisyen_id = c.Int(),
                        bolum_bolum_id = c.Int(),
                        diger_kullanicilar_diger_kullanicilar_id = c.Int(),
                        ogrenci_ogrenci_id = c.Int(),
                    })
                .PrimaryKey(t => t.proje_id)
                .ForeignKey("dbo.akademisyens", t => t.akademisyen_akademisyen_id)
                .ForeignKey("dbo.bolums", t => t.bolum_bolum_id)
                .ForeignKey("dbo.diger_kullanicilar", t => t.diger_kullanicilar_diger_kullanicilar_id)
                .ForeignKey("dbo.ogrencis", t => t.ogrenci_ogrenci_id)
                .Index(t => t.akademisyen_akademisyen_id)
                .Index(t => t.bolum_bolum_id)
                .Index(t => t.diger_kullanicilar_diger_kullanicilar_id)
                .Index(t => t.ogrenci_ogrenci_id);
            
            CreateTable(
                "dbo.diger_kullanicilar",
                c => new
                    {
                        diger_kullanicilar_id = c.Int(nullable: false, identity: true),
                        diger_kullanicilar_adi = c.String(),
                        diger_kullanicilar_soyadi = c.String(),
                        diger_kullanicilar_parola = c.String(),
                        diger_kullanicilar_kullanici_adi = c.String(),
                        diger_kullanicilar_mail = c.String(),
                        diger_kullanicilar_unvan = c.String(),
                        diger_kullanicilar_uzmanlik_alani = c.String(),
                        diger_kullanicilar_kayit_tarihi = c.String(),
                        diger_kullanicilar_acıklama = c.String(),
                    })
                .PrimaryKey(t => t.diger_kullanicilar_id);
            
            CreateTable(
                "dbo.kaynaks",
                c => new
                    {
                        kaynak_id = c.Int(nullable: false, identity: true),
                        kaynak_name = c.String(),
                        kaynak_aciklamasi = c.String(),
                        kaynak_tarih = c.DateTime(),
                        kaynak_url = c.String(),
                        proje_id = c.Int(),
                        kaynak_yukleyen_id = c.Int(),
                        kaynak_yukleyen_statu = c.String(),
                    })
                .PrimaryKey(t => t.kaynak_id)
                .ForeignKey("dbo.projes", t => t.proje_id)
                .Index(t => t.proje_id);
            
            CreateTable(
                "dbo.yorums",
                c => new
                    {
                        yorum_id = c.Int(nullable: false, identity: true),
                        yorum_icerigi = c.String(),
                        yorumu_yapan_id = c.String(),
                        yorumu_yapan_statu = c.String(),
                        yor_proje_id = c.Int(),
                        yorum_tarihi = c.DateTime(),
                        proje_proje_id = c.Int(),
                    })
                .PrimaryKey(t => t.yorum_id)
                .ForeignKey("dbo.projes", t => t.proje_proje_id)
                .Index(t => t.proje_proje_id);
            
            CreateTable(
                "dbo.universites",
                c => new
                    {
                        universite_id = c.Int(nullable: false, identity: true),
                        universite_adi = c.String(),
                        universite_adresi = c.String(),
                        universite_faks = c.String(),
                        universite_tel = c.String(),
                        universite_sehir = c.String(),
                    })
                .PrimaryKey(t => t.universite_id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        kullaniciadi = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.fakultes", "universite_universite_id", "dbo.universites");
            DropForeignKey("dbo.yorums", "proje_proje_id", "dbo.projes");
            DropForeignKey("dbo.projes", "ogrenci_ogrenci_id", "dbo.ogrencis");
            DropForeignKey("dbo.kaynaks", "proje_id", "dbo.projes");
            DropForeignKey("dbo.grups", "proje_proje_id", "dbo.projes");
            DropForeignKey("dbo.projes", "diger_kullanicilar_diger_kullanicilar_id", "dbo.diger_kullanicilar");
            DropForeignKey("dbo.projes", "bolum_bolum_id", "dbo.bolums");
            DropForeignKey("dbo.projes", "akademisyen_akademisyen_id", "dbo.akademisyens");
            DropForeignKey("dbo.ogrencis", "grup_grup_id", "dbo.grups");
            DropForeignKey("dbo.grups", "akademisyen_akademisyen_id", "dbo.akademisyens");
            DropForeignKey("dbo.ogrencis", "fakulte_id", "dbo.fakultes");
            DropForeignKey("dbo.ogrencis", "bolum_id", "dbo.bolums");
            DropForeignKey("dbo.bolums", "fakulte_fakulte_id", "dbo.fakultes");
            DropForeignKey("dbo.akademisyens", "bolum_bolum_id", "dbo.bolums");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.yorums", new[] { "proje_proje_id" });
            DropIndex("dbo.kaynaks", new[] { "proje_id" });
            DropIndex("dbo.projes", new[] { "ogrenci_ogrenci_id" });
            DropIndex("dbo.projes", new[] { "diger_kullanicilar_diger_kullanicilar_id" });
            DropIndex("dbo.projes", new[] { "bolum_bolum_id" });
            DropIndex("dbo.projes", new[] { "akademisyen_akademisyen_id" });
            DropIndex("dbo.grups", new[] { "proje_proje_id" });
            DropIndex("dbo.grups", new[] { "akademisyen_akademisyen_id" });
            DropIndex("dbo.ogrencis", new[] { "grup_grup_id" });
            DropIndex("dbo.ogrencis", new[] { "fakulte_id" });
            DropIndex("dbo.ogrencis", new[] { "bolum_id" });
            DropIndex("dbo.fakultes", new[] { "universite_universite_id" });
            DropIndex("dbo.bolums", new[] { "fakulte_fakulte_id" });
            DropIndex("dbo.akademisyens", new[] { "bolum_bolum_id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.universites");
            DropTable("dbo.yorums");
            DropTable("dbo.kaynaks");
            DropTable("dbo.diger_kullanicilar");
            DropTable("dbo.projes");
            DropTable("dbo.grups");
            DropTable("dbo.ogrencis");
            DropTable("dbo.fakultes");
            DropTable("dbo.bolums");
            DropTable("dbo.akademisyens");
        }
    }
}
