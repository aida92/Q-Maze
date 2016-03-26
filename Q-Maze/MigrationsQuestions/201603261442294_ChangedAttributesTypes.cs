namespace QMaze.MigrationsQuestions
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAttributesTypes : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Question");
            AlterColumn("dbo.Question", "ID", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Question", "Points", c => c.Short(nullable: false));
            AddPrimaryKey("dbo.Question", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Question");
            AlterColumn("dbo.Question", "Points", c => c.Int(nullable: false));
            AlterColumn("dbo.Question", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Question", "ID");
        }
    }
}
