using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeerController : MonoBehaviour
{
    //Beer Cooldown
    float enemyTillNextBeer = 8.0f;
    float currentCount = 0.0f;

    //UI related
    [SerializeField] Slider nextBeerSlider;
    public bool beerPicked = false;
    private BeerMechanic beerMechanic;
    [SerializeField] GameObject BeerPickUp;
    FadeInFadeOutUI fadeOutUI;

    private void Start()
    {
        beerMechanic = GameObject.FindGameObjectWithTag("Player").GetComponent<BeerMechanic>();
        fadeOutUI = nextBeerSlider.GetComponent<FadeInFadeOutUI>();
        fadeOutUI.HideUIQuickly();
        nextBeerSlider.minValue = currentCount;
        nextBeerSlider.maxValue = 1.0f;
    }

    private void Update()
    {

        if (BeerPickUp.activeSelf == false)
        {
            fadeOutUI.fadeIn = true;
            fadeOutUI.fadeOut = false;

            if (currentCount < enemyTillNextBeer)
            {

                float sliderNormalized = currentCount / enemyTillNextBeer;

                nextBeerSlider.value = sliderNormalized;

            }
            else
            {
                currentCount = 0.0f;
                nextBeerSlider.value = 0.0f;
                BeerPickUp.SetActive(true);
            }

        }
        else
        {
            fadeOutUI.fadeOut = true;
            fadeOutUI.fadeIn = false;
        }

    }
    private void OnEnable()
    {
        EnemyMovment.EnemyDeadEvent += OnEnemyKilled;
    }

    private void OnDisable()
    {
        EnemyMovment.EnemyDeadEvent -= OnEnemyKilled;
    }

    public void OnEnemyKilled()
    {
        if (BeerPickUp.activeSelf) return;

        currentCount++;

    }

}
