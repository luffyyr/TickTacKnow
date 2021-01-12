using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Text;
using SimpleJSON;

[System.Serializable]
public class Login : MonoBehaviour
{
    public TextMeshProUGUI Username;
    public TextMeshProUGUI Password;
    public string ysr;
    public string user;
    public string pass;
    public string urlll;

    void Start()
    {
        Username = GetComponent<TextMeshProUGUI>();
        Password = GetComponent<TextMeshProUGUI>();
    }
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
        cred.novoTraxAppID = 4;
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
            //Debug.Log("All OK");
            Debug.Log("Status Code: " + request.responseCode);
            string Result = Encoding.UTF8.GetString(request.downloadHandler.data);
            ApiServer obj = JsonUtility.FromJson<ApiServer>(Result);
            Debug.Log("Token is: " +obj.message);
            Debug.Log("Status is: " +obj.status);
            Debug.Log("userRole is: " +obj.userRole);
            Debug.Log("recordID is: " +obj.recordID);
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
