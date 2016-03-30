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

        public IEnumerable<ApplicationUser> AppUsers { get; set; }

        public IEnumerable<string> OrderBy
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
        }
    }
}