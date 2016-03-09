namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vs9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.begenmes", "begenmedurumu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.begenmes", "begenmedurumu");
        }
    }
}
