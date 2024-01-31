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

    //Heal
    float healingPerSecond = -4.5f;
    [SerializeField] ChargeBar panicBar;

    //Beer Throw Related
    [SerializeField] GameObject beerThrownPrefab;
    Vector3 forwardDir = Vector3.zero;

    void Start()
    {
        consumeSlider.minValue = 0.0f;
        consumeSlider.maxValue = maxTimeToConsume;
        fadeOutUI = consumeSlider.GetComponent<FadeInFadeOutUI>();
        fadeOutUI.HideUIQuickly();
    }

    void Update()
    {
        if(beerPickedUp) 
        {
            if(beerDrinkable)
            {
                if (Input.GetKey(KeyCode.F))
                {
                    if (!beerEmpty)
                        BeerHealingLogic();
                }
                else if (Input.GetKeyUp(KeyCode.F))
                {
                    fadeOutUI.fadeOut = true;
                    fadeOutUI.fadeIn = false;
                }
            } 

            if(Input.GetMouseButtonDown(0))
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
        panicBar.ModifCharge(healperFrame);
    }

    void BeerThrowingLogic()
    {
        forwardDir = gameObject.transform.forward;

        GameObject beer = Instantiate(beerThrownPrefab, gameObject.transform.position + forwardDir, Quaternion.identity);
        beer.GetComponent<BeerThrow>().GatherInfo(forwardDir);

        beerPickedUp = false;
        beerEmpty = false;
    }
}
