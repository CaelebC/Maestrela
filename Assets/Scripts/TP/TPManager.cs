using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TPManager : MonoBehaviour
{   
    // private Text textTP;
    // private Text textTimer;
    
    private int totalTP;
    private int maxTP;
    private int regenRate;
    private bool regenerating = false;
    
    private DateTime nextRegenTime;
    private DateTime lastRegenTime;

    public int regenTPAmount = 1;

    bool firstRun = true;
    
    void Start()
    {
        string value = PlayerStats.TP.ToString();
        Debug.Log("TP is: " + value);
        totalTP = PlayerStats.TP;
        maxTP = PlayerStats.maxTP;
        regenRate = PlayerStats.regenRateTP;
        StartCoroutine(RestoreRoutine());
    }

    // void Update()
    // {
    //     if (firstRun)
    //     {
    //         string value = PlayerStats.TP.ToString();
    //         Debug.Log("TP is: " + value);
    //         totalTP = PlayerStats.TP;
    //         maxTP = PlayerStats.maxTP;
    //         regenRate = PlayerStats.regenRateTP;
    //         Debug.Log("FIRST RUN");
    //         firstRun = false;
    //     }
    // }

    // This is what makes the TP actually regenerate
    IEnumerator RestoreRoutine()
    {
        Debug.Log(totalTP);
        Debug.Log(maxTP);
        UpdateTimer();
        UpdateTP();
        regenerating = true;

        while (totalTP < maxTP)
        {
            DateTime currentTime = DateTime.Now;
            DateTime counter = nextRegenTime;
            bool isAdding = false;

            while (currentTime > counter)
            {
                if (totalTP < maxTP)
                {
                    isAdding = true;
                    totalTP += regenTPAmount;
                    DateTime timeToAdd = lastRegenTime > counter ? lastRegenTime : counter;
                    counter = AddDuration(timeToAdd, regenRate);
                }
                else
                {
                    
                    break;
                }
            }

            if (isAdding)
            {
                lastRegenTime = DateTime.Now;
                nextRegenTime = counter;
            }

            UpdateTimer();
            UpdateTP();
            Save();
            yield return null;
        }

        regenerating = false;
    }

    private void UpdateTimer()
    {
        if (totalTP >= maxTP)
        {
            return;
        }

        TimeSpan t = nextRegenTime - DateTime.Now;
        string value = String.Format("{0}:{1:D2}:{2:D2}", t.TotalHours, t.Minutes, t.Seconds);
        // textTimer.text = value;
    }

    public void UpdateTP()
    {
        // Debug.Log("UpdateTP ran");
        PlayerStats.TP = totalTP;
    }

    private DateTime AddDuration(DateTime time, int duration)
    {
        return time.AddSeconds(duration);
    }

    private DateTime StringToDate(string date)
    {
        if(String.IsNullOrEmpty(date))
            return DateTime.Now;

        return DateTime.Parse(date);   
    }

    public void Save()
    {
        PlayerPrefs.SetString("nextRegenTime", nextRegenTime.ToString());
        PlayerPrefs.SetString("lastRegenTime", lastRegenTime.ToString());
    }

    public void Load()
    {
        nextRegenTime = StringToDate(PlayerPrefs.GetString("nextRegenTime"));
        lastRegenTime = StringToDate(PlayerPrefs.GetString("lastRegenTime"));
    }
}
