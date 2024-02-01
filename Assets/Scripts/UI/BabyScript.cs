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
            other.gameObject.GetComponent<Enemy>().AtackEnemy(GetHit);
        }
    }

    public void GetHit()
    {
        panicBar.ModifCharge(chargeValue);
    }
}
