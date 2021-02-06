using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionMindGames 
{
    public int  questioId;
    public string questionText;
    public string questionHint;
    public string Answer;
    public int Time;  // this should be in sec;
    public string UserAnswer;
}


[System.Serializable]
public class Root
{
    public int levelno;    
    public bool haveTime;
    public int lvl_Time;
    public bool haveQuesTime;
    public List<QuestionMindGames>  Questions;
}