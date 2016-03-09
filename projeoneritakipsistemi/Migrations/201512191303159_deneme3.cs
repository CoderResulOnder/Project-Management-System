namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deneme3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.yazilimmuhs", "Projeadi", c => c.String());
            AlterColumn("dbo.yazilimmuhs", "projeaciklama", c => c.String());
            AlterColumn("dbo.yazilimmuhs", "projegrupuyeleri", c => c.String());
            AlterColumn("dbo.yazilimmuhs", "projedil", c => c.String());
            AlterColumn("dbo.yazilimmuhs", "projeide", c => c.String());
            AlterColumn("dbo.yazilimmuhs", "surumkontsisadi", c => c.String());
            AlterColumn("dbo.yazilimmuhs", "baclocimgurl", c => c.String());
            AlterColumn("dbo.yazilimmuhs", "sprintimgurl", c => c.String());
            AlterColumn("dbo.yazilimmuhs", "checkinimgurl", c => c.String());
            AlterColumn("dbo.yazilimmuhs", "youtubecalismavideourl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.yazilimmuhs", "youtubecalismavideourl", c => c.String(nullable: false));
            AlterColumn("dbo.yazilimmuhs", "checkinimgurl", c => c.String(nullable: false));
            AlterColumn("dbo.yazilimmuhs", "sprintimgurl", c => c.String(nullable: false));
            AlterColumn("dbo.yazilimmuhs", "baclocimgurl", c => c.String(nullable: false));
            AlterColumn("dbo.yazilimmuhs", "surumkontsisadi", c => c.String(nullable: false));
            AlterColumn("dbo.yazilimmuhs", "projeide", c => c.String(nullable: false));
            AlterColumn("dbo.yazilimmuhs", "projedil", c => c.String(nullable: false));
            AlterColumn("dbo.yazilimmuhs", "projegrupuyeleri", c => c.String(nullable: false));
            AlterColumn("dbo.yazilimmuhs", "projeaciklama", c => c.String(nullable: false));
            AlterColumn("dbo.yazilimmuhs", "Projeadi", c => c.String(nullable: false));
        }
    }
}
