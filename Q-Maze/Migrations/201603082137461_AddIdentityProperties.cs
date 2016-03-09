namespace QMaze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdentityProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "HighScore", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "GamesPlayed", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "GamesWon", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "QuestionsTotal", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "QuestionsCorrect", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "RegisterDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "RegisterDate");
            DropColumn("dbo.AspNetUsers", "QuestionsCorrect");
            DropColumn("dbo.AspNetUsers", "QuestionsTotal");
            DropColumn("dbo.AspNetUsers", "GamesWon");
            DropColumn("dbo.AspNetUsers", "GamesPlayed");
            DropColumn("dbo.AspNetUsers", "HighScore");
        }
    }
}
