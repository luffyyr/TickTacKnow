using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour {

    private const float REAL_SECONDS_PER_INGAME_DAY = 30f;

    private Transform clockHourHandTransform;
    private Transform clockMinuteHandTransform;
    private Text timeText;
    private float day;
    private bool Started =  false;

    public QuizManager qiz;

    private void Awake() 
    {
        qiz = GameObject.Find("QuizManager").GetComponent<QuizManager>();
        clockHourHandTransform = transform.Find("hourHand");
        clockMinuteHandTransform = transform.Find("minuteHand");
        timeText = transform.Find("timeText").GetComponent<Text>();
    }

    private void Start()
    {
        StartCoroutine(setTrue());
    }
    private void Update() 
    {
        ResetClock();
    }

    public void SetTrueTrue()
    {
        StartCoroutine(setTrue());
    }
    IEnumerator setTrue()
    {
        yield return new WaitForSeconds(5f);
        Started = true;
    }
    void ResetClock()
    {
            day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;
            float dayNormalized = day % 1f;

            float rotationDegreesPerDay = 360f;
            //clockHourHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay);

            float hoursPerDay = 30f;
            clockMinuteHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay);

            string hoursString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");

            float minutesPerHour = 60f;
            string minutesString = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHour).ToString("00");
            timeText.text = hoursString + ":" + minutesString;

            if (hoursString == "00" && Started == true)
            {
                qiz.Wrong();
                Destroy(this.gameObject);
                //Started = false;
            }
    }
}
