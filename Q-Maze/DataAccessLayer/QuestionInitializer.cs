using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using QMaze.Models;


namespace QMaze.DataAccessLayer
{
    public class QuestionInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<QuestionContext>
    {
        protected override void Seed(QuestionContext context)
        {
            //create the base of questions
            var questions = new List<Question>
            {
                new Question{Text = "Is this working?", Answers="Hope so!\nNo\nYes", CorrectAnswer="Yes", Points=666}
            };
            questions.ForEach(q => context.Questions.Add(q));
            context.SaveChanges();
        }
    }
}