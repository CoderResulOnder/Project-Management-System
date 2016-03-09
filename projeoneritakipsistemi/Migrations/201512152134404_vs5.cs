namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vs5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.akademisyens", "akademisyen_adi", c => c.String(nullable: false));
            AlterColumn("dbo.akademisyens", "akademisyen_soyadi", c => c.String(nullable: false));
            AlterColumn("dbo.akademisyens", "akademisyen_tc", c => c.String(nullable: false));
            AlterColumn("dbo.akademisyens", "akademisyen_tel", c => c.String(nullable: false));
            AlterColumn("dbo.akademisyens", "akademisyen_oda_no", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.akademisyens", "akademisyen_oda_no", c => c.Int());
            AlterColumn("dbo.akademisyens", "akademisyen_tel", c => c.String());
            AlterColumn("dbo.akademisyens", "akademisyen_tc", c => c.String());
            AlterColumn("dbo.akademisyens", "akademisyen_soyadi", c => c.String());
            AlterColumn("dbo.akademisyens", "akademisyen_adi", c => c.String());
        }
    }
}
