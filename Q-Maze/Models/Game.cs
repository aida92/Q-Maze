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
            won = false;
            totalScore = 0;

            List<Question> quiz = questionContext.Questions.ToList(); // edit this after defining the game 
        }

        public int Id { get; set; }

        [Display(Name = "Level", ResourceType = typeof(Resources.HomeTexts))]
        public int Level { get; set; } // determins the number of questions
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ApplicationUser Player { get; set; }

        [Display(Name = "Score", ResourceType = typeof(Resources.HomeTexts))]
        public int totalScore { get; set; }
        public bool won { get; set; }

        public string PlayerId { get; set; }


    }
}