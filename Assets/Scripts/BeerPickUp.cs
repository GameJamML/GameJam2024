using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeerPickUp : MonoBehaviour
{

    uint enemyTillNextBeer = 5;
    uint currentCount = 0;
    [SerializeField]Slider nextBeer;

    private void OnEnable()
    {
        EnemyMovment.EnemyDeadEvent += OnEnemyKilled;
    }

    private void OnDisable()
    {
        EnemyMovment.EnemyDeadEvent -= OnEnemyKilled;
    }

    private void Start()
    {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        


    }

    public void OnEnemyKilled()
    {
        if(currentCount < enemyTillNextBeer) 
        {
            currentCount++;
            Debug.Log("EnemyDEAD");
        }
        else
        {
            currentCount = 0;
            if(gameObject.activeSelf == false) 
            {
                gameObject.SetActive(true);
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
                bm.beerPickedUp = true;

                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("BeerMechanic NOT FOUND ON PLAYER");
            }
        }
    }
}
