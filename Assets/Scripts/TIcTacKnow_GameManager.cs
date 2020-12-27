using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TIcTacKnow_GameManager : MonoBehaviour
{
    public static TIcTacKnow_GameManager Instance;

    public GameObject Levels;
    public Text ScoreTxt;
    public Text RoundText;

    public float StartTime;
    public int TotalTime;

    public float TotalScore;
    
    public int RoundNo;
    public int LevelNo;

    [Header("IdleorNot")]
    private float afkTimer = 120;
    public GameObject AFK;

    //these gameobjects will be disable now
    GameObject g1;
    GameObject g2;


    void instance()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Awake()
    {
        instance();
    }

    private void Start()
    {
        StartTime = Time.time;  //it will store the starting time when game was started

        StartCoroutine(StartGame());
        StartCoroutine(TotalTimeSpend());
    }
    void Update()
    {
        ScoreTxt.text = (""+TotalScore);
        RoundText.text = (""+RoundNo);

        AFKTimer();
    }

    IEnumerator StartGame()
    {
        Levels.SetActive(true);
        yield return new WaitForSeconds(2f);
        Levels.SetActive(false);
        TIcTAc.Instance.GetReady();
    }

    IEnumerator LevelCompleted()// change this completely so when we move to round 2 we will take new questions again we will redo all things instead of restating the game
    {

        if (RoundNo == 4)
        {
            RoundNo = 1;
            LevelNo += 1;
            Levels.SetActive(true);
            Levels.GetComponent<Text>().text = ("Level " + LevelNo);
            yield return new WaitForSeconds(2f);
            Levels.SetActive(false);           
            TIcTAc.Instance.GetReady();
        }
        else
        {          
            yield return new WaitForSeconds(.15f);
            TIcTAc.Instance.GetReady();
        }
    }

    public void WinReset()
    {
        StartCoroutine(ResetGame());
    }

    public void LostReset()
    {
        StartCoroutine(GameLost());
    }
    IEnumerator ResetGame()  // In this we will reset all the components like we see at start
    {
        yield return new WaitForSeconds(4f);
        TIcTAc.Instance.YouWon.SetActive(false);
        //before doing this we have to set the value of QA to null so all question will be deleted
        // we will call api coroutine to get questions from it
        //we have to reset the tictac box , make icon dissapear and make it interactable again
        RoundNo += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // restarting the current scene
        StartCoroutine(LevelCompleted());
    }

    IEnumerator GameLost()  // In this we will reset all the components like we see at start
    {
        yield return new WaitForSeconds(4f);
        TIcTAc.Instance.YouLost.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // restarting the current scene

        RoundNo = 1;
        LevelNo = 1;
        TotalScore = 0;
        Levels.SetActive(true);
        Levels.GetComponent<Text>().text = ("Level " + LevelNo);
        yield return new WaitForSeconds(2f);
        Levels.SetActive(false);
        TIcTAc.Instance.GetReady();
    }

    IEnumerator TotalTimeSpend()
    {
        TotalTime = (int)(Time.time - StartTime);
        //Debug.Log(TotalTime +"Sec");
        yield return new WaitForSeconds(5f);
        StartCoroutine(TotalTimeSpend());
    }

    private void AFKTimer()
    {
        if (Input.anyKey)   //checking we clicked any key while playing game or not(basically checking we are AFK or Not)
        {
            afkTimer = 15f;
            AFK.SetActive(false);
            Debug.Log("any key pressed");
        }
        if (afkTimer <= 0f)
        {
            //qiz = GameObject.Find("QuizManager").GetComponent<QuizManager>();
            AFK.SetActive(true);
            Debug.Log("Afk");
        }
        afkTimer -= Time.deltaTime;
    }
}
