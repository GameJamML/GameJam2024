using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInFadeOutUI : MonoBehaviour
{

    [SerializeField] private CanvasGroup canvasGroup;
    public bool fadeIn = false;
    public bool fadeOut = false;

    public void HideUIQuickly()
    {
        canvasGroup.alpha = 0.0f;
    }
   
    void Update()
    {
        if(fadeIn) 
        {
            if(canvasGroup.alpha < 1.0f)
            {
                canvasGroup.alpha += Time.deltaTime;

                if (canvasGroup.alpha >= 1.0f)
                {
                    fadeIn = false;
                }
            }
        }
        if (fadeOut)
        {

            canvasGroup.alpha -= Time.deltaTime;

            if (canvasGroup.alpha <= 0.0f)
            {
                fadeOut = false;
            }
            
        }
    }
}
