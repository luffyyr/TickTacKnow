using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockWordScramble : MonoBehaviour
{

    private const float REAL_SECONDS_PER_INGAME_DAY = 30f;

    private Transform clockHourHandTransform;
    private Transform clockMinuteHandTransform;
    private Text timeText;
    private float day;
    private bool Started = false;
    [SerializeField]
    private Animator anim;

    public WordScramble qiz;

    public bool AlarmTime;
    public AudioClip alamSound;
    public AudioSource speaker;

    private void Awake()
    {
        qiz = GameObject.Find("WorldScrambleManager").GetComponent<WordScramble>();
        clockHourHandTransform = transform.Find("hourHand");
        clockMinuteHandTransform = transform.Find("minuteHand");
        timeText = transform.Find("timeText").GetComponent<Text>();
    }

    private void Start()
    {
        StartCoroutine(setTrue());
        anim = GetComponent<Animator>();
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

        if (hoursString == "25" && Started == true && !AlarmTime)
        {
            dosomething();
            AlarmTime = true;
            Debug.Log("Animating");
        }

        if (hoursString == "00" && Started == true)
        {
            qiz.NextQuestion();
            //Destroy(this.gameObject);
            var clock = GameObject.Find("ClockCanvas(Clone)");
            Destroy(clock);
            //Started = false;
        }
    }

    void dosomething()
    {
        anim.SetTrigger("AlarmTime");
        speaker.PlayOneShot(alamSound);
    }

}
