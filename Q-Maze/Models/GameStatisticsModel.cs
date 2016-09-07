using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QMaze.Models
{
    public class GameStatisticsModel
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        [Display(Name = "OrderBy", ResourceType = typeof(Resources.HomeTexts))]
        public string OrderBy
        {
            get;
            set;
        }

        public IDictionary<bool, string> OrderOptions { get; set; }

        [Display(Name = "AscDesc", ResourceType = typeof(Resources.HomeTexts))]
        public bool Descending
        {
            get;
            set;
        }

        //public enum Ordering { HighScore, GamesPlayed, GamesWon, QuestionsAttempted, QuestionsAnswered, RegisterDate }; //?
        public IEnumerable<ApplicationUser> AppUsers { get; set; }

        public IEnumerable<string> OrderByList
        {
            get
            {
                List<string> list = new List<string>();
                list.Add(QMaze.Resources.HomeTexts.Highscore);
                list.Add(QMaze.Resources.HomeTexts.GamesPlayed);
                list.Add(QMaze.Resources.HomeTexts.GamesWon);
                list.Add(QMaze.Resources.HomeTexts.QuestionsAttempted);
                list.Add(QMaze.Resources.HomeTexts.QuestionsAnswered);
                //list.Add(QMaze.Resources.HomeTexts.RegDate);  
                // TO DO : figure out why its sorting weird by date :)
                return list;
            }
        }

        public GameStatisticsModel()
        {
            AppUsers = context.Users.ToList();
            OrderBy = "none"; //default ordering: by id
            Descending = true;
            OrderOptions = new Dictionary<bool, string>();
            OrderOptions.Add(false, "ascending");
            OrderOptions.Add(true, "descending");
        }

        public void RefreshList()
        {
            if (Descending)
            {
                switch (OrderBy)
                {
                    case "High score": AppUsers = context.Users.OrderByDescending(x => x.HighScore).ToList();
                        break;
                    case "Games played": AppUsers = context.Users.OrderByDescending(x => x.GamesPlayed).ToList();
                        break;
                    case "Games won": AppUsers = context.Users.OrderByDescending(x => x.GamesWon).ToList();
                        break;
                    case "Questions attempted": AppUsers = context.Users.OrderByDescending(x => x.QuestionsTotal).ToList();
                        break;
                    case "Questions answered": AppUsers = context.Users.OrderByDescending(x => x.QuestionsCorrect).ToList();
                        break;
                    //case "Date registered": AppUsers = context.Users.OrderByDescending(x => x.RegisterDate).ToList();
                    //    break;
                    default: AppUsers = context.Users.OrderByDescending(x => x.Id);
                        break;
                }
            }
            else // ascending
            {
                switch (OrderBy)
                {
                    case "High score": AppUsers = context.Users.OrderBy(x => x.HighScore);
                        break;
                    case "Games played": AppUsers = context.Users.OrderBy(x => x.GamesPlayed).ToList();
                        break;
                    case "Games won": AppUsers = context.Users.OrderBy(x => x.GamesWon).ToList();
                        break;
                    case "Questions attempted": AppUsers = context.Users.OrderBy(x => x.QuestionsTotal).ToList();
                        break;
                    case "Questions answered": AppUsers = context.Users.OrderBy(x => x.QuestionsCorrect).ToList();
                        break;
                    //case "Date registered": AppUsers = context.Users.OrderBy(x => x.RegisterDate).ToList();
                    //    break;
                    default: AppUsers = context.Users.OrderBy(x => x.Id);
                        break;
                }
            }

        }
    }
}