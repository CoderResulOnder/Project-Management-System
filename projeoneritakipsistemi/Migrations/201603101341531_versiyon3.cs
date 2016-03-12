namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class versiyon3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ogrencis", "proje_proje_id", "dbo.projes");
            DropForeignKey("dbo.projes", "ogrenci_ogrenci_id", "dbo.ogrencis");
            DropForeignKey("dbo.projes", "ogrenci_ogrenci_id1", "dbo.ogrencis");
            DropIndex("dbo.ogrencis", new[] { "proje_proje_id" });
            DropIndex("dbo.projes", new[] { "ogrenci_ogrenci_id" });
            DropIndex("dbo.projes", new[] { "ogrenci_ogrenci_id1" });
            CreateTable(
                "dbo.projeogrencis",
                c => new
                    {
                        proje_proje_id = c.Int(nullable: false),
                        ogrenci_ogrenci_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.proje_proje_id, t.ogrenci_ogrenci_id })
                .ForeignKey("dbo.projes", t => t.proje_proje_id, cascadeDelete: true)
                .ForeignKey("dbo.ogrencis", t => t.ogrenci_ogrenci_id, cascadeDelete: true)
                .Index(t => t.proje_proje_id)
                .Index(t => t.ogrenci_ogrenci_id);
            
            DropColumn("dbo.ogrencis", "proje_proje_id");
            DropColumn("dbo.projes", "ogrenci_ogrenci_id");
            DropColumn("dbo.projes", "ogrenci_ogrenci_id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.projes", "ogrenci_ogrenci_id1", c => c.Int());
            AddColumn("dbo.projes", "ogrenci_ogrenci_id", c => c.Int());
            AddColumn("dbo.ogrencis", "proje_proje_id", c => c.Int());
            DropForeignKey("dbo.projeogrencis", "ogrenci_ogrenci_id", "dbo.ogrencis");
            DropForeignKey("dbo.projeogrencis", "proje_proje_id", "dbo.projes");
            DropIndex("dbo.projeogrencis", new[] { "ogrenci_ogrenci_id" });
            DropIndex("dbo.projeogrencis", new[] { "proje_proje_id" });
            DropTable("dbo.projeogrencis");
            CreateIndex("dbo.projes", "ogrenci_ogrenci_id1");
            CreateIndex("dbo.projes", "ogrenci_ogrenci_id");
            CreateIndex("dbo.ogrencis", "proje_proje_id");
            AddForeignKey("dbo.projes", "ogrenci_ogrenci_id1", "dbo.ogrencis", "ogrenci_id");
            AddForeignKey("dbo.projes", "ogrenci_ogrenci_id", "dbo.ogrencis", "ogrenci_id");
            AddForeignKey("dbo.ogrencis", "proje_proje_id", "dbo.projes", "proje_id");
        }
    }
}
