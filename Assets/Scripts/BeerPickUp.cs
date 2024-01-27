using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerPickUp : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BeerMechanic bm = other.gameObject.GetComponent<BeerMechanic>();

            if (bm != null) 
            {
                bm.beerPickedUp = true;

                Destroy(gameObject);
            }
            else
            {
                Debug.Log("BeerMechanic NOT FOUND ON PLAYER");
            }
        }
    }
}
