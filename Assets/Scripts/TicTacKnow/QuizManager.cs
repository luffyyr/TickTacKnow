using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class QuizManager : MonoBehaviour
{
    public List<QAClass> QA;    // list to store all the questions/answers we have 
    public GameObject[] options;            // this gameobject store the UI buttton for option

    public int currentQuestion;            // this will store the current question from the list

    public Text QuestionTxt;            // we are accessing the text component from Question object in UI

    public int ButtonPressed; // it will store the value what button is pressed in ticktac

    [Header("AudioClips")]
    public AudioClip wonClip;
    public AudioClip lostClip;
    public AudioClip correctClip;
    public AudioClip WrongClip;
    public AudioClip HoverClip;
    public AudioSource source;

    public TIcTAc TicTacObj;

    public GameObject Clock;



    void Start()
    {
        ButtonPressed = -99;  // just storing wrong value in it , we cant store zero cause there is 0button to 8button
        //GenerateQuestion();    
    }

    public void GenerateQuestion(int btnNo)
    {
        ButtonPressed = btnNo;
        //Debug.Log("button pressed is " + ButtonPressed);
        if (QA.Count > 0)
        {
          
            currentQuestion = Random.Range(0, QA.Count);   // randomly picking the question from list 

            QuestionTxt.text = QA[currentQuestion].questionText;  // Displaying the text in UI gameObject 
            SetAnswers(); // setting option text to button and also answers their Answer Button Gameobject i.e bool
            //Clock.SetActive(true);
        }
        else
        {
            Debug.Log("no questions");
            // now we want to restart the level or exit the scene
        }

    }

    void SetAnswers()
    {        
        for(int i = 0; i< options.Length; i++)
        {
            options[i].GetComponent<Answer>().isCorrect = false;   // making sure that answer object of buttons contains false by default

            options[i].transform.GetChild(1).GetComponent<Text>().text = QA[currentQuestion].options[i].optionText;
            
            if(QA[currentQuestion].options[i].optionID == QA[currentQuestion].answer)
            {
                options[i].GetComponent<Answer>().isCorrect = true;
            } 
        }
    }

    public void Correct()
    {
        source.PlayOneShot(correctClip,.5f);
        QA.RemoveAt(currentQuestion);            // removing the question from list so it cant be show again
        TicTacObj.UserIsCorrect(ButtonPressed);
        //GenerateQuestion();              // we have given the answers so we will generate a new question
        ButtonPressed = -99;
        Clock = GameObject.Find("ClockCanvas(Clone)");
        Destroy(Clock);
    }

    public void Wrong()
    {
        source.PlayOneShot(WrongClip,.5f);
        QA.RemoveAt(currentQuestion);            // removing the question from list so it cant be show again
        TicTacObj.UserIsWrong(ButtonPressed);
        //GenerateQuestion();              // we have given the answers so we will generate a new question
        ButtonPressed = -99;
        Clock = GameObject.Find("ClockCanvas(Clone)");
        Destroy(Clock);
    }

    public void WonClip() //play right answer clip
    {
        //AudioSource.PlayClipAtPoint(wonClip, transform.position);
        source.PlayOneShot(wonClip,.5f);
    }

    public void LostClip()  //play the wrong aswer clip
    {
        //AudioSource.PlayClipAtPoint(lostClip, transform.position);
        source.PlayOneShot(lostClip,.5f);
    }
    public void HoverOverSound() //play hoverOver clip
    {
        source.PlayOneShot(HoverClip,0.01f);
    }
}
