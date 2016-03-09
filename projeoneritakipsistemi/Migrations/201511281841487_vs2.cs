namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vs2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "kullanici_turu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "kullanici_turu");
        }
    }
}
