using System;
using UnityEngine;

public static class GameData
{
    public static void SaveData()
    {
        PlayerPrefs.SetString("lastPlayed", DateTime.Now.ToBinary().ToString());
        PlayerPrefs.SetFloat("food", Needs.food);
        PlayerPrefs.SetFloat("happiness", Needs.happiness);
        PlayerPrefs.SetFloat("sleep", Needs.sleep);
        PlayerPrefs.Save();
    }

    public static void LoadData()
    {
        if (PlayerPrefs.HasKey("lastPlayed"))
        {
            long binaryTime = Convert.ToInt64(PlayerPrefs.GetString("lastPlayed"));
            DateTime savedTime = DateTime.FromBinary(binaryTime);
            DateTime currentTime = DateTime.Now;
            TimeSpan timePassed = currentTime - savedTime;

            double hoursPassed = timePassed.TotalHours;

            float foodFromLastSession = PlayerPrefs.GetFloat("food");
            float happinessFromLastSession = PlayerPrefs.GetFloat("happiness");
            float sleepFromLastSession = PlayerPrefs.GetFloat("sleep");

            Needs.food = Mathf.Clamp(foodFromLastSession - (float)(hoursPassed * 2f), 0f, 10f);
            Needs.happiness = Mathf.Clamp(happinessFromLastSession - (float)(hoursPassed * 2f), 0f, 10f);
            Needs.sleep = Mathf.Clamp(sleepFromLastSession + (float)(hoursPassed * 2f), 0f, 10f);
        }
        else
        {
            Needs.food = 5f;
            Needs.happiness = 5f;
            Needs.sleep = 5f;
        }
    }
}
