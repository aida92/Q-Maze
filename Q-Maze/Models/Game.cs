using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QMaze.DataAccessLayer;
using System.ComponentModel.DataAnnotations;

namespace QMaze.Models
{
    public class GameModel
    {
        private ApplicationDbContext appContext = new ApplicationDbContext();
        private QuestionContext questionContext = new QuestionContext();

        public GameModel(string playerId, int level)
        {
            PlayerId = playerId;
            Player = appContext.Users.Where(x => x.Id == this.PlayerId).First();
            Level = level;
            Won = false;
            TotalScore = 0;


            // select the correct number of random questions from database 
            /*quiz = new List<Question>();
            
            List<Question> tmpList = questionContext.Questions.ToList();
            quiz = tmpList;
            
            int count = tmpList.Count;
            Random rnd = new Random();
            for (int i = 0; i < Level; i++)
            {
                int index = rnd.Next() % count;
                Question tmp = tmpList[index];
                tmpList[index] = tmpList[i];
                tmpList[i] = tmp;
                count--;              
            }

            quiz.AddRange(tmpList.Take(Level));*/
        }

        public int Id { get; set; }

        [Display(Name = "Level", ResourceType = typeof(Resources.HomeTexts))]
        public int Level { get; set; } // determins the number of questions
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ApplicationUser Player { get; set; }

        [Display(Name = "Score", ResourceType = typeof(Resources.HomeTexts))]
        public int TotalScore { get; set; }
        public bool Won { get; set; }

        public string PlayerId { get; set; }

        //public List<Question> quiz { get; set; }

    }
}