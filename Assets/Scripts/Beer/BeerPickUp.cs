using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeerPickUp : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _canInteractText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //BeerMechanic bm = other.gameObject.GetComponent<BeerMechanic>();

            //if (bm != null && bm.beerPickedUp == false)
            //{
            //    bm.beerPickedUp = true;

            //    gameObject.SetActive(false);
            //    AudioManager.Instace.PlayerSFX(AudioType.Correctkey);
            //}

            _canInteractText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if(Input.GetKeyDown(KeyCode.E))
            {

                BeerMechanic bm = other.gameObject.GetComponent<BeerMechanic>();

                if (bm != null && bm.beerPickedUp == false)
                {
                    bm.beerPickedUp = true;

                    gameObject.SetActive(false);
                    _canInteractText.gameObject.SetActive(false);
                    AudioManager.Instace.PlayerSFX(AudioType.Correctkey);
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canInteractText.gameObject.SetActive(false);
        }
    }
}
