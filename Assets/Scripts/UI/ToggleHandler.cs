using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleHandler : MonoBehaviour
{

    [SerializeField] private Toggle _toggle;
    [SerializeField] private bool IsSFX = false;

    void Start()
    {
        _toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool value)
    {
        if(IsSFX) 
        {
            if (value)
            {
                AudioManager.Instace.UnMuteSFX();
            }
            else
            {
                AudioManager.Instace.MuteSFX();
            }
        }
        else
        {
            if (value)
            {
                AudioManager.Instace.UnMuteBGM();
            }
            else
            {
                AudioManager.Instace.MuteBGM();
            }
        }
    }
}
