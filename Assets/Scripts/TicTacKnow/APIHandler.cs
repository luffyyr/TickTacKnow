using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

[Serializable]
public class APIHandler : MonoBehaviour
{
    [Header("Url")]
    public string url;

    public QuizManager QM;

    private void Awake()
    {
        StartCoroutine(API());
    }

    IEnumerator API()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    string Result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    //Debug.Log(Result);
                    //QAClass ysr = JsonUtility.FromJson<QAClass>(Result);         //jsonUtility only support single object type 
                    QAClass[] ysr = JsonHelper.getJsonArray<QAClass>(Result);       // we created a jsonhelper class that support array 


                    // here we are checking how many question are there in given api
                    foreach(QAClass p in ysr)
                    {
                        QM.QA.Add(p);
                    }
                    //Debug.Log(QM.QA.Count);

                    

                    int z = 0;
                    foreach (QAClass x in ysr)
                    {                        
                        //Debug.Log(x.questionId);
                        QM.QA[z].questionId = x.questionId;

                        //Debug.Log(x.questionText);
                        QM.QA[z].questionText = x.questionText;

                        //Debug.Log(x.answer);
                        QM.QA[z].answer = x.answer;

                        int h = 0;
                        foreach (Option y in x.options)
                        {
                            //Debug.Log(y.optionText);
                            QM.QA[z].options[h].optionText = y.optionText;

                            //Debug.Log(y.optionID);
                            QM.QA[z].options[h].optionID = y.optionID;
                            h++;
                        }                        
                        z++;
                    }

                }
            }
        }
    }
}

#region 
public class JsonHelper     // we are creating a custom wrapper class to hold the array from json format
{
    public static T[] getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}
#endregion  //Jason Helper Class to store the Array from jason file
