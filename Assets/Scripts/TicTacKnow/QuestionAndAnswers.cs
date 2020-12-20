using System.Collections.Generic;
[System.Serializable]
public class QuestionAndAnswers
{
    public int QuestionID;
    public string Question;  
    public int CorrectAnsmwerID;
    public List<AnswerOptions> Opts;
}
[System.Serializable]
public class AnswerOptions
{
    public string AnswerText;
    public int AnswerID;
}
    