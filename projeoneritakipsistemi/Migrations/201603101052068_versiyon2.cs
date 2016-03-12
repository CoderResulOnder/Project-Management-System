namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versiyon2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.sirkets",
                c => new
                    {
                        sirket_adi = c.String(nullable: false, maxLength: 128),
                        sirket_adresi = c.String(nullable: false),
                        sirket_tel = c.String(nullable: false),
                        sirket_site = c.String(),
                        sirket_mail = c.String(nullable: false),
                        sirket_fax_adresi = c.String(),
                        sirketcalismaalani = c.String(),
                        sirket_hakkinda = c.String(),
                        sirketi_ekleyen_kullaniciid = c.String(),
                    })
                .PrimaryKey(t => t.sirket_adi);
            
            AddColumn("dbo.projes", "sirket_sirket_adi", c => c.String(maxLength: 128));
            CreateIndex("dbo.projes", "sirket_sirket_adi");
            AddForeignKey("dbo.projes", "sirket_sirket_adi", "dbo.sirkets", "sirket_adi");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.projes", "sirket_sirket_adi", "dbo.sirkets");
            DropIndex("dbo.projes", new[] { "sirket_sirket_adi" });
            DropColumn("dbo.projes", "sirket_sirket_adi");
            DropTable("dbo.sirkets");
        }
    }
}
