using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MindGamesManager : MonoBehaviour
{
    public static MindGamesManager Instance;

    [Header("Question List Class")]
    public List<QuestionMindGames> QA;   //this will store the current level data and answer
    public List<Root> questions;        // this will store all the data from the Api

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
    public int CurrentLevel;
    public int TotalQuestions;


    [Header("Time Components")]
    public float timeRemaining;
    public bool timerIsRunning = false;
    public Text timeText;
    public bool RoundTimer = false;   // this will check if there is roudn timer
    public bool QuestionsTimer = false;  // this will check if we have any question timer

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
    public GameObject YouWonUI;
    public GameObject TotalQuestionUI;
    public GameObject LevelUI;
    public Text LevelNoUI;
    public GameObject GoToQuestion;  // this will contain the UI Input field which can be used to go to specific Question to change its answer
    public GameObject GoToQuestionBtn;
    public GameObject ChangeAnswerBtn;
    public int GotoQuesNo;
    public InputField UserAnswered;              //this text show what answer user wrote 
    public GameObject Sorry_ULost;

    public List<string> Dropdown_QuestionNo;
    public Dropdown Dropdown_List;
    public bool DropDownList_ListAdded = false;
    public GameObject DropdownUI;

    public GameObject Clock;
    public GameObject LevelName;
    public Text LevelName_Txt;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        // we want to check all the parametres and then fill the list with our data
        CurrentLevel = 1;
        TotalQuestions = 0;        
        QuestionNo = 0;
        currentQuestion = 0;
        FillListData(CurrentLevel); // we will start game with level 1(i.e parameter) and then we will increase it manually throught game 
        StartCoroutine(Loadingbar());
        LoadingScreen.SetActive(true);       
        MainScreen.SetActive(false);
        Invoke("LetsStart", 5f);  // make it 5f 
        
    }
    void Update()
    {
        ScoreUI.text = YourScore.ToString();
        LevelNoUI.text = CurrentLevel.ToString();

        QuesNo();
        if (!Pause)
        {
            CalculateTime();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddList();
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
        TotalQuestions = QA.Count;                         // assigning the totalquestion value here
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
        Hint.text = "Write Your Answer Here:\n\n\nEx: " + QA[currentQuestion].questionHint;
        //Hint.text = "Write Your Answer Here:\n\n\nEx of Answer Format: " + QA[currentQuestion].questionHint;
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

    public void SetUserAnswer()
    {
        UserAnswered.text = "";
        UserAnswered.text = QA[GotoQuesNo].UserAnswer;
        Debug.Log(UserAnswered.text);
    }

    public void SubmitAnswer(InputField answer) // this function is for next question not to sumbit all the Answers 
    {
        Pause = true;
        string x;
        if (answer.text =="")
        {
            x = "";
            QA[currentQuestion].UserAnswer = x;
        }
        else
        {
            x = answer.text.ToLower();
            QA[currentQuestion].UserAnswer = x;
        }
       
               
        if(QuestionNo == QA.Count)   //it will check if this is his last question
        {
            Debug.Log("show submit button / No Questions are Left");
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
    }

    public void CorrectAnswer()
    {
        Hint.text = "Correct Answer";
        YourScore += 10;

        // now we will inovke the Generate Question function to create new questions in UI after some time
        CorrectAnswerUI.SetActive(true);
        Invoke("GenerateQuestion", 5f);
        type.text = "";
    } // we will delete this later cause we are not using it 

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
        Debug.Log(percentage + " %");

        // now we have to show you passed or failed 
        //here we weill move to lvl 2
        if(percentage >= 50)
        {
            QuestionUI.SetActive(false);
            ReviewUI.SetActive(false);
            SubmitButton.SetActive(false);
            PreviewButton.SetActive(false);
            TotalQuestionUI.SetActive(false);
            LevelUI.SetActive(false);
            GoToQuestionBtn.SetActive(false);
            GoToQuestion.SetActive(false);

            //make button and total question UI disable
            CurrentLevel++;
            
            QA.RemoveAll(QA.Contains); // Remove all the elements in the list
            Dropdown_QuestionNo.RemoveAll(Dropdown_QuestionNo.Contains);
            Dropdown_List.ClearOptions();

            FillListData(CurrentLevel);
            if (CurrentLevel > questions.Count)          // checking how many level are there , if there are more levels to show then we will start them else end this game
            {
                Debug.Log("Game Over");
            }
            else
            {
                YouWonUI.SetActive(true);
                DropdownUI.SetActive(false);
                StartCoroutine(ShowLevel());
                DropDownList_ListAdded = false;
                Invoke("NextLevel", 5f);
            }
        }
        else
        {
            QuestionUI.SetActive(false);
            ReviewUI.SetActive(false);
            SubmitButton.SetActive(false);
            PreviewButton.SetActive(false);
            TotalQuestionUI.SetActive(false);
            LevelUI.SetActive(false);
            GoToQuestionBtn.SetActive(false);
            GoToQuestion.SetActive(false);
            DropdownUI.SetActive(false);
            Debug.Log("you failed");
            Sorry_ULost.SetActive(true);
            Invoke("RestartGame", 5f);
        }
    }

    public void NextLevel()
    {
        //we have to disable  the review pannel
        // now we will enable the question UI and everthing will go as before
        YouWonUI.SetActive(false);
        LevelName.SetActive(false);
        QuestionUI.SetActive(true);
        NextButton.SetActive(true);
        TotalQuestionUI.SetActive(true);
        LevelUI.SetActive(true);

        //SkipButton.SetActive(true);
        QuestionNo = 0;
        GenerateQuestion(0);
    }

    public void ReviewBtn() // this button will show user the answer of the question he answered
    {
        TotalQuestionUI.SetActive(false);
        //GoToQuestionBtn.SetActive(true);
        //GoToQuestion.SetActive(true);
        //GoToQuestion.SetActive(true);
        AddList();
        //SubmitReviewAnswerBtn.SetActive(true);
        ReviewUI.SetActive(true);
        QuestionUI.SetActive(false);
        PreviewButton.SetActive(false);   ////////////////////////////////////////////
        ReviewText.text = "";
        foreach(QuestionMindGames x in QA)
        {
            string s;
            string Qno = x.questioId.ToString();
            string Question = x.questionText.ToString();
            string Answered = x.UserAnswer.ToString();
            s = "\n"+"<color=red>" + Qno + ".</color> " + Question + "\n" + "<color=green>Answer Submitted : </color> " + Answered + "\n";
            ReviewText.text += s;
        }

        // we want to disable QuestionNo UI in bottom 
        // enable CheckQuestion UI so user can enter and go back to that question 
        //  enable submit button to submit this question answer and after that we will come back to review scene
    }

    public void QuesNo() 
    {
        QuestionNoUI.text = QuestionNo.ToString() + "/" + TotalQuestions;
    }

    public void FillListData(int level)
    {
        int x = 0;   // to check what Level data we need
        if (level > questions.Count)
        {
            Debug.Log("Game is Over");
        }
        else
        {
            foreach (Root i in questions)
            {
                if (level - 1 == x)
                {
                    // here we will take data of this root class like level no , have timer or not, have 
                    int z = 0;

                    // we have to make aour list null

                    foreach (QuestionMindGames p in i.Questions)
                    {
                        QA.Add(p);
                    }
                    foreach (QuestionMindGames y in i.Questions)
                    {
                        QA[z].questioId = y.questioId;
                        QA[z].questionHint = y.questionHint;
                        QA[z].questionText = y.questionText;
                        QA[z].Answer = y.Answer;
                        QA[z].Time = y.Time;
                        z++;
                    }
                    break;
                }
                x++;
            }
        }
       
    }

    public void GoToQuestionNo(InputField no)   // function is used to visit the speicfic question so we can change its answer
    {
        int r;
        r = int.Parse(no.text);
        GotoQuesNo = r-1;         //subtracting 1 cause index no start from zero

        QuestionUI.SetActive(true);
        GenerateQuestion(GotoQuesNo);
        SetUserAnswer();

        ChangeAnswerBtn.SetActive(true);
        GoToQuestionBtn.SetActive(false);
        GoToQuestion.SetActive(false);
        ReviewUI.SetActive(false);
        PreviewButton.SetActive(false);
        SubmitButton.SetActive(false);
        NextButton.SetActive(false);
    }

    public void ChangeAnswer(InputField answer)  // this function wil change answer and go back to review page 
    {
        string x;
        if (answer.text == "")
        {
            x = "";
            QA[currentQuestion].UserAnswer = x;
        }
        else
        {
            x = answer.text.ToLower();
            QA[currentQuestion].UserAnswer = x;
        }

        type.text = "";
        ReviewUI.SetActive(true);
        ReviewBtn();
        ChangeAnswerBtn.SetActive(false);
        SkipButton.SetActive(false);
        NextButton.SetActive(false);
        SubmitButton.SetActive(true);
        PreviewButton.SetActive(false);
    }

    public void RestartGame()
    {
        Loader.Load(Loader.Scene.BigBrain);
    }

    // we need to increase list size according to questions in list
    // now we will assign list to dropdown 
    // enable this dropdown object 

    public void DropdownList(int index)
    {
        int i;
        i = index;
        Debug.Log(i);

        GotoQuesNo = i;         //subtracting 1 cause index no start from zero

        QuestionUI.SetActive(true);
        GenerateQuestion(GotoQuesNo);
        SetUserAnswer();

        ChangeAnswerBtn.SetActive(true);
        //GoToQuestionBtn.SetActive(false);
        //GoToQuestion.SetActive(false);
        ReviewUI.SetActive(false);
        PreviewButton.SetActive(false);
        SubmitButton.SetActive(false);
        NextButton.SetActive(false);
    }

    public void AddList()
    {
        int no = 1;
        if(DropDownList_ListAdded == false )
        {
            for (int i = 0; i < QA.Count; i++)
            {
                Dropdown_QuestionNo.Add("Question No. " + no.ToString());
                no++;
            }
            Dropdown_List.AddOptions(Dropdown_QuestionNo); // adding list to dropdown
            DropDownList_ListAdded = true;
        }
        DropdownUI.SetActive(true);
    }

    IEnumerator LevelAnimation()
    {

        // here we will start animating level 
        yield return new WaitForSeconds(3f);
        // Now we will set active the level UI 

    }

    public void TimeUp()  // function is used to show that time is up
    {
        Debug.Log("Game Over");
        // we will automatically show the result i.e no review panel
    }

    IEnumerator ShowLevel()
    {
        LevelName_Txt.text = CurrentLevel.ToString();
        yield return new WaitForSeconds(2f);
        YouWonUI.SetActive(false);
        LevelName.SetActive(true);
    }
}

 