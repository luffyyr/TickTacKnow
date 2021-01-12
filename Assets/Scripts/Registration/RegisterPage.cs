using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.Networking;

[System.Serializable]
public class RegisterPage : MonoBehaviour
{
    List<string> Sex = new List<string> { "Male", "Female", "Other" };
    

    [Header("Registration Items")]    
    public string Email;
    public string FirstName;
    public string LastName;
    public string Gender;
    public string LoginCredential;
    public string VerificationOTP;
    private int App_ID = 5;

    public string Password;
    public string ConfirmPassword;
    public string Name;

    [Header("URL")]
    public string RegistrationUrl;
    public string VerificationUrl;

    [Header("UIObjects")]
    public GameObject RegistrationPage;
    public GameObject VerificationPage;
    public Dropdown SelectSex;

    private void Start()
    {
        PopulateList();
    }

    void PopulateList()
    {
        SelectSex.AddOptions(Sex);
    }

    public void FName(InputField firstname)
    {
        FirstName = firstname.text;
    }

    public void LName(InputField lastname)
    {
        LastName = lastname.text;
    }

    public void Mail(InputField email)
    {
        Email = email.text;
    }
    public void Genderr(int index)
    {
        Gender = Sex[index];
        Debug.Log(Sex[index]);
    }

    public void Key(InputField password)
    {
        Password = password.text;
    }

    public void ConfirmKey(InputField confirmpassword)
    {
        ConfirmPassword = confirmpassword.text;
    }

    public void verificationKey(InputField otp)
    {
        VerificationOTP = otp.text;
    }

    public void Submit()  //function will called when we will press the button
    {
        Name = FirstName +" "+ LastName;
        //Debug.Log(Name);
        LogIn(App_ID, Email);
        RegistrationPage.SetActive(false);
        VerificationPage.SetActive(true);
    }

    public void LogIn(int AppId, string Email)
    {
        Root cred = new Root();
        cred.app_ID = AppId;
        cred.email = Email;
        Debug.Log(cred.app_ID);
        Debug.Log(cred.email);
        var jason = JsonUtility.ToJson(cred);

        StartCoroutine(PushLogIn(RegistrationUrl, jason));
    }
    IEnumerator PushLogIn(string url, string logindataJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(logindataJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);

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
            RootResponse obj = JsonUtility.FromJson<RootResponse>(Result);
            Debug.Log(Result);
            Debug.Log("status" + obj.status);
            Debug.Log("status" + obj.message);             
        }
    }

    public void SubmitOTP()
    {
        OTP(VerificationOTP);
    }

    public void OTP(string otp)
    {
        Registration cred = new Registration();
        cred.verificationOTP = otp;
        cred.email = Email;
        cred.app_ID = App_ID;
        cred.firstName = FirstName;
        cred.lastName = LastName;
        cred.loginCredential = Password;
        cred.gender = Gender;

        var jason = JsonUtility.ToJson(cred);
        Debug.Log(jason);
        StartCoroutine(PushOTP(VerificationUrl, jason));
    }
    IEnumerator PushOTP(string url, string logindataJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(logindataJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);

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
            RootResponse obj = JsonUtility.FromJson<RootResponse>(Result);
            Debug.Log(Result);
            Debug.Log("status" + obj.status);
            Debug.Log("status" + obj.message);
        }
    }

    [System.Serializable]
    public class Root
    {
        public int app_ID;
        public string email;
    }

    [System.Serializable]
    public class RootResponse
    {
        public bool status;
        public string message;
    }

    public void CLoseRegistrationPage()
    {
        RegistrationPage.SetActive(false);
    }

    public void CloseVerificationPage()
    {
        VerificationPage.SetActive(false);
    }
}