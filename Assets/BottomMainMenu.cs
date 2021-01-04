using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BottomMainMenu : MonoBehaviour
{
    public static BottomMainMenu Instance;
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

    public void BackBtn()
    {
        SceneManager.LoadScene(0);
        TIcTacKnow_GameManager.Instance.DestroyMe();
    }
}
