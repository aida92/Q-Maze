using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace QMaze.Models
{
    public class SetupGameModel
    {
        //private ApplicationDbContext context = new ApplicationDbContext();
        public int ResolutionX { get; set; }
        public int ResolutionY { get; set; }

        [Display(Name = "Level", ResourceType = typeof(Resources.HomeTexts))]
        public int SelectedLevel { get; set; }

        public IDictionary<int, string> Levels { get; set; }

        public SetupGameModel()
        {
            ResolutionX = 500;
            ResolutionY = 500;
            SelectedLevel = 1;

            Levels = new Dictionary<int, string>();
            //list.Add(QMaze.Resources.HomeTexts.Intermmediate);
            //list.Add(QMaze.Resources.HomeTexts.Hard);
            //list.Add(QMaze.Resources.HomeTexts.Insane);
            //list.Add(QMaze.Resources.HomeTexts.LevelMATF);
            Levels.Add(1, QMaze.Resources.HomeTexts.Easy);
            Levels.Add(2, "Intermmediate");
            Levels.Add(3, "Hard");
            Levels.Add(4, "Insane");
            Levels.Add(5, "LevelMATF");
        }

    }
}