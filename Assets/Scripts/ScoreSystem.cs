using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text speedText;
    private float score;
    public const string HighScoreKey = "HighScore"; //yanlış yazmamak için variable a dönüştürdük.
    
    void Update()
    {
        //score += Time.deltaTime;
        score += Time.deltaTime * Car.speed;
        speedText.text = Mathf.FloorToInt(score).ToString();
    }

    private void OnDestroy() 
    {
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        if(score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey , Mathf.FloorToInt(score));
        }
    }
}
