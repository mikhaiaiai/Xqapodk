using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int day;
    public float time;
    public bool isRunning;

    private float timePerSec = 1 / 120;

    private void Update()
    {
        if (isRunning)
        {
            time += timePerSec * Time.deltaTime;
            if (time % 1 == 0)
            {
                
            }
            if (time >= 24f)
            {
                day++;
                time = 0;
            }
        }

        
    }
}
