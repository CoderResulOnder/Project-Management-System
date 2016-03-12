namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versiyon0 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ogrencis", "ogrenci_bitirmeprojesiid", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ogrencis", "ogrenci_bitirmeprojesiid");
        }
    }
}
