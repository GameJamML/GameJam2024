using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    public ChargeBar shieldBar;
    public ShieldButton shieldButton;
    [SerializeField] private float hpReducer;
    [HideInInspector] public bool shieldActive;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (shieldBar.ActualHP() <= 0)
        {
            ActiveShield(false);
            shieldButton.ResetButton();
            shieldBar.actualCharge = 100;
            shieldActive = false;
        }
        else if (shieldButton.shieldStopped == false)
        {
            ActiveShield(true);
            shieldActive = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().AtackEnemyShield(GetHit);
        }
    }

    public void ActiveShield(bool active)
    {
        if (active == true)
        {
            gameObject.SetActive(true);
        }
        else
        {

            gameObject.SetActive(false);
        }
    }

    public bool GetHit()
    {
        shieldBar.ModifCharge(-hpReducer);
        
        if (shieldBar.ActualHP() <= 0)
        {
            return false;
        }

        return true;
    
    }
}
