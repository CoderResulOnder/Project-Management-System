namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vs10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.begenmes", "begenenid", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.begenmes", "begenenid", c => c.Int());
        }
    }
}
