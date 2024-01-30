using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildNeeds : MonoBehaviour
{

    Image imageNeeds;

    private void OnEnable()
    {

        Timer.MinutePassed += DrawNeedsOnGirl;

    }

    private void OnDisable()
    {
        Timer.MinutePassed -= DrawNeedsOnGirl;
    }

    private void DrawNeedsOnGirl()
    {
        if (imageNeeds != null) 
        {
            
        }
    }

}
