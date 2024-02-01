using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] float minutes = 5.0f;
    private float currentTimePassed = 0.0f;
    private float minutePassed = 0.0f;
    private float twoMinutes = 0.0f;
    public static Action MinutePassed;
    public static Action TwoMinutesPassed;

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
        twoMinutes += Time.deltaTime;

        if (minutePassed >= 60)
        {
            minutePassed = 0;
            MinutePassed?.Invoke();
            AudioManager.Instace.PlayerSFX(AudioType.Clock);
        }
        if(twoMinutes >= 120) 
        {
            twoMinutes = 0;
            TwoMinutesPassed?.Invoke();
        }
        if(SecToMin(currentTimePassed) >= minutes)
        {
           
            SceneManager.LoadScene("WinScene");

        }
    }

}
