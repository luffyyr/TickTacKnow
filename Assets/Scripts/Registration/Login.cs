using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Text;
using SimpleJSON;
/// <summary>
/// This scripts is used when we want to login , this script send the username and password to this Api we calling
/// </summary>
[System.Serializable]
public class Login : MonoBehaviour
{   
    [SerializeField]
    private string user;
    [SerializeField]
    private string pass;
    [SerializeField]
    private string urlll;

    public void UserName(InputField username)
    {
        user = username.text;
    }

    public void PassWord(InputField password)
    {
        pass = password.text;
    }
    public void Submit()  //function will called when we will press the button
    {
        LogIn(user, pass);
    }

    public void LogIn(string Username, string Password)
    {
        Log cred = new Log();
        cred.username = Username;

        cred.password = Password;
        cred.novoTraxAppID = 5;
        var jason = JsonUtility.ToJson(cred);
        
        StartCoroutine(Push(urlll,jason));
    }
    IEnumerator Push(string url, string logindataJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(logindataJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        //string getByte = Encoding.ASCII.GetString(bodyRaw);
        //Debug.Log(getByte);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "my_token");
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            Debug.Log("Error: " + request.error);
        }
        else
        {
            Debug.Log("Status Code: " + request.responseCode);
            string Result = Encoding.UTF8.GetString(request.downloadHandler.data);
            ApiServer obj = JsonUtility.FromJson<ApiServer>(Result);
            Debug.Log("Token is: " +obj.message);
            Debug.Log("Status is: " +obj.status);
            if(obj.status == true)
            {
                BottomMainMenu.Instance.LogInPage.SetActive(false);
                BottomMainMenu.Instance.LoggedInForPrizes = true;
                BottomMainMenu.Instance.UserName = user;
            }
        }
    }

    [System.Serializable]
    public class Log
    {
        public string username;
        public string password;
        public int novoTraxAppID;
    }
}
