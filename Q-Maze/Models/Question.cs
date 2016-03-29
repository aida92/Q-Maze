using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMaze.Models
{
    public class Question
    {
        public long ID { get; set; }
        public string Text { get; set; }
        //separator between answers is a newline, or semicolon???
        public string Answers { get; set; }
        public string CorrectAnswer { get; set; }
        public short Points { get; set; }
    }
}