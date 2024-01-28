using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerDrinkable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BeerMechanic bm = other.gameObject.GetComponent<BeerMechanic>();

            if (bm != null)
            {
                bm.beerDrinkable = true;

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

            }
            else
            {
                Debug.Log("BeerMechanic NOT FOUND ON PLAYER");
            }
        }
    }
}
