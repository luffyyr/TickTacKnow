using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TIcTAc : MonoBehaviour
{
    public Sprite[] icon;    //sprite at 0 is X and at 1 is O
    public Sprite questionIcon; //it contain question sprite
    public int turnCount;  // it counts the total tuended we played
    public int whoseTurn;  // 0 = user and 1 = pc
    public Button[] button;
    public int[] MarkedPlaces; // this will store marked places in 3*3 tick tac game 
    public GameObject CommentBox;    //this show the result of the game at bottom pannel
    public GameObject firstComment;
        
    public QuizManager quiz_Obj;
    public GameObject questionUI;
    public GameObject OptionsUI;



    void Start()
    {
        GetReady();
    }

    void GetReady()  // preparing all the assets and gameobject components
    {        
        firstComment.SetActive(true);   // setting all the UI component disabled
        questionUI.SetActive(false);
        OptionsUI.SetActive(false);

        whoseTurn = 0;     

        for (int i = 0; i < button.Length; i++)
        {
            button[i].interactable = true;
            button[i].GetComponent<Image>();         // we cant null it at start else we know what will happen
        }

        for(int i = 0; i < MarkedPlaces.Length; i++)
        {
            MarkedPlaces[i] = 100;
        }
    } //contain all the assets we need 

    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            StartCoroutine(RestartGame());
        } //restart level

        CheckDraw();
    }

    public void ButtonClicked(int BtnNum)
    {
        firstComment.SetActive(false);

        // from here we will start the question 
        button[BtnNum].image.sprite = questionIcon;

        // here we will check the question and get result i.e true or false
        StartKBC(BtnNum);

        //ShowResult(BtnNum);
    }
    public void WinnerCheck()
    {
        int s1 = MarkedPlaces[0] + MarkedPlaces[1] + MarkedPlaces[2];
        int s2 = MarkedPlaces[3] + MarkedPlaces[4] + MarkedPlaces[5];
        int s3 = MarkedPlaces[6] + MarkedPlaces[7] + MarkedPlaces[8];
        int s4 = MarkedPlaces[0] + MarkedPlaces[3] + MarkedPlaces[6];
        int s5 = MarkedPlaces[1] + MarkedPlaces[4] + MarkedPlaces[7];
        int s6 = MarkedPlaces[2] + MarkedPlaces[5] + MarkedPlaces[8];
        int s7 = MarkedPlaces[0] + MarkedPlaces[4] + MarkedPlaces[8];
        int s8 = MarkedPlaces[2] + MarkedPlaces[4] + MarkedPlaces[6];

        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };
        for(int i = 0; i< solutions.Length; i++)
        {
                if(solutions[i] == 0)
                {
                    CommentBox.SetActive(true);
                    CommentBox.GetComponent<Text>().text = "You Won!";
                    quiz_Obj.WonClip();
                    StartCoroutine(RestartGame());
                }
                if (solutions[i] == 3)
                {
                    CommentBox.SetActive(true);
                    CommentBox.GetComponent<Text>().text = "You Lost!";
                    quiz_Obj.LostClip();
                    StartCoroutine(RestartGame());
                    return;
                }
        }  
    } // contains the results for tic tac winner

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarting the current scene
    }

    void CheckDraw()
    {
        if (turnCount == 9 && !CommentBox.activeSelf)
        {
            CommentBox.SetActive(true);
            CommentBox.GetComponent<Text>().text = "Draw";
            StartCoroutine(RestartGame());
        }
    }

    // this fucntion will start when we click tictac button
    void StartKBC(int btnNo)   // we will enable Q/A components and pass new questions from the list
    {
        questionUI.SetActive(true);
        OptionsUI.SetActive(true);

        quiz_Obj.GenerateQuestion(btnNo);
    }

    public void UserIsCorrect(int btn)
    {
        //Debug.Log("this is btn" + btn);
        button[btn].image.sprite = icon[0];
        button[btn].interactable = false;
        MarkedPlaces[btn] = 0; 
        turnCount++;
        //Debug.Log("" + turnCount);
        if (turnCount > 2)
        {
            WinnerCheck();
        }
        questionUI.SetActive(false);
        OptionsUI.SetActive(false);
        firstComment.SetActive(true);  
    }

    public void UserIsWrong(int btn)
    {
        button[btn].image.sprite = icon[1];
        button[btn].interactable = false;
        MarkedPlaces[btn] =  1; 
        turnCount++;
        Debug.Log("" + turnCount);
        if (turnCount > 2)
        {
            WinnerCheck();
        }
        questionUI.SetActive(false);
        OptionsUI.SetActive(false);
        firstComment.SetActive(true);  
    }
    
}
