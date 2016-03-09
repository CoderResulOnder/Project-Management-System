namespace projeoneritakipsistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class azurevs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.azureprojekontrols",
                c => new
                    {
                        odevid = c.Int(nullable: false, identity: true),
                        kullanici = c.String(),
                        bulutSistemleriblogurl = c.String(),
                        sanalmakineblogurl = c.String(),
                        webservisleriblogurl = c.String(),
                        storageblogurl = c.String(),
                        kullanmadiginizbulutservisiblogurl = c.String(),
                        dataset = c.String(),
                        MachineLearningProjeadi = c.String(),
                        projeaciklama = c.String(),
                        projegrupuyeleri = c.String(),
                        kullanilanmodeltanit = c.String(),
                        bitmisproje = c.String(),
                        teslimtarihi = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.odevid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.azureprojekontrols");
        }
    }
}
