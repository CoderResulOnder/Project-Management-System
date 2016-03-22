namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versiyon8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.projes", "proje_teslim_tarihi", c => c.DateTime(nullable: false));
            AlterColumn("dbo.projes", "proje_yayin_tarihi", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.projes", "proje_yayin_tarihi", c => c.DateTime());
            AlterColumn("dbo.projes", "proje_teslim_tarihi", c => c.DateTime());
        }
    }
}
