using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BottomMainMenu : MonoBehaviour
{
    public static BottomMainMenu Instance;
    public GameObject LoadingAnimation;

    public bool LoggedIn = false;
    public string UserName;
    public Text UserNameText;
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

    private void Update()
    {
        UserNameText.text = UserName;
    }

    IEnumerator LoadingScreen()
    {
        LoadingAnimation.SetActive(true);
        yield return new WaitForSeconds(4f);
        LoadingAnimation.SetActive(false);
    }
}
