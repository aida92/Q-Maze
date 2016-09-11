using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QMaze.Models
{
    public class GameModel
    {
        private ApplicationDbContext appContext = new ApplicationDbContext();

        public GameModel()
        {
            Level = 1;
            Won = false;
            TotalScore = 0;
        }

        public GameModel(int level)
        {
            Level = level;
            Won = false;
            TotalScore = 0;
        }

        public int Id { get; set; }

        [Display(Name = "Level", ResourceType = typeof(Resources.HomeTexts))]
        public int Level { get; set; } // determins the number of questions

        public int TotalScore { get; set; }
        public bool Won { get; set; }

        public int TotalQuestions { get; set; }
        public int CorrectQuestions { get; set; }
        public virtual void SaveScore() {}
    }
}