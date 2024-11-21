namespace ValidacionTelefonica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PruebaCoinciliar : DbMigration
    {
        public override void Up()
        {
            DropColumn("Negocio.ReglaValidacion", "ReglaValidacionID");
        }
        
        public override void Down()
        {
            DropColumn("Negocio.ReglaValidacion", "ReglaValidacionID");
        }
    }
}
