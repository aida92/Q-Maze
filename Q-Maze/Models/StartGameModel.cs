using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMaze.Models
{
    public class StartGameModel
    {
        //private ApplicationDbContext context = new ApplicationDbContext();

        private string Id;
        public int ResolutionX { get; set; }
        public int ResolutionY { get; set; }

        public StartGameModel(string ID, int x, int y)
        {
            this.Id = ID;
            this.ResolutionX = x;
            this.ResolutionY = y;
        }
    }
}