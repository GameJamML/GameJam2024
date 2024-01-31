using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    public ChargeBar shieldBar;
    public ShieldButton shieldButton;
    [SerializeField] private float hpReducer;
    
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
        }
        else if (shieldButton.shieldStopped == false)
        {
            ActiveShield(true);
        }
    }


    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            shieldBar.ModifCharge(-hpReducer);
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
}
