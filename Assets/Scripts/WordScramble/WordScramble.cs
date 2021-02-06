using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class Word
{
    public string word;  // word we want to show in inspector
    [Header("leave empty if you want randomized")]
    public string desiredRandom; // randomized word of the above word string

    public string GetString()
    {
        if (!string.IsNullOrEmpty(desiredRandom))
        {
            return desiredRandom;
        }

        string result = word;
       
        while(result == word)
        {
            result = "";
            List<char> characters = new List<char>(word.ToCharArray());
            while (characters.Count > 0)   // we are radomizing the word letters here
            {
                int indexChar = Random.Range(0, characters.Count - 1);
                result += characters[indexChar];

                characters.RemoveAt(indexChar);
            }           
        }
        return result;
    }
}
public class WordScramble : MonoBehaviour
{
    public Word[] words;
    [Header("UI Reference")]
    public CharObject prefab;
    public Transform container;
    public float space;
   

    List<CharObject> charObjects = new List<CharObject>();
    CharObject firstSelected;

    public int currentWord;
    public float LerpSpeed;      //it is speed of appearing the words on screen that we are using in lerp function

    public static WordScramble main;

    [Header("UI Components")]
    public string inputWord;
    public Text InputText;
    public int score = 0;
    public Text ScoreText;
    public float CurrentTime;
    public Text CurrentTimeText;

    [Header("Set these Gameobject False")]
    public GameObject Instruction;
    public GameObject ContainerBox;
    public GameObject TextBox;
    public GameObject Round;
    public GameObject TimeObj;
    public GameObject ScoreObj;
    public GameObject CheckObj;
    public GameObject Correct;
    public GameObject Wrong;
    public GameObject ResetIcon;
    public GameObject ClockPrefab;

    public Transform clockPosition;

    public int currentLevel;
    public Text currentLevel_Text;
    public int QuestionedAnswerd;
    public GameObject gameOverImg;
    public GameObject CorrectAnswerImg;
    public GameObject WrongAnswerImg;
    public int CorrectAnswerNo;
    public Text CorrectAnswer_Text;
    public int WrongAnswerNo;
    public Text WrongAnswerl_Text;
    public int TotalQuestion;
    public GameObject RoundUI_Img;
    public Text RoundUI_Img_Text;


    //public GameObject clock;
    char[] wordchar;
    public bool canCheck;  // bool to check if we can check our answer or not 

    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        //ShowScramble(currentWord);   //showing scramble word
        gameOverImg.SetActive(false);
        WrongAnswerImg.SetActive(false);
        CorrectAnswerImg.SetActive(false);

        currentLevel = 1;
        CurrentTime = 0f;
        Instruction.SetActive(true);
        ItsTime();
    }

    // Update is called once per frame
    void Update()
    {
        /*CurrentTime += Time.deltaTime;
        DisplayTime(CurrentTime);*/

        CorrectAnswer_Text.text = CorrectAnswerNo.ToString();
        WrongAnswerl_Text.text = WrongAnswerNo.ToString();
        InputText.text = inputWord;
        ScoreText.text = score.ToString();
        currentLevel_Text.text = currentLevel.ToString();

        //RepositionObject();
        if (Input.GetKeyDown(KeyCode.Space))    // we will null the char[] here
        {
            //wordchar = null;
            StartCoroutine(LOL());
        }

        //Check_increaseLevel();
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(5f);
        Instruction.SetActive(false);
        ContainerBox.SetActive(true);
        TextBox.SetActive(true);
        //TimeObj.SetActive(true);
        ScoreObj.SetActive(true);
        CheckObj.SetActive(true);
        Round.SetActive(true);
        ResetIcon.SetActive(true);
        CurrentTime = 0f;
        canCheck = true;
        CorrectAnswerImg.SetActive(true);
        WrongAnswerImg.SetActive(true);
        ShowScramble(currentWord);
    }

    public void ItsTime()
    {
        StartCoroutine(startGame());
    }

    void DisplayTime(float timeToDisplay)
    {
        //timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        CurrentTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void RepositionObject()
    {
        if(charObjects.Count == 0)
        {
            return;
        }

        float center = (charObjects.Count - 1) / 2;
        for(int i = 0; i < charObjects.Count; i++)
        {
            //charObjects[i].rectTransform.anchoredPosition = Vector2.Lerp(charObjects[i].rectTransform.anchoredPosition,new Vector2((i - center) * space, 0), LerpSpeed * Time.deltaTime);  // setting up the position of the word blocks in ui

            charObjects[i].rectTransform.anchoredPosition = new Vector2((i - center) * space, 0);  // setting up the position of the word blocks in ui


            charObjects[i].index = i;
        }
       
        Instantiate(ClockPrefab, clockPosition.transform.position, clockPosition.rotation);
        TotalQuestion += 1;
    }

    /// <summary>
    /// show a random word to the screen
    /// </summary>
    public void ShowScramble()
    {
        int i = Random.Range(0, words.Length - 1);
        ShowScramble(i);
        Debug.Log(i);
    }

    /// <summary>
    /// show word from collection with desired index
    /// </summary>
    /// <param name="index">index of the element</param>
    public void ShowScramble(int index)    //we will take the random word from the input words like cat, programming etc ,index shows the list index of the words
    {
        charObjects.Clear();
        foreach(Transform child in container)
        {
            Destroy(child.gameObject);
        }

        if(index > words.Length - 1)
        {
            Debug.Log("index out of range, please enter range between 0_" + (words.Length - 1).ToString());
            return;
        }

        char[] chars = words[index].GetString().ToCharArray(); // converting string to char array 
        foreach(char c in chars)
        {
            CharObject clone = Instantiate(prefab.gameObject).GetComponent<CharObject>();
            clone.transform.SetParent(container);

            charObjects.Add(clone.Init(c));
        }

        currentWord = index;

        RepositionObject();
    }

    public void Swap(int indexA, int indexB)
    {
        CharObject tmpA = charObjects[indexA];

        charObjects[indexA] = charObjects[indexB];
        charObjects[indexB] = tmpA;


        //charObjects[indexA].transform.SetAsLastSibling(); //?
        //charObjects[indexB].transform.SetAsLastSibling(); //?
        //CheckWord();
    }

    public void Select(CharObject charObject)
    {
        if (firstSelected)
        {
            Swap(firstSelected.index, charObject.index);
            //Unselect
            //firstSelected = null;
            firstSelected.Select();   //?
            charObject.Select();     //?
        }
        else
        {
            firstSelected = charObject;
        }
    }

    public void Pressed(CharObject charObject)  // this is called when the button is pressed i.e word button
    {
        var y = charObject.character;  // we are taking his character value and storing it in local varialble y
        InputWord(y);
        charObject.GetComponent<Button>().interactable = false;
        //charObject.Select();                                                   ////////////////////////////////////////////////////
    }

    public void InputWord(char y)
    {
        // check if this button is already selected or not
        //if not then only we will allow it to add char to our string else not
        inputWord += y;  //Adding char to the string to make a word
    }
    public void Verify()
    {      
        if (inputWord != "" && canCheck)
        {
            canCheck = false;
            ResetCanCheck();
            if (inputWord == words[currentWord].word)
            {
                Arrange();
                var clock = GameObject.Find("ClockCanvas(Clone)");
                Destroy(clock);
                currentWord++;
                CorrectAnswer();               
            }
            else
            {
                Arrange();
                var clock = GameObject.Find("ClockCanvas(Clone)");
                Destroy(clock);
                currentWord++;
                WrongAnswer();
                
            }
        }
    }
    public void UnSelect()
    {
        firstSelected = null;
    } // not using this
   /* public void CheckWord()
    {
        StartCoroutine(CoCheckWord());
    }  //not using */

    /*IEnumerator CoCheckWord()
    {
        yield return new WaitForSeconds(.5f);
        string word = "";
        foreach (CharObject charObject in charObjects)
        {
            word += charObject.character;
        }

        if (word == words[currentWord].word)
        {
            currentWord++;
            ShowScramble(currentWord);
        }
    } // not using*/

    public void CorrectAnswer()
    {
        Correct.SetActive(true);
        StartCoroutine(correctAnswer());
    }
    IEnumerator correctAnswer()
    {
        yield return new WaitForSeconds(3f);
        Correct.SetActive(false);
        CorrectAnswerNo += 1;
        if(CorrectAnswerNo == 5)
        {
            Debug.Log("Round Complete");
            currentLevel += 1;
            RESETUI();
            ShowRoundUI();
        }
        else
        {
            ShowScramble(currentWord);
        }
        inputWord = "";
        score += currentLevel*10;
    }
    public void WrongAnswer()
    {
        Wrong.SetActive(true);
        StartCoroutine(wrongAnswer());
    }
    IEnumerator wrongAnswer()
    {
        yield return new WaitForSeconds(3f);
        WrongAnswerNo += 1;
        if (WrongAnswerNo >= 5)
        {
            Debug.Log("we lost");
            GameOver();
        }
        else
        {
            Wrong.SetActive(false);
            inputWord = "";
            score -= (currentLevel * 10) / 2;
            if (score < 0)
            {
                score = 0;
            }
            ShowScramble(currentWord);
        }       
    }

    public void DeletethisChar(CharObject cha)  // we want delete this word when pressed the button and also set interactable to true
    {
        for(int i = 0; i < inputWord.Length; i++ )
        {
            if(cha.character == inputWord[i])
            {
                //inputWord.Remove(i);
                //Debug.Log(inputWord);
                print("found it ");
                return;
            }
            
        }
        // we will first take the char it contain 
        // now we will search this char in the charobj list and then remove it 
        // now we will show this button interactable again 
        // and also show the word written in the box after removing this char
    }

    public void ResetWord()  // this will reset the word and suffle the scrambled word again
    {
        //var clock = GameObject.Find("ClockCanvas(Clone)");  // we dont want to destroy clock cause it will give user the infinite time
        //Destroy(clock);
        inputWord = "";
        ResetScramble(currentWord);  // instead of using this we have to create a new function similar to this but we have to remove the clock instantiate code 
    }

    void ResetScramble(int index)
    {
        charObjects.Clear();
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        if (index > words.Length - 1)
        {
            Debug.Log("index out of range, please enter range between 0_" + (words.Length - 1).ToString());
            return;
        }

        char[] chars = words[index].GetString().ToCharArray(); // converting string to char array 
        foreach (char c in chars)
        {
            CharObject clone = Instantiate(prefab.gameObject).GetComponent<CharObject>();
            clone.transform.SetParent(container);

            charObjects.Add(clone.Init(c));
        }

        currentWord = index;

        //////////////////////////////////////////////////////////
        
        if (charObjects.Count == 0)
        {
            return;
        }

        float center = (charObjects.Count - 1) / 2;
        for (int i = 0; i < charObjects.Count; i++)
        {
            //charObjects[i].rectTransform.anchoredPosition = Vector2.Lerp(charObjects[i].rectTransform.anchoredPosition,new Vector2((i - center) * space, 0), LerpSpeed * Time.deltaTime);  // setting up the position of the word blocks in ui

            charObjects[i].rectTransform.anchoredPosition = new Vector2((i - center) * space, 0);  // setting up the position of the word blocks in ui


            charObjects[i].index = i;
        }
    } // this function is only used to scramble word and repostion it , dont use this function except when you want to reset
    public void NextQuestion()
    {
        StartCoroutine(nextQuestion());
    }

    IEnumerator nextQuestion()
    {
        yield return new WaitForSeconds(3f);
        /* Wrong.SetActive(false);
         int i = currentWord + 1;
         currentWord = i;
         ShowScramble(i);
         inputWord = "";
         score -= (currentLevel * 10)/2;
         if (score < 0)
         {
             score = 0;
         }*/

        WrongAnswerNo += 1;
        if (WrongAnswerNo >= 5)
        {
            Debug.Log("we lost");
            GameOver();
        }
        else
        {
            Wrong.SetActive(false);
            inputWord = "";
            int i = currentWord + 1;
            currentWord = i;
            score -= (currentLevel * 10) / 2;
            if (score < 0)
            {
                score = 0;
            }
            ShowScramble(currentWord);
        }

    }

    public void DetectandDestroyClock()
    {
        var clock = GameObject.Find("ClockCanvas(Clone)");
        Destroy(clock);
    }

    public void Arrange()
    {
        StartCoroutine(LOL());
    }
    IEnumerator LOL()
    {        
        wordchar = words[currentWord].word.ToCharArray();

        for (int i = 0; i < container.childCount; i++)
        {
            Transform var = container.GetChild(i);
            var x = var.GetComponent<CharObject>().character;
            //var xIndex = var.GetSiblingIndex();

            if (wordchar[i] != x)
            {
                var var_pos = var.transform.position;

                for (int j = i + 1; j < container.childCount; j++)
                {
                    Transform var2 = container.GetChild(j);
                    var y = var2.GetComponent<CharObject>().character;

                    if (wordchar[i] == y)
                    {
                        //Debug.Log(y);
                        //var var2_pos = var2.transform.position;
                        //var.transform.position = var2.transform.position;
                        //var2.transform.position = var_pos;

                        // using dotween animations here
                        var.transform.DOMove(var2.transform.position, .5f, false).SetEase(Ease.InOutElastic);
                        var.transform.SetSiblingIndex(j);
                        var2.transform.DOMove(var_pos, .5f, false).SetEase(Ease.InOutElastic);
                        var2.transform.SetSiblingIndex(i);
                        yield return new WaitForSeconds(.5f);
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else
            {
                continue;
            }
        }
    }

    public void ResetCanCheck()
    {
        StartCoroutine(ResetCheckBtn());
    }

    IEnumerator ResetCheckBtn()
    {
        yield return new WaitForSeconds(3f);
        canCheck = true;
    }

    public void Check_increaseLevel()
    {
        if(CorrectAnswerNo == 5)
        {
            Debug.Log("Round Complete");
            currentLevel += 1;
            RESETUI();
        }
        if(WrongAnswerNo >= 5)
        {
            Debug.Log("we lost");
            GameOver();
        }
    }

    public void GameOver()
    {
        var clock = GameObject.Find("ClockCanvas(Clone)");
        Destroy(clock);
        gameOverImg.SetActive(true);
        StartCoroutine(RestartLevel());
    }

    public void RESETUI()
    {
        CorrectAnswerNo = 0;
        WrongAnswerNo = 0;
        TotalQuestion = 0;
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(5f);
        Loader.Load(Loader.Scene.WordScramble);
    }

    public void ShowRoundUI()
    {
        Instruction.SetActive(false);
        ContainerBox.SetActive(false);
        TextBox.SetActive(false);
        //TimeObj.SetActive(true);
        ScoreObj.SetActive(false);
        CheckObj.SetActive(false);
        Round.SetActive(false);
        ResetIcon.SetActive(false);

        CorrectAnswerImg.SetActive(false);
        WrongAnswerImg.SetActive(false);

        RoundUI_Img_Text.text = currentLevel.ToString();
        RoundUI_Img.SetActive(true);
        StartCoroutine(NextLevel());
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(3f);
        ShowScramble(currentWord);

        RoundUI_Img.SetActive(false);

        Instruction.SetActive(false);
        ContainerBox.SetActive(true);
        TextBox.SetActive(true);
        //TimeObj.SetActive(true);
        ScoreObj.SetActive(true);
        CheckObj.SetActive(true);
        Round.SetActive(true);
        ResetIcon.SetActive(true);

        CorrectAnswerImg.SetActive(true);
        WrongAnswerImg.SetActive(true);
    }
}
