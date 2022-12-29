using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text speedText;
    private float speed;
    
    void Update()
    {
        //score += Time.deltaTime;
        speed = Car.speed * 10;
        speedText.text = "Speed : " + Mathf.FloorToInt(speed).ToString();
    }
}
