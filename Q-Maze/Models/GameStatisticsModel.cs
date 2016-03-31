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

        public string OrderBy
        {
            get;
            set;
        }

        public enum Ordering {HighScore, GamesPlayed, GamesWon, QuestionsAttempted, QuestionsAnswered, RegisterDate}; //?
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
            OrderBy = "None";
        }

        public void RefreshList()
        {
            switch (OrderBy)
            {
                case "High score":          AppUsers = context.Users.OrderByDescending(x => x.HighScore).ToList();
                    break;
                case "Games played":        AppUsers = context.Users.OrderByDescending(x => x.GamesPlayed).ToList();
                    break;
                case "Games won":           AppUsers = context.Users.OrderByDescending(x => x.GamesWon).ToList();
                    break;
                case "Questions attempted": AppUsers = context.Users.OrderByDescending(x => x.QuestionsTotal).ToList();
                    break;
                case "Questions answered":  AppUsers = context.Users.OrderByDescending(x => x.QuestionsCorrect).ToList();
                    break;
                case "Date registered":     AppUsers = context.Users.OrderByDescending(x => x.RegisterDate).ToList();
                    break;
                default:                    AppUsers = context.Users.OrderByDescending(x => x.Id);
                    break;
            }
        }
    }
}