namespace ValidacionTelefonica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeagregacolumnaaTableReglaValidacion : DbMigration
    {
        public override void Up()
        {
            AddColumn("Negocio.ReglaValidacion", "ReglaValidacionID", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            DropColumn("Negocio.ReglaValidacion", "ReglaValidacionID");
        }
    }
}
