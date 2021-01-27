using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    //public GameObject clock;

    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        //ShowScramble(currentWord);   //showing scramble word
        CurrentTime = 0f;
        Instruction.SetActive(true);
        ItsTime();
    }

    // Update is called once per frame
    void Update()
    {
        /*CurrentTime += Time.deltaTime;
        DisplayTime(CurrentTime);*/

        InputText.text = inputWord;
        ScoreText.text = score.ToString();
        //RepositionObject();
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(2f);
        Instruction.SetActive(false);
        ContainerBox.SetActive(true);
        TextBox.SetActive(true);
        //TimeObj.SetActive(true);
        ScoreObj.SetActive(true);
        CheckObj.SetActive(true);
        Round.SetActive(true);
        ResetIcon.SetActive(true);
        CurrentTime = 0f;
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
        var clock = GameObject.Find("ClockCanvas(Clone)");
        Destroy(clock);
        Instantiate(ClockPrefab, clockPosition.transform.position, clockPosition.rotation);
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
        if (inputWord != "")
        {
            if (inputWord == words[currentWord].word)
            {
                var clock = GameObject.Find("ClockCanvas(Clone)");
                Destroy(clock);
                currentWord++;
                CorrectAnswer();
            }
            else
            {
                //open a ui to show wrong answer
                var clock = GameObject.Find("ClockCanvas(Clone)");
                Destroy(clock);
                WrongAnswer();
            }
        }
    }
    public void UnSelect()
    {
        firstSelected = null;
    }
    public void CheckWord()
    {
        StartCoroutine(CoCheckWord());
    }
    IEnumerator CoCheckWord()
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
    }

    public void CorrectAnswer()
    {
        Correct.SetActive(true);
        StartCoroutine(correctAnswer());
    }
    IEnumerator correctAnswer()
    {
        yield return new WaitForSeconds(.5f);
        Correct.SetActive(false);
        ShowScramble(currentWord);
        inputWord = "";
        score += 20;
    }
    public void WrongAnswer()
    {
        Wrong.SetActive(true);
        StartCoroutine(wrongAnswer());
    }
    IEnumerator wrongAnswer()
    {
        yield return new WaitForSeconds(.5f);
        Wrong.SetActive(false);
        ShowScramble(currentWord);
        inputWord = "";
        score -= 20;
        if(score < 0)
        {
            score = 0;
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
        var clock = GameObject.Find("ClockCanvas(Clone)");
        Destroy(clock);
        inputWord = "";
        ShowScramble(currentWord);
    }

    public void NextQuestion()
    {
        StartCoroutine(nextQuestion());
    }

    IEnumerator nextQuestion()
    {
        yield return new WaitForSeconds(.5f);
        Wrong.SetActive(false);
        int i = currentWord + 1;
        currentWord = i;
        ShowScramble(i);
        inputWord = "";
        score -= 20;
        if (score < 0)
        {
            score = 0;
        }
    }
}
