using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer : MonoBehaviour
{
    public bool isCorrect = false;

    public QuizManager quizManager;
    public void Ans()
    {
        if (isCorrect)
        {
            Debug.Log("correct answer");
            quizManager.Correct();
        }
        else
        {
            Debug.Log("wrong answer");
            quizManager.Wrong();
        }
    }
}
