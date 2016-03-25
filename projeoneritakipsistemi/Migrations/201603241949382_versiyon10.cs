namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versiyon10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.projes", "proje_yapimiyla_ilgili_öneri", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.projes", "proje_yapimiyla_ilgili_öneri");
        }
    }
}
