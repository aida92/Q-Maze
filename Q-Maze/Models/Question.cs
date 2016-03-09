using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMaze.Models
{
    public class Question
    {
        public int ID { get; set; }
        public string Text { get; set; }
        //separator between answers is a newline
        public string Answers { get; set; }
        public string CorrectAnswer { get; set; }
        public int Points { get; set; }
    }
}