namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versiyon9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.kaynaks", "kaynak_yukleyen_id", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.kaynaks", "kaynak_yukleyen_id", c => c.Int());
        }
    }
}
