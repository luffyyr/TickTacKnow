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
                    int z = 0;
                    foreach (QAClass x in ysr)
                    {                        
                        //Debug.Log(x.questionId);
                        QM.QA[z].QuestionID = x.questionId;
                        //Debug.Log(x.questionText);
                        QM.QA[z].Question = x.questionText;
                        //Debug.Log(x.answer);
                        QM.QA[z].CorrectAnsmwerID = x.answer;
                        int h = 0;
                        foreach (Option y in x.options)
                        {
                            //Debug.Log(y.optionText);
                            QM.QA[z].Opts[h].AnswerText = y.optionText;
                            //Debug.Log(y.optionID);
                            QM.QA[z].Opts[h].AnswerID = y.optionID;
                            Debug.Log("valuee of h is: " + h);
                            h++;
                        }                        
                        Debug.Log("valueeee of zzzz is : "+z);
                        z++;
                    }

                }
            }
        }
    }
}
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

