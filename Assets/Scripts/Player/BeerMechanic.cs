using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeerMechanic : MonoBehaviour
{

    public bool beerPickedUp = false;
    public bool beerDrinkable = false;
    bool beerEmpty = false;

    //Consume Related
    float currentTime = 0.0f;
    float maxTimeToConsume = 5.0f;
    [SerializeField]Slider consumeSlider;
    FadeInFadeOutUI fadeOutUI;
    float healingPerSecond = 7.5f;
    float totalHealing = 0.0f;

    //Beer Throw Related

    [SerializeField] GameObject beerPrefab;
    Vector3 forwardDir = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        consumeSlider.minValue = 0.0f;
        consumeSlider.maxValue = maxTimeToConsume;
        fadeOutUI = consumeSlider.GetComponent<FadeInFadeOutUI>();
        fadeOutUI.HideUIQuickly();
    }

    // Update is called once per frame
    void Update()
    {
        if(beerPickedUp) 
        {
            if(beerDrinkable)
            {
                if (Input.GetKey(KeyCode.F))
                {
                    if (!beerEmpty)
                    {
                        BeerHealingLogic();
                    }
                }
                else if (Input.GetKeyUp(KeyCode.F))
                {
                    fadeOutUI.fadeOut = true;
                    fadeOutUI.fadeIn = false;
                }
            } 

            if(Input.GetKeyDown(KeyCode.Space))
            {
                BeerThrowingLogic();
            }
        }
    }

    void BeerHealingLogic()
    {
        fadeOutUI.fadeIn = true;
        fadeOutUI.fadeOut = false;

        if (currentTime < maxTimeToConsume) 
        {
            currentTime += Time.deltaTime;
            consumeSlider.value = currentTime;

            //Healing per frame
            HealPlayer(healingPerSecond * Time.deltaTime);
        }
        else
        {
            //Beer Finished
            consumeSlider.value = 0.0f;
            beerEmpty = true;

            fadeOutUI.fadeOut = true;
            fadeOutUI.fadeIn = false;
        }
    }

    void HealPlayer(float healperFrame)
    {
        
        Debug.Log("Player Healed" + healperFrame + "hp");
        Debug.Log("Player Healed" + totalHealing + "hp");
        totalHealing += healperFrame;
    }

    void BeerThrowingLogic()
    {

        forwardDir = gameObject.transform.forward;

        GameObject beer = Instantiate(beerPrefab, gameObject.transform.position + forwardDir, Quaternion.identity);
        beer.GetComponent<BeerThrow>().GatherInfo(forwardDir);

        beerPickedUp = false;
        beerEmpty = false;
    }

}
