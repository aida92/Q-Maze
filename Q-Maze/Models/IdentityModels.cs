using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;

namespace QMaze.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Display(Name = "HighScore", ResourceType=typeof(Resources.HomeTexts))]
        public virtual int HighScore { get; set; }
        [Display(Name = "GamesPlayed", ResourceType = typeof(Resources.HomeTexts))]
        public virtual int GamesPlayed { get; set; }
        [Display(Name = "GamesWon", ResourceType = typeof(Resources.HomeTexts))]
        public virtual int GamesWon { get; set; }
        [Display(Name = "QuestionsAttempted", ResourceType = typeof(Resources.HomeTexts))]
        public virtual int QuestionsTotal { get; set; } //total number of questions attempted in all games
        [Display(Name = "QuestionsAnswered", ResourceType = typeof(Resources.HomeTexts))]
        public virtual int QuestionsCorrect { get; set; } //total number of correct answers in all games
        
        public virtual DateTime RegisterDate { get; set; }

        [Display(Name = "HighScore", ResourceType = typeof(Resources.HomeTexts))]
        public virtual string FormattedRegDate 
        {
            get
            {
                return RegisterDate.ToShortDateString(); // formatted according to chosen language
            }
        }

        //public virtual string Token { get; set; } // for SfS2x DB integration, see: http://smartfoxserver.com/blog/integrate-a-website-login-with-smartfoxserver/
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}