using System;
using System.Collections.Generic;


[Serializable]
public class QAClass
{
    public int questionId;
    public string questionText;
    public int answer;
    public List<Option> options;
}

[Serializable]
public class Option
{
    public string optionText;
    public int optionID;    
} 