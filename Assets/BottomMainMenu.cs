using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BottomMainMenu : MonoBehaviour
{
    public static BottomMainMenu Instance;
    public GameObject LoadingAnimation;
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
       // StartCoroutine(LoadingScreen());
    }
    public void BackBtn()
    {
        SceneManager.LoadScene(0);
        TIcTacKnow_GameManager.Instance.DestroyMe();
    }

    IEnumerator LoadingScreen()
    {
        LoadingAnimation.SetActive(true);
        yield return new WaitForSeconds(4f);
        LoadingAnimation.SetActive(false);
    }
}
