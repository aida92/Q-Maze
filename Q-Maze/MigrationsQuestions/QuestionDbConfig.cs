namespace QMaze.MigrationsQuestions
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class QuestionDbConfig : DbMigrationsConfiguration<QMaze.DataAccessLayer.QuestionContext>
    {
        public QuestionDbConfig()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"MigrationsQuestions";
            ContextKey = "QMaze.DataAccessLayer.QuestionContext";
        }

        protected override void Seed(QMaze.DataAccessLayer.QuestionContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
