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

        //public IEnumerable<SelectListItem> Levels { get; set; }
        public List<SelectListItem> Levels;

        public SetupGameModel()
        {
            ResolutionX = 500;
            ResolutionY = 500;
            SelectedLevel = 2;

            Levels = new List<SelectListItem>();
            Levels.Add(new SelectListItem { Text = QMaze.Resources.HomeTexts.Easy, Value = "1", Selected = true });
            Levels.Add(new SelectListItem { Text = "Intermediate", Value = "2" });
            Levels.Add(new SelectListItem { Text = "Hard", Value = "3" });
            Levels.Add(new SelectListItem { Text = "Insane", Value = "4" });
            Levels.Add(new SelectListItem { Text = "MATF", Value = "5" });
        }

    }
}