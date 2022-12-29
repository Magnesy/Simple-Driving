using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Button playButton;
    [SerializeField] private AndroidNotificationHandler androidNotificationHandler;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeDuration;

    private int energy;

    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";

    private void Start() 
    {
        OnApplicationFocus(true);
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        if(!hasFocus) {return;}

        CancelInvoke();
        
        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);

        highScoreText.text = "HighScore : " + highScore;

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);

        string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);

        if (energyReadyString == string.Empty) { return; }

        DateTime energyReady = DateTime.Parse(energyReadyString);

        if (energy == 0)
        {
            if (DateTime.Now > (energyReady.AddSeconds(energyRechargeDuration * 4)))
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
            else
            {
                playButton.interactable = false;
                Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
                Invoke(nameof(EnergyRecharged), (energyReady.AddSeconds(energyRechargeDuration) - DateTime.Now).Seconds);
                Invoke(nameof(EnergyRecharged), (energyReady.AddSeconds(energyRechargeDuration * 2) - DateTime.Now).Seconds);
                Invoke(nameof(EnergyRecharged), (energyReady.AddSeconds(energyRechargeDuration * 3) - DateTime.Now).Seconds);
                Invoke(nameof(EnergyRecharged), (energyReady.AddSeconds(energyRechargeDuration * 4) - DateTime.Now).Seconds);
            }
        }

        if (energy == 1)
        {
            if (DateTime.Now > (energyReady.AddSeconds(energyRechargeDuration * 3)))
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
            else
            {
                Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
                Invoke(nameof(EnergyRecharged), (energyReady.AddSeconds(energyRechargeDuration) - DateTime.Now).Seconds);
                Invoke(nameof(EnergyRecharged), (energyReady.AddSeconds(energyRechargeDuration * 2) - DateTime.Now).Seconds);
                Invoke(nameof(EnergyRecharged), (energyReady.AddSeconds(energyRechargeDuration * 3) - DateTime.Now).Seconds);
            }
        }

        if (energy == 2)
        {
            if (DateTime.Now > (energyReady.AddSeconds(energyRechargeDuration * 2)))
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
            else
            {
                Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
                Invoke(nameof(EnergyRecharged), (energyReady.AddSeconds(energyRechargeDuration) - DateTime.Now).Seconds);
                Invoke(nameof(EnergyRecharged), (energyReady.AddSeconds(energyRechargeDuration * 2) - DateTime.Now).Seconds);
            }
        }

        if (energy == 3)
        {
            if (DateTime.Now > (energyReady.AddSeconds(energyRechargeDuration)))
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
            else
            {
                Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
                Invoke(nameof(EnergyRecharged), (energyReady.AddSeconds(energyRechargeDuration) - DateTime.Now).Seconds);
            }
        }

        if (energy == 4)
        {
            if (DateTime.Now > (energyReady))
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
            else
            {
                Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
            }
        }

        energyText.text = "PLAY ( " + energy.ToString() + " Energy Left )";
    }

    private void EnergyRecharged()
    {
        playButton.interactable = true;
        energy++;
        PlayerPrefs.SetInt(EnergyKey, energy);
        energyText.text = "PLAY ( " + energy.ToString() + " Energy Left )";
    }

    public void Play()
    {
        if (energy < 1) { return; }

        energy--;

        PlayerPrefs.SetInt(EnergyKey, energy);

        if (energy < maxEnergy)
        {
            DateTime energyReady = DateTime.Now.AddMinutes(energyRechargeDuration);
            PlayerPrefs.SetString(EnergyReadyKey, energyReady.ToString());
#if UNITY_ANDROID
            androidNotificationHandler.ScheduleNotification(energyReady);
#endif
        }

        SceneManager.LoadScene(1);
    }
}
