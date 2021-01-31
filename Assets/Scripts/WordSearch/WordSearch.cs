using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordSearch : MonoBehaviour
{
    public static WordSearch Instance; // Creating the instance

    public GameObject GridSystemPrefab;
    public Transform GridSystemPos;  // this gameobject will be the refernce for the gridSystem Position

    public string WordList; //this will contain the word we will store 
    public int Score;
    public Text ScoreText;
    public int WordsInList = 0;
    public int WordsInListFound= 0;
    public string WordsList;
    public Text WordsListText;
    public int LevelNo;
    public Text LevelNoText;
    public bool NextLevel = false;

    public GameObject GameScreen;
    public GameObject InstructionScreen;
    public GameObject NextLevelImg;
    public Text NextLevelImg_No;
    public GameObject Level;
    public GameObject WellDone;


    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LevelNo = 1;
        GameScreen.SetActive(false);
        StartGame();
        WellDone.SetActive(false);
    }

    private void Update()
    {
        WordsListText.text = WordList;
        ScoreText.text = Score.ToString();
        LevelNoText.text = LevelNo.ToString();

        if(WordsInList != 0 && WordsInList == WordsInListFound)
        {
            NextLevel = true;
            SkipLevel();
        }

        if(NextLevel == true)
        {
            LevelNo += 1;
            NextLevel = false;
            WellDone.SetActive(true);
            StartCoroutine(WellDoneImg());
        }
    }

    public void StartOver()  // this func is used to restart whole game
    {
        Loader.Load(Loader.Scene.WordSearch);
    }

    public void SkipLevel()  // this function will instantiate new different grid
    {
        var clock = GameObject.Find("Ysr(Clone)");
        Destroy(clock);
        WordList = "";
       
        var x = WordsInListFound * (LevelNo * 10);  // question answered * levelno 
        Score -= x;
        WordsInList = 0;
        WordsInListFound = 0;
        Level.SetActive(false);
        Restart();
    }

    public void StartGame()
    {
        StartCoroutine(LetsStartGame());
    }

    IEnumerator LetsStartGame()
    {
        yield return new WaitForSeconds(5f);
        GameScreen.SetActive(true);
        InstructionScreen.SetActive(false);
        Instantiate(GridSystemPrefab, transform.position, Quaternion.identity);  // just instantiaing the word Search grid here and setting up the positions
    }

    public void Restart()
    {
        StartCoroutine(ResetartE());
    }

    IEnumerator ResetartE()
    {
        yield return new WaitForSeconds(3f);
        Instantiate(GridSystemPrefab, transform.position, Quaternion.identity);
        NextLevelImg.SetActive(false);      
    }

    IEnumerator WellDoneImg()
    {
        yield return new WaitForSeconds(2f);
        WellDone.SetActive(false);
        NextLevelImg_No.text = LevelNo.ToString();
        NextLevelImg.SetActive(true);
    }
}
