namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versiyon81 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.begenmes", "begenmemesebebi", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.begenmes", "begenmemesebebi");
        }
    }
}
