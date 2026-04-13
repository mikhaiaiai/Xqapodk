using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]  private int day = 1;
    [SerializeField] private float time = 6.0f;
    [SerializeField] private bool isStopped = true;
    [SerializeField] private float gameSpeedPerSec = 144f / 3600f; /* 1 день = 
                                                     10 реальных минут */
    [SerializeField] private string localisedDay = "DAY";

    [SerializeField] private TextMeshProUGUI dayTMP;
    [SerializeField] private TextMeshProUGUI timeTMP;
    [SerializeField] private Light lightComponent;

    private void Update()
    {
        if (!isStopped)
        {
            time += gameSpeedPerSec * Time.deltaTime;
            ShowTime();

            if ((int)time == 6 && !lightComponent.enabled)
            {
                lightComponent.enabled = true;
            }

            if ((int)time == 20 && lightComponent.enabled)
            {
                lightComponent.enabled = false;
            }

            if (time >= 24f)
            {
                time = 0f;
                day++;
                ShowDay();

            }

        }
    }
    public void SetDayString(string dayString)
    {
        localisedDay = dayString;
    }
    public void SetDayTime(int d, float t)
    {
        day = d;
        time = t;
    }

    public int GetDay()
    {
        return day;
    }
    public float GetTime()
    {
        return time;
    }

    public void StartTime()
    {
        isStopped = false;
    }

    public void StopTime()
    {
        isStopped = true;
    }
     
    private void ShowDay()
    {
        dayTMP.text = localisedDay + " " + day.ToString();
    }

    private void ShowTime()
    {
        timeTMP.text = ((int)time).ToString() + ":" + ((int)((time * 60) % 60)).ToString("00");
    }
}
