using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeerPickUp : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BeerMechanic bm = other.gameObject.GetComponent<BeerMechanic>();

            if (bm != null && bm.beerPickedUp == false) 
            {
                bm.beerPickedUp = true;

                gameObject.SetActive(false);
            }
            else
            {

            }
        }
    }
}
