namespace ValidacionTelefonica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnReglaValidacion3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Negocio.ReglaValidacion", "ReglaValiddacionID", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AddColumn("Negocio.ReglaValidacion", "ReglaValidacionID", c => c.Int(nullable: false, identity: true));
        }
    }
}
