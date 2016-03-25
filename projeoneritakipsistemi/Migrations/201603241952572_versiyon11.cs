namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versiyon11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.projes", "proje_yapimiyla_ilgili_oneri", c => c.String());
            DropColumn("dbo.projes", "proje_yapimiyla_ilgili_öneri");
        }
        
        public override void Down()
        {
            AddColumn("dbo.projes", "proje_yapimiyla_ilgili_öneri", c => c.String());
            DropColumn("dbo.projes", "proje_yapimiyla_ilgili_oneri");
        }
    }
}
