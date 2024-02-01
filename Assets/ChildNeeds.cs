using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildNeeds : MonoBehaviour
{

    [SerializeField] Image imageNeeds;
    [SerializeField] ChargeBar panicBar;
    float currentNeedTimer = 0.0f;
    float maxCurrentNeedTime = 5.5f;
    bool isSick = false;
    bool PillsPickedUp = false;
    public static Action<bool> OnChildSick;

    private void Start()
    {
        imageNeeds.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isSick)
        {
            currentNeedTimer += Time.deltaTime;

            if(currentNeedTimer > maxCurrentNeedTime)
            {
                currentNeedTimer = 0.0f;

                panicBar.ModifCharge(2.5f);
            }
        }
        else
        {
            currentNeedTimer = 0.0f;
        }
    }

    private void OnEnable()
    {
        Timer.TwoMinutesPassed += HealthNeedsOnGirl;
        Pills.OnPillsPickedUp += SetPillsPickedUp;
    }

    private void OnDisable()
    {
        Timer.TwoMinutesPassed -= HealthNeedsOnGirl;
        Pills.OnPillsPickedUp -= SetPillsPickedUp;
    }

    private void HealthNeedsOnGirl()
    {
        if (imageNeeds != null) 
        {
            imageNeeds.gameObject.SetActive(true);
            isSick = true;
            OnChildSick?.Invoke(true);
        }
    }

    private void SetPillsPickedUp()
    {
        PillsPickedUp = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(PillsPickedUp)
            {
                imageNeeds.gameObject.SetActive(false);
                isSick = false;
                PillsPickedUp = false;
                OnChildSick?.Invoke(false);
            }
        }
    }
}
