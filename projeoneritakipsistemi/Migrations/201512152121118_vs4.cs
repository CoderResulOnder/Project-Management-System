namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vs4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.diger_kullanicilar", "diger_kullanicilar_kayit_tarihi", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.diger_kullanicilar", "diger_kullanicilar_kayit_tarihi", c => c.String());
        }
    }
}
