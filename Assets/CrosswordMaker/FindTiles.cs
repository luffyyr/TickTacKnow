using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FindTiles : MonoBehaviour
{
    public int totalCrWrd;



    private void Start()
    {
        
    }

    private void Update()
    {

       
    }

    //for highliting same tiles of a word
    public void FindWord(string acrossName, string downName)
    {
        UIManager.instance.totalCrWrd = gameObject.transform.childCount;

        int count = 0;

        while (count < gameObject.transform.childCount)
        {

            //tiles founded
            print(gameObject.transform.GetChild(count).gameObject);

            //for restting other boxes to their default colour
            GameObject obj = gameObject.transform.GetChild(count).gameObject;

            obj.transform.GetChild(1).GetChild(0).GetComponent<TMP_InputField>().image.color = Color.Lerp(Color.white, Color.red, 0.05f);

            if (obj.transform.GetChild(2).gameObject.name == acrossName)
            {
                obj.transform.GetChild(1).GetChild(0).GetComponent<TMP_InputField>().image.color = Color.Lerp(Color.yellow, Color.white, 0.2f);

            }

            if (obj.transform.GetChild(3).gameObject.name == downName)
            {
                obj.transform.GetChild(1).GetChild(0).GetComponent<TMP_InputField>().image.color = Color.Lerp(Color.yellow, Color.white, 0.2f);
            }




            count++;
        }

     

    }
}
