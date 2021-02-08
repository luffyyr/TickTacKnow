using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //public Button clueButton;
    public static UIManager instance;
   // public CrosswordsAlgorithm GetCrosswordsAlgo;
   // public GameObject cluePanel;

    public GameObject youWOnPanel;
    public GameObject menuCanvas;
    public int rightCount;
    public int totalCrWrd = 1;
    public GameObject tileContainer;
    public GameObject cluePanels;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        Invoke("OffMenu", 5f);
    }


    void OffMenu()
    {
        menuCanvas.SetActive(false);
    }


    private void Update()
    {

        if (rightCount != 0)
        {
            if (rightCount == totalCrWrd)
            {
                Invoke("YouWon", 2f);
            }
        }
    }


    //public void ShowClues()
    //{
    //    if(!cluePanel.activeInHierarchy)
    //    {
    //        cluePanel.SetActive(true);
    //    }
    //    else
    //    {
    //        cluePanel.SetActive(false);
    //    }
    //}

    public void YouWon()
    {
        youWOnPanel.SetActive(true);
        tileContainer.SetActive(false);
        cluePanels.SetActive(false);
    }

}
