using UnityEngine;

public class BabyScript : MonoBehaviour
{
    public ChargeBar panicBar;
    [SerializeField] private float chargeValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyMovment>().KillEnemy(false);
            panicBar.ModifCharge(chargeValue);
        }
    }
}
