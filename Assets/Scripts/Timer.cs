using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] float minutes = 5.0f;
    private float currentTimePassed = 0.0f;
    private float minutePassed = 0.0f;
    public static Action MinutePassed;
    
    float MinToSec(float value)
    {
        return value * 60;
    }

    float SecToMin(float value)
    {
        return value / 60;
    }

    private void Update()
    {
        currentTimePassed += Time.deltaTime;
        minutePassed += Time.deltaTime;

        if (minutePassed >= 10)
        {
            minutePassed = 0;
            MinutePassed?.Invoke();
            AudioManager.Instace.PlayerSFX(AudioType.Clock);
        }
        if(SecToMin(currentTimePassed) >= minutes)
        {
           
            SceneManager.LoadScene("EndScene");

        }
    }

}
