using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 public class OpenGame : MonoBehaviour
 {
    public Loader.Scene sceneName;

    private Loader.Scene previousScene;
    
    public void OpenScene()
    {
        Loader.Load(sceneName);
        Debug.Log(sceneName.ToString());
    }
    public void BackBtn()
    {
        SceneManager.LoadScene(0);
        if(TIcTacKnow_GameManager.Instance != null)
        {
            TIcTacKnow_GameManager.Instance.DestroyMe();
        }
    }
 }
