using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ysr : MonoBehaviour
{
    public bool useWordpool; // 'should we use the wordpool?'
    public TextAsset wordpool; // if true, wordpool will be utilized

    public string[] words; // overwritten if wordpool = true
    public int maxWordCount; // max number of words used in the game to find
    public int maxWordLetters; // max length of word used 
    public bool allowReverse; // if true, words can be selected in reverse order.
    public int gridX, gridY; // grid dimensions
    public float sensitivity; // sensitivity of tiles when clicked
    public float spacing; // spacing between tiles
    public GameObject tile, background, current;
    public Color defaultTint, mouseoverTint, identifiedTint;
    public bool ready = false, correct = false;
    public string selectedString = "";
    public List<GameObject> selected = new List<GameObject>();

    private List<GameObject> tiles = new List<GameObject>();
    public GameObject temporary, backgroundObject;
    public int identified = 0;
    private float time;
    private string[,] matrix;
    private Dictionary<string, bool> word = new Dictionary<string, bool>();
    public Dictionary<string, bool> insertedWords = new Dictionary<string, bool>();
    private string[] letters = new string[26]
    {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};

    private Vector2 ray;
    private RaycastHit2D hit;
    private int mark = 0;

    // these data is used to create grid i.e row and column
    [SerializeField]
    private int rows = 5;
    [SerializeField]
    private int cols = 8;
    [SerializeField]
    private float tileSize = 1;

    public float newPosY;

    private static Ysr instance;
    public static Ysr Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        List<string> findLength = new List<string>();
        int count = 0;

        /*if (useWordpool)
        {
            words = wordpool.text.Split(';');    //storing words in it i.e total words
        }
        else
        {
            maxWordCount = words.Length; //maxWordCount storing the total no of words we have
        } 

        if (maxWordCount <= 0) 
        {
            maxWordCount = 1; 
        } */

        Mix(words); // we are now suffling the list that contain the words 



        Mathf.Clamp(maxWordLetters, 0, gridY < gridX ? gridX : gridY);  //setting maxwordletter value here based on our grid size
        Debug.Log(maxWordLetters);

        while (findLength.Count < maxWordCount + 1)  //taking words from our words list 
        {
            if (words[count].Length <= maxWordLetters)
            {
                findLength.Add(words[count]);
                //Debug.Log(words[count]);
            }
            count++;
        }

        for (int i = 0; i < maxWordCount; i++)  //now we are putting this in the dictionary so that we can check it later 
        {
            if (!word.ContainsKey(findLength[i].ToUpper()) && !word.ContainsKey(findLength[i])) //checking if the dictionary contain value and key if not then we will put this word with value
            {
                word.Add(findLength[i], false);
            }
        }

        Mathf.Clamp01(sensitivity);   //setting up the senstivity
        matrix = new string[gridX, gridY];

        InstantiateBG();

        //CenterBG(); // if we want to use this function then we have to make sure Grid pivot point is at center

        InsertWords(); // Assigning the words to the tiles on the grid


        FillRemaining();
        time = Time.time;

        transform.position = new Vector3(transform.position.x, newPosY, transform.position.z);
    }

    /// <summary>
    /// using this fucntion to suffle the string in the array
    /// </summary>
    /// <param name="words">array is passed here</param>
    private void Mix(string[] words)
    {
        for (int t = 0; t < words.Length; t++)
        {
            string tmp = words[t];
            int r = UnityEngine.Random.Range(t, words.Length);
            words[t] = words[r];
            words[r] = tmp;
        }
    }

    private void InstantiateBG()  //instantiating Grid Tiles 
    {
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("TileA"));

        for (int i = 0; i < rows; i++)   // Instantating tiles in the gameobject 
        {
            for (int j = 0; j < cols; j++)
            {
                GameObject tile = (GameObject)Instantiate(referenceTile, transform);

                float posX = j * tileSize;
                float posY = i * -tileSize;

                tile.transform.position = new Vector2(posX, posY); // assigning them position
                tile.name = "tile-" + i.ToString() + "-" + j.ToString(); //changing his name here
                BoxCollider2D boxCollider = tile.GetComponent<BoxCollider2D>() as BoxCollider2D;
                boxCollider.size = new Vector3(sensitivity, 1, sensitivity);
                tile.GetComponent<YsrLetter>().letter.text = "";
                tile.GetComponent<YsrLetter>().gridX = i;   //setting the value x of tile
                tile.GetComponent<YsrLetter>().gridY = j;   //setting the value y of tile
                tiles.Add(tile);
                matrix[i, j] = "";  //empty the matrix value i.e :x,y
            }
        }

        Destroy(referenceTile);

        float gridW = tileSize * cols;
        float gridH = tileSize * rows;

        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);  // changing the position of this gameObject
    }

    private void CenterBG() //function to move grid to center of the screen
    {
        backgroundObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + 50, 200));
    }

    /// <summary>
    /// This is our main function in which we are storing the words in form of character in the grid 
    /// </summary>
    private void InsertWords()
    {
        System.Random rn = new System.Random();
        foreach (KeyValuePair<string, bool> p in word)
        {
            string s = p.Key.Trim();
            bool placed = false;
            while (placed == false)
            {
                int row = rn.Next(gridX);  // generating random number for row 
                int column = rn.Next(gridY);  // generating random number for column
                int directionX = 0;
                int directionY = 0;
                while (directionX == 0 && directionY == 0)
                {
                    directionX = rn.Next(3) - 1;
                    directionY = rn.Next(3) - 1;
                }
                placed = InsertWord(s.ToLower(), row, column, directionX, directionY);
                mark++;
                if (mark > 100)
                {
                    break;
                }
            }
        }
    }

    private bool InsertWord(string word, int row, int column, int directionX, int directionY)
    {
        if (directionX > 0)
        {
            if (row + word.Length >= gridX)  //checking again if the length of the word is more than our grid coordinate i.e (9,9) <= word.length
            {
                return false;  //return to the function
            }
        }
        if (directionX < 0)
        {
            if (row - word.Length < 0)
            {
                return false;
            }
        }
        if (directionY > 0)
        {
            if (column + word.Length >= gridY) //checking again if the length of the word is more than our grid coordinate i.e (9,9) < word.length
            {
                return false;
            }
        }
        if (directionY < 0)
        {
            if (column - word.Length < 0)
            {
                return false;
            }
        }

        if (((0 * directionY) + column) == gridY - 1)
        {
            return false;
        }

        for (int i = 0; i < word.Length; i++)
        {
            if (!string.IsNullOrEmpty(matrix[(i * directionX) + row, (i * directionY) + column])) //simply checking that the position in that speccific coordinate is emoty or not
            {
                return false;
            }
        }

        insertedWords.Add(word, false); // we have meet all our conditions so now we can show this word in grid and add to our inserted word list
        char[] w = word.ToCharArray();   //parsing string to char array
        for (int i = 0; i < w.Length; i++)
        {
            matrix[(i * directionX) + row, (i * directionY) + column] = w[i].ToString();
            GameObject.Find("tile-" + ((i * directionX) + row).ToString() + "-" + ((i * directionY) + column).ToString()).GetComponent<YsrLetter>().letter.text = w[i].ToString();
            //var obj = GameObject.Find("/GridObject/"+"tile-" + ((i * directionX) + row).ToString() + "-" + ((i * directionY) + column).ToString());
            //Debug.Log(GameObject.Find("tile-" + ((i * directionX) + row).ToString() + "-" + ((i * directionY) + column).ToString()).name);
        }
        return true;
    }

    private void FillRemaining()
    {
        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                if (matrix[i, j] == "")
                {
                    matrix[i, j] = letters[UnityEngine.Random.Range(0, letters.Length)];
                    GameObject.Find("tile-" + i.ToString() + "-" + j.ToString()).GetComponent<YsrLetter>().letter.text = matrix[i, j];
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) //checking if we press down the mouse button
        {         
            ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //creating the raycast from camera to mouse point
            Debug.DrawRay(ray, (Input.mousePosition), Color.green);
            hit = Physics2D.Raycast(ray, Vector2.zero);
            if (hit)
            {
                current = hit.transform.gameObject;  //passing the reference of the gameobject to current 
            }
            ready = true;  //setting ready to true
        }
        if (Input.GetMouseButtonUp(0))   //if we pressup i.e stop pressing  & this will not runn until we unpress the mouse button 
        {
            ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(ray, Vector2.zero);
            if (hit)
            {
                current = hit.transform.gameObject;   // since we unpressed the mouse button so 
            }

            Verify(); //verifying the word we are selecting
        }
    }

    private void Verify() //
    {
        if (!correct)
        {
            foreach (KeyValuePair<string, bool> p in insertedWords)
            {
                if (selectedString.ToLower() == p.Key.Trim().ToLower())
                {
                    foreach (GameObject g in selected)
                    {
                        g.GetComponent<YsrLetter>().identified = true;
                    }
                    correct = true;
                }
                if (allowReverse)
                {
                    if (Reverse(selectedString.ToLower()) == p.Key.Trim().ToLower())
                    {
                        foreach (GameObject g in selected)
                        {
                            g.GetComponent<YsrLetter>().identified = true;
                        }
                        correct = true;
                    }
                }
            }
        }

        if (correct) // if word is correct that means we paired a word from our list 
        {
            insertedWords.Remove(selectedString);  //removing the selected string from our word list
            insertedWords.Remove(Reverse(selectedString));

            if (word.ContainsKey(selectedString))
            {
                insertedWords.Add(selectedString, true);
            }
            else if (word.ContainsKey(Reverse(selectedString)))
            {
                insertedWords.Add(Reverse(selectedString), true);
            }
            identified++;
        }

        ready = false;      //again reverting ready to false i.e we are not ready to check the string with inserted words
        selected.Clear();
        selectedString = "";
        correct = false;
    }

    private string Reverse(string word)   // just reversing the word here
    {
        string reversed = "";
        char[] letters = word.ToCharArray();
        for (int i = letters.Length - 1; i >= 0; i--)
        {
            reversed += letters[i];
        }
        return reversed;
    }

    void OnGUI()   // this is default unity Gui function which will shows the words and timer
    {
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("   Timer: ");
        GUILayout.Label(TimeElapsed());
        GUILayout.EndHorizontal();

        foreach (KeyValuePair<string, bool> p in insertedWords)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("   " + p.Key);
            if (p.Value)
            {
                GUILayout.Label("*");
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }

    private string TimeElapsed()
    {
        TimeSpan t = TimeSpan.FromSeconds(Mathf.RoundToInt(Time.time - time));
        return String.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }
}
