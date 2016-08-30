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

        [Display(Name = "Order by")]
        public string OrderBy
        {
            get;
            set;
        }

        public IDictionary<bool, string> OrderOptions { get; set; }

        [Display(Name = "Asc/Desc")]
        public bool Descending
        {
            get;
            set;
        }

        public enum Ordering { HighScore, GamesPlayed, GamesWon, QuestionsAttempted, QuestionsAnswered, RegisterDate }; //?
        public IEnumerable<ApplicationUser> AppUsers { get; set; }

        public IEnumerable<string> OrderByList
        {
            get
            {
                List<string> list = new List<string>();
                list.Add("High score");
                list.Add("Games played");
                list.Add("Games won");
                list.Add("Questions attempted");
                list.Add("Questions answered");
                list.Add("Date registered");
                return list;
            }
        }

        public GameStatisticsModel()
        {
            AppUsers = context.Users.ToList();
            OrderBy = "None"; //default ordering: by userId
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
                    case "Date registered": AppUsers = context.Users.OrderByDescending(x => x.RegisterDate).ToList();
                        break;
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
                    case "Date registered": AppUsers = context.Users.OrderBy(x => x.RegisterDate).ToList();
                        break;
                    default: AppUsers = context.Users.OrderBy(x => x.Id);
                        break;
                }
            }

        }
    }
}