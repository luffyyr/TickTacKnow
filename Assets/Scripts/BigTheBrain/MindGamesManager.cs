using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MindGamesManager : MonoBehaviour
{
    [Header("Question List Class")]
    public List<QuestionMindGames> QA; //this will store the current level data and answer
    public List<Root> questions;    // this will store all the data from the api

    public GameObject QuestionUI;
    public GameObject NextButton;
    public GameObject SubmitButton;
    public GameObject PreviewButton;
    public GameObject SkipButton;

    public int currentQuestion = 0;
    public Text Question;
    public Text Hint; 
    public string CurrentAnswer;
    public InputField type;
    public int YourScore;
    public int QuestionNo;

    [Header("Time Components")]
    public float timeRemaining;
    public bool timerIsRunning = false;
    public Text timeText;
    public bool RoundTimer = false;
    public bool QuestionsTimer = false;

    public GameObject TimeOutUI;
    public GameObject WrongAnswerUI;
    public GameObject CorrectAnswerUI;
    public Text ScoreUI;

    public bool Pause = false;
    public GameObject LoadingScreen;  //loading screen logo
    public GameObject LoadingBar;     // loading screen bottom logo
    public GameObject MainScreen;     //our main gaming screen
    public GameObject ReviewUI;     //our main gaming screen
    public Text ReviewText; 
    public Text QuestionNoUI;

    private void Start()
    {
        // we want to check all the parametres and then fill the list with our data

        QuestionNo = 0;
        currentQuestion = 0;
        StartCoroutine(Loadingbar());
        LoadingScreen.SetActive(true);       
        MainScreen.SetActive(false);
        Invoke("LetsStart", 1f);  // make it 5f 
    }
    void Update()
    {
        ScoreUI.text = YourScore.ToString();
        QuesNo();
        if (!Pause)
        {
            CalculateTime();
        }              
    }

    IEnumerator Loadingbar()
    {
        yield return new WaitForSeconds(1f);
        LoadingBar.SetActive(true);
    }
    public void LetsStart()
    {
        LoadingScreen.SetActive(false);
        MainScreen.SetActive(true);
        YourScore = 0;
        GenerateQuestion(0);
    }

    public void GenerateQuestion(int i)
    {
        QuestionUI.SetActive(true);
        CorrectAnswerUI.SetActive(false);
        WrongAnswerUI.SetActive(false);
        TimeOutUI.SetActive(false);
        //AnswerUI.SetActive(true);

        if (QA.Count > 0)
        {
            currentQuestion = i;   // randomly picking the question from list 

            Question.text = QA[currentQuestion].questionText;  // Displaying the text in UI gameObject 
            SetHint();
            SetAnswers(); // setting option text to button and also answers 
            SetTime();
            QuestionNo += 1;
        }
        else
        {
            Debug.Log("no questions Left");
        }
    }

    public void SetHint()
    {
        //Hint.text = "Write Your Answer Here:\nAnswer format should be like this:\n" +QA[currentQuestion].questionHint;
        Hint.text = "Write Your Answer Here:\n\n\nEx of Answer Format: " + QA[currentQuestion].questionHint;
        //Debug.Log("Write Your Answer Here \n Answer Format Should be this "+Hint.text);
    }

    public void SetAnswers()
    {
        CurrentAnswer = QA[currentQuestion].Answer;
        //Debug.Log(CurrentAnswer);
    }

    public void SetTime()
    {
       
        if (!QuestionsTimer)  //checking if we want a timer in our for questions or not
        {
            Pause = true;
            timeText.gameObject.SetActive(false);
        }
        else
        {
            Pause = false;
            timeText.gameObject.SetActive(true);
            timerIsRunning = true;
            timeRemaining = QA[currentQuestion].Time;
        }
    
    }

    public void SubmitAnswer(InputField answer)
    {
        Pause = true;
        string x;
        if (answer.text =="")
        {
            x = "Skipped";
            QA[currentQuestion].UserAnswer = x;
        }
        else
        {
            x = answer.text.ToLower();
            QA[currentQuestion].UserAnswer = x;
        }
       
        
        
        if(QuestionNo == QA.Count)   //it will check if this is his last question
        {
            Debug.Log("show submit button");
            SkipButton.SetActive(false);
            NextButton.SetActive(false);
            SubmitButton.SetActive(true); 
            PreviewButton.SetActive(true); 
            // now we will show the submit button and also preview button to user so they can check the answer
        }
        else
        {
            currentQuestion++;
            type.text = "";

            GenerateQuestion(currentQuestion);
        }

        /* if(CurrentAnswer.ToLower() == x.ToLower())
         {
             CorrectAnswer();
         }
         else
         {
             WrongAnswer();
         }*/
    }

    public void CorrectAnswer()
    {
        Hint.text = "Correct Answer";
        YourScore += 10;

        // now we will inovke the Generate Question function to create new questions in UI after some time
        CorrectAnswerUI.SetActive(true);
        Invoke("GenerateQuestion", 5f);
        type.text = "";
    }

    public void WrongAnswer()
    {
        YourScore -= 5;
        if (YourScore < 0)
        {
            YourScore = 0;
        }
        Hint.text = "Wrong Answer\nCorrect Answer is: "+ CurrentAnswer;
        WrongAnswerUI.SetActive(true);
        Invoke("GenerateQuestion", 5f);
        type.text = "";
    }

    public void TimeOut() //function will be called if user time is out
    {
        QuestionUI.SetActive(false);
        YourScore -= 5;
        if (YourScore < 0)
        {
            YourScore = 0;
        }

        //AnswerUI.SetActive(false);
        // we should reset question 
        //before that we should set all components false
        //we will now show the correct answer and time Out UI
        TimeOutUI.SetActive(true);
        StartCoroutine(NextQue());
    }
    IEnumerator NextQue()
    {
        yield return new WaitForSeconds(5f);
        currentQuestion += 1;
        GenerateQuestion(currentQuestion);
    }
    void CalculateTime()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                TimeOut();
            }
        }
        else
        {
            timeText.gameObject.SetActive(false);
        }
    } //this function calulating remaining time every frame
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void Submit()  // this will submit all our answer to check them with the server 
    {
        float correctAnswer = 0;
       foreach(QuestionMindGames x in QA)
       {
           if(x.Answer.ToLower() == x.UserAnswer.ToLower())
           {
                correctAnswer += 1;
           }
       }
        Debug.Log(correctAnswer);
        Debug.Log(QA.Count);

        float percentage = (correctAnswer/ QA.Count) * 100;
        Debug.Log(percentage);
    }
    public void ReviewBtn() // this button will show user the answer of the question he answered
    {
        ReviewUI.SetActive(true);
        QuestionUI.SetActive(false);
        ReviewText.text = "";
        foreach(QuestionMindGames x in QA)
        {
            string s;
            string Qno = x.questioId.ToString();
            string Question = x.questionText.ToString();
            string Answered = x.UserAnswer.ToString();
            s = "\n" + Qno + ". " + Question + "\n" +"U Answered :  " + Answered + "\n";
            ReviewText.text += s;
        }
    }

    public void QuesNo() 
    {

        QuestionNoUI.text = QuestionNo.ToString() + "/" + QA.Count;
    }

    public void FillListData(int level)
    {
        int x = 0;   
        foreach(Root i in questions)
        {
            if(x == level-1)
            {
               // foreach(QuestionMindGames y in )
            }
            x++;
        }
       
    }
}

