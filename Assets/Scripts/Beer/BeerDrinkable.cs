using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeerDrinkable : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI healText;
    bool biberonPickedUp = false;
    bool drinkable = false;

    private void Update()
    {
        if (healText != null) 
        {
            if(!biberonPickedUp && !drinkable) 
            {
                healText.text = "";
            }
            else if(biberonPickedUp && drinkable)
            {
                healText.text = "Hold E for Calm Baby with the baby bottle\r\n";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BeerMechanic bm = other.gameObject.GetComponent<BeerMechanic>();

            if (bm != null)
            {
                bm.beerDrinkable = true;
                biberonPickedUp = bm.beerPickedUp;
                drinkable = true;
            }
            else
            {
                Debug.Log("BeerMechanic NOT FOUND ON PLAYER");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BeerMechanic bm = other.gameObject.GetComponent<BeerMechanic>();

            if (bm != null)
            {
                bm.beerDrinkable = false;
                biberonPickedUp = bm.beerPickedUp;
                drinkable = false;
            }
            else
            {
                Debug.Log("BeerMechanic NOT FOUND ON PLAYER");
            }
        }
    }
}
