namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vs12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.projes", "projeolusturanid", c => c.String());
            DropColumn("dbo.projes", "proje_ogrenci_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.projes", "proje_ogrenci_id", c => c.Int());
            DropColumn("dbo.projes", "projeolusturanid");
        }
    }
}
