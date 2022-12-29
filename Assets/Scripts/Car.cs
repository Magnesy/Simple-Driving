using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] public static float speed = 8f;
    [SerializeField] private float speedGain = 0.2f;
    [SerializeField] private float turnSpeed = 10f;

    private int steerValue;

    void Start() 
    {
        speed = 8f;
    }

    
    void Update()
    {
        speed += speedGain * Time.deltaTime;
        transform.Rotate(0f,steerValue * turnSpeed * Time.deltaTime,0f);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void Steer(int value)
    {
        steerValue = value;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(0); // 0 numara Scene_MainMenu ye denk geliyor.
        }
    }
}
