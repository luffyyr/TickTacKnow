using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TIcTAc : MonoBehaviour
{
    public static TIcTAc Instance;

    public Sprite[] icon;    //sprite at 0 is X and at 1 is O
    public Sprite questionIcon; //it contain question sprite
    public int turnCount;  // it counts the total tuended we played
    public int whoseTurn;  // 0 = user and 1 = pc
    public Button[] button;
    public int[] MarkedPlaces; // this will store marked places in 3*3 tick tac game 
    public GameObject CommentBox;    //this show the result of the game at bottom pannel
    public GameObject firstComment;
        
    public QuizManager quiz_Obj;
    public GameObject TicTacBorder;
    public GameObject questionUI;
    public GameObject OptionsUI;
    public GameObject[] optionslist;

    public GameObject YouWon;
    public GameObject YouLost;
    public GameObject LevelComplete;

    public GameObject clockPrefab;
    public Transform ClockPosition;


    public bool CanWe = true;


    void Awake()
    {
        Instance = this;
    }

    public void GetReady()  // preparing all the assets and gameobject components
    {        
        firstComment.SetActive(true);   // setting all the UI component disabled
        questionUI.SetActive(false);
        OptionsUI.SetActive(false);
        YouLost.SetActive(false);
        LevelComplete.SetActive(false);
        YouWon.SetActive(false);
        TicTacBorder.SetActive(true);

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
            //TIcTacKnow_GameManager.Instance.LostReset();
            GenerateRandom();
        } //restart level

        CheckDraw();
    }

    public void ButtonClicked(int BtnNum)
    {
        if (CanWe)
        {
            firstComment.SetActive(false);
            button[BtnNum].interactable = false;                         //*************************
                                                                         // we will swap position of optiosns here

            // from here we will start the question 
            button[BtnNum].image.sprite = questionIcon;

            Instantiate(clockPrefab, ClockPosition.transform.position, ClockPosition.rotation);

            // here we will check the question and get result i.e true or false
            StartKBC(BtnNum);
            CanWe = false;
        }
       
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
                 //CommentBox.SetActive(true);
                 //CommentBox.GetComponent<Text>().text = "You Won!";
                    firstComment.SetActive(false);
                    DisableOn();
                    LevelComplete.SetActive(true);
                    quiz_Obj.WonClip();
                    TIcTacKnow_GameManager.Instance.WinReset();
                }
                if (solutions[i] == 3)
                {
                 //CommentBox.SetActive(true);
                 //CommentBox.GetComponent<Text>().text = "You Lost!";
                    firstComment.SetActive(false);
                    DisableOn();
                    YouLost.SetActive(true);
                    quiz_Obj.LostClip();
                    TIcTacKnow_GameManager.Instance.LostReset();
                    return;
                }
        }  
    } // contains the results for tic tac winner

    

    void CheckDraw()
    {
        if (turnCount == 9 && !CommentBox.activeSelf)
        {
            //CommentBox.SetActive(true);
            //CommentBox.GetComponent<Text>().text = "Draw";
            firstComment.SetActive(false);
            YouLost.SetActive(true);
            TIcTacKnow_GameManager.Instance.LostReset();
        }
    }

    // this fucntion will start when we click tictac button
    void StartKBC(int btnNo)   // we will enable Q/A components and pass new questions from the list
    {
        questionUI.SetActive(true);
        OptionsUI.SetActive(true);

        questionUI.transform.GetChild(0).GetComponent<Text>().enabled = true;
        foreach (GameObject x in optionslist)
        {
            x.transform.GetChild(1).GetComponent<Text>().enabled = true;
        }

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

        questionUI.SetActive(false);
        OptionsUI.SetActive(false);
        firstComment.SetActive(true);
        TIcTacKnow_GameManager.Instance.TotalScore += 10 * TIcTacKnow_GameManager.Instance.LevelNo;
        
        if (turnCount > 2)
        {
            WinnerCheck();
        }

        /*questionUI.transform.GetChild(0).GetComponent<Text>().enabled = false;
        foreach(GameObject x in optionslist)
        {
            x.transform.GetChild(1).GetComponent<Text>().enabled = false;
        }*/

        CanWe = true;
    }

    public void UserIsWrong(int btn)
    {
        button[btn].image.sprite = icon[1];
        button[btn].interactable = false;
        MarkedPlaces[btn] =  1; 
        turnCount++;

        questionUI.SetActive(false);
        OptionsUI.SetActive(false);
        firstComment.SetActive(true);

        if (turnCount > 2)
        {
            WinnerCheck();
        }


        CanWe = true;
    }

    private void DisableOn()
    {
        firstComment.SetActive(false);
        OptionsUI.SetActive(false);
        questionUI.SetActive(false);
        TicTacBorder.SetActive(false);
    }

    void fn()
    {
        for (int i = 0; i < 4; i++)
        {
            int y = Random.Range(0, optionslist.Length);
            Debug.Log("this is y" + y);
            if (i != y)
            {
                RectTransform lol, lol2, tmp;
                lol = optionslist[i].GetComponent<RectTransform>();
                lol2 = optionslist[y].GetComponent<RectTransform>();
                Debug.Log(lol.localPosition);
                Debug.Log(lol2.localPosition);
                tmp = lol;
                lol.localPosition = lol2.localPosition;
                lol2.localPosition = tmp.localPosition;
                //optionslist[i].transform.position = optionslist[y].transform.position;
                //optionslist[y].transform.position = lol;
            }
        }
    }

            int Lenght = 4;
            List<int> list = new List<int>();
            int[] h = new int[4];
            int i = 0;
            void GenerateRandom()
            {
                for (int j = 0; j < Lenght; j++)
                {
                    int Rand = Random.Range(0, 4);
                    while (list.Contains(Rand))
                    {
                        Rand = Random.Range(0, 4);
                    }
                    list.Add(Rand);
                    //print(list[j]);
                    h[j] = list[j];
                    print(h[i]);
                    i++;
                }
                //print(h[0]);
                //print(h[1]);
                //print(h[2]);
                //print(h[3]);
            }
}
