using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(0);
        TIcTacKnow_GameManager.Instance.DestroyMe();
    }
}

