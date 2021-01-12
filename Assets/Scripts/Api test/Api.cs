using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System;

public class Api : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(APi());
    }

    IEnumerator APi()
    {
        //string Url = "https://apidev.novotraxdemo.com/api/Bus/GetFuelExpense?vehicleId=18";  // taking url and saving as string
        string Url = "https://jsonplaceholder.typicode.com/todos/1";  // taking url and saving as string
        //string Url = "https://api.github.com/users/hadley/orgs";  // taking url and saving as string
        using (UnityWebRequest www = UnityWebRequest.Get(Url)) 
        {
            yield return www.SendWebRequest();     // simply checking if the network is working or not any other issue
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    //string Result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data); // reading data from server file
                    //Root ysrr = JsonUtility.FromJson<Root>(Result);
                    JSONNode Result = JSON.Parse(System.Text.Encoding.UTF8.GetString(www.downloadHandler.data));
                    Debug.Log("" +Result["userId"]);
                    Debug.Log(Result); // printing the result 
                    //Root ysrr = JsonUtility.FromJson<Root>(Result);                    
                    //Debug.Log(ysrr.userId);
                    //Debug.Log(ysrr.id);
                    //Debug.Log(ysrr.title);
                    //Debug.Log(ysrr.completed);

                }
            }
        }        
    }
    
   [Serializable]
    public class Root
    {
        public int userId;
        public int id;
        public string title;
        public bool completed;
    }
    [Serializable]
    public class Root2
    {
        public string login { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string hooks_url { get; set; }
        public string issues_url { get; set; }
        public string members_url { get; set; }
        public string public_members_url { get; set; }
        public string avatar_url { get; set; }
        public string description { get; set; }
    }
}
