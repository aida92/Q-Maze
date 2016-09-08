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
                new Question{Text = "One?", Answers="Hope so!\nNo\nYes", CorrectAnswer="2", Points=666},
                new Question{Text = "Two?", Answers="Hope so!\nNo\nYes", CorrectAnswer="2", Points=666},
                new Question{Text = "Three?", Answers="Hope so!\nNo\nYes", CorrectAnswer="1", Points=666},
                new Question{Text = "Four?", Answers="Hope so!\nNo\nYes", CorrectAnswer="1", Points=666},
                new Question{Text = "Five?", Answers="Hope so!\nNo\nYes", CorrectAnswer="1", Points=666},
                new Question{Text = "Six?", Answers="Hope so!\nNo\nYes", CorrectAnswer="2", Points=666}
            };
            questions.ForEach(q => context.Questions.Add(q));
            context.SaveChanges();
        }
    }
}