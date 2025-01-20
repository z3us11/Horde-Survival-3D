using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text timerTxt;
    public TMP_Text currentLapTxt;
    public TMP_Text bestLapTxt;

    List<float> currentLapTimes;
    List<float> bestLapTimes;

    float timer;
    float bestTime;

    private void Start()
    {
        currentLapTimes = new List<float>();
        bestLapTimes = new List<float>();

        currentLapTxt.text = "CURRENT LAP: \n";
        bestLapTxt.text = "BEST LAP : \n";
        timerTxt.text = "";
        bestTime = float.MaxValue;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timerTxt.text = FormatToSecondsMilliseconds(timer);
    }

    public void UpdateTimer(int checkpointNum)
    {
        if(checkpointNum == 0)
        {
            currentLapTxt.text = "CURRENT LAP: \n";
        }

        if(checkpointNum >= currentLapTimes.Count)
        {
            currentLapTimes.Add(timer);
            currentLapTxt.text += "<color=white>" + FormatToSecondsMilliseconds(currentLapTimes[checkpointNum]) + "</color>\n";
        }
        else
        {
            currentLapTimes[checkpointNum] = timer;
            if (currentLapTimes[checkpointNum] < bestLapTimes[checkpointNum])
            {
                currentLapTxt.text += "<color=green>" + FormatToSecondsMilliseconds(currentLapTimes[checkpointNum]) + "</color>\n";
                bestLapTimes[checkpointNum] = currentLapTimes[checkpointNum];
            }
            else if(currentLapTimes[checkpointNum] > bestLapTimes[checkpointNum])
            {
                currentLapTxt.text += "<color=red>" + FormatToSecondsMilliseconds(currentLapTimes[checkpointNum]) + "</color>\n";
            }
            else
            {
                currentLapTxt.text += "<color=white>" + FormatToSecondsMilliseconds(currentLapTimes[checkpointNum]) + "</color>\n";
            }
        }
        if(checkpointNum >= bestLapTimes.Count)
        {
            bestLapTimes.Add(timer);
        }
    }

    public void UpdateBestTimer()
    {
        if(timer < bestTime)
        {
            bestTime = timer;
            for(int i = 0; i < bestLapTimes.Count; i++)
            {
                bestLapTimes[i] = currentLapTimes[i];
            }
        }

        timer = 0;

        bestLapTxt.text = "BEST LAP : " + bestTime + "\n";
        foreach(float lapTime in bestLapTimes) 
        {
            bestLapTxt.text += FormatToSecondsMilliseconds(lapTime) + "\n";
        }
    }

    string FormatToSecondsMilliseconds(float time)
    {
        // Calculate minutes and seconds
        int seconds = Mathf.FloorToInt(time);
        int milliseconds = Mathf.FloorToInt((time - seconds) * 1000); // Remaining milliseconds
        // Format as mm:ss
        return string.Format("{0}:{1:000}", seconds, milliseconds);
    }

}
