using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    public ChargeBar shieldBar;
    public ShieldButton shieldButton;
    [SerializeField] private float hpReducer;
    [HideInInspector] public bool shieldActive;

    void Update()
    {
        if (shieldButton.shieldStopped == false)
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
        
        if (shieldBar.ActualHP <= 0)
        {
            ActiveShield(false);
            shieldButton.ResetButton();
            shieldBar.ActualHP = 100;
            shieldActive = false;

            return false;
        }
        return true;
    }
}
