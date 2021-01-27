using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This is our GameManager Script
/// </summary>
public class BottomMainMenu : MonoBehaviour
{
    public static BottomMainMenu Instance;
    public GameObject LoadingAnimation;

    public bool LoggedInForPrizes = false;
    public bool LoggedInForFun = false;
    public string UserName;
    public Text UserNameText;

    [Header("UIObjects")]
    public GameObject LogInPage;
    public GameObject RegistrationPage;
    public GameObject VerificationPage;
    public GameObject playforprizes;
    public GameObject playforfun;
    public GameObject RegisteredUI;

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

    public void CloseRegisrationPage()
    {
        RegistrationPage.SetActive(false);
    }

    public void OpenRegisrationPage()
    {
        RegistrationPage.SetActive(true);
    }

    public void CloseVerificationPage()
    {
        VerificationPage.SetActive(false);
    }

    public void OpenVerificationPage()
    {
        VerificationPage.SetActive(true);
    }

    public void OpenRegisteredPage()
    {
        RegisteredUI.SetActive(true);
    }
    public void CloseRegisteredPage()
    {
        RegisteredUI.SetActive(false);
    }

    public void PlayforFun()
    {
        playforfun.SetActive(false);
        playforprizes.SetActive(false);
        LoggedInForFun = true;
    }
    public void PlayforPrizes()
    {
        playforprizes.SetActive(false);
        playforfun.SetActive(false);
        LogInPage.SetActive(true);
    }
    public void WannaPlay()
    {
        playforprizes.SetActive(true);
        playforfun.SetActive(true);
    }
    public void CreateNewAccount()
    {
        LogInPage.SetActive(false);
        RegistrationPage.SetActive(true);
    }
    
    public void CloseLoginPage()
    {
        LogInPage.SetActive(false);
    }
}
