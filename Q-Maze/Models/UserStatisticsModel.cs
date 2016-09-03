using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QMaze.Models
{
   
    public class UserStatisticsModel
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        private string Id;

        public UserStatisticsModel(string Id)
        {
            this.Id = Id;
        }

        public ApplicationUser AppUser
        {
            get
            {
                return context.Users.Where(x => x.Id == this.Id).First();
            }
        }

        [Display(Name="Percentage won")]
        public double SuccessRate
        {
            get
            {
                if (AppUser.GamesPlayed != 0)
                    return AppUser.GamesWon * 100.0 / AppUser.GamesPlayed;
                else
                    return 0;
            }
        }

        [Display(Name = "Rank", ResourceType = typeof(Resources.HomeTexts))]
        public int Ranking
        {
            get
            {
                return context.Users.Where(x => x.HighScore > AppUser.HighScore).Count() + 1;
            }
        }


        public int MemberFor
        {
            get
            {
                return (DateTime.Today - AppUser.RegisterDate).Days;
            }
        }

    }
}