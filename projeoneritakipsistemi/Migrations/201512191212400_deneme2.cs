namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deneme2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.yazilimmuhs", "Projeadi", c => c.String(nullable: false));
            AddColumn("dbo.yazilimmuhs", "projeaciklama", c => c.String(nullable: false));
            AddColumn("dbo.yazilimmuhs", "projegrupuyeleri", c => c.String(nullable: false));
            AddColumn("dbo.yazilimmuhs", "projedil", c => c.String(nullable: false));
            AddColumn("dbo.yazilimmuhs", "projeide", c => c.String(nullable: false));
            AddColumn("dbo.yazilimmuhs", "surumkontsisadi", c => c.String(nullable: false));
            AddColumn("dbo.yazilimmuhs", "teslimtarihi", c => c.DateTime(nullable: false));
            AddColumn("dbo.yazilimmuhs", "baclocimgurl", c => c.String(nullable: false));
            AddColumn("dbo.yazilimmuhs", "sprintimgurl", c => c.String(nullable: false));
            AddColumn("dbo.yazilimmuhs", "checkinimgurl", c => c.String(nullable: false));
            AddColumn("dbo.yazilimmuhs", "youtubecalismavideourl", c => c.String(nullable: false));
            DropColumn("dbo.yazilimmuhs", "adi");
        }
        
        public override void Down()
        {
            AddColumn("dbo.yazilimmuhs", "adi", c => c.String(nullable: false));
            DropColumn("dbo.yazilimmuhs", "youtubecalismavideourl");
            DropColumn("dbo.yazilimmuhs", "checkinimgurl");
            DropColumn("dbo.yazilimmuhs", "sprintimgurl");
            DropColumn("dbo.yazilimmuhs", "baclocimgurl");
            DropColumn("dbo.yazilimmuhs", "teslimtarihi");
            DropColumn("dbo.yazilimmuhs", "surumkontsisadi");
            DropColumn("dbo.yazilimmuhs", "projeide");
            DropColumn("dbo.yazilimmuhs", "projedil");
            DropColumn("dbo.yazilimmuhs", "projegrupuyeleri");
            DropColumn("dbo.yazilimmuhs", "projeaciklama");
            DropColumn("dbo.yazilimmuhs", "Projeadi");
        }
    }
}
