using System.Collections.Generic;
using System.Linq;
using System;

public class Question {
    public string question;
    public List<string> answers;
    public string correct;

    public static int D_QUESTION = 0;
    public static int D_ANSWERS = 1;
    public static int D_CORRECT = 2;
    
    public Question(string data) {
        /** data FORMAT => QUESTION,ANSWER_1|ANSWER_2|ANSWER_3|ANSWER_N,ANSWER */

        var arr = data.Split(',');
        if( arr.Length > 3) {
            throw new Exception("Question not formatted well (too many commas)");
        }
        question = arr[D_QUESTION];
        correct = arr[D_CORRECT];
        answers = arr[D_ANSWERS].Split('|').ToList();

        if (!answers.Contains(correct.TrimEnd().TrimStart())) {
            throw new Exception("Question not formatted well (correct answer not found in answers)");
        }
    }



    override public string ToString() {
        return question + " => " + correct;
    }

}
