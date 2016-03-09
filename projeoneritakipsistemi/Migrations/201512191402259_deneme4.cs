namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deneme4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.yazilimmuhs", "kullaniciadi", c => c.String());
            AddColumn("dbo.yazilimmuhs", "parola", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.yazilimmuhs", "parola");
            DropColumn("dbo.yazilimmuhs", "kullaniciadi");
        }
    }
}
