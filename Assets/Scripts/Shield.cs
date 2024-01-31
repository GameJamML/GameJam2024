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
            other.gameObject.GetComponent<Enemy>().AtackEnemy();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<Enemy>().atack == true)
            {
                shieldBar.ModifCharge(-hpReducer);
                other.gameObject.GetComponent<Enemy>().atack = false;
            }
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
