using UnityEngine;

public class BabyScript : MonoBehaviour
{
    public ChargeBar panicBar;
    [SerializeField] private float chargeValue;
    [SerializeField] private float enemyCooldown;
    public Shield shield;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().AtackEnemy();
            panicBar.ModifCharge(chargeValue);
        }
        
  
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && shield.shieldActive == false)
        {
            if (other.gameObject.GetComponent<Enemy>().atack == true)
            {
                
                panicBar.ModifCharge(chargeValue);
                other.gameObject.GetComponent<Enemy>().atack = false;
            }
        }
    }
}
