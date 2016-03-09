namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vs3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.kaynaks", "kaynak_url", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.kaynaks", "kaynak_url", c => c.String());
        }
    }
}
