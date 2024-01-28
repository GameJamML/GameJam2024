using UnityEngine;

public class PlayerAttackRange : MonoBehaviour
{
    private GameObject _firstContactEnemy = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _firstContactEnemy = other.gameObject;
            _firstContactEnemy.SendMessage("EnemyCatched");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_firstContactEnemy!= null && other.gameObject == _firstContactEnemy)
        {
            _firstContactEnemy = null;
            _firstContactEnemy.SendMessage("EnemyEscaped");
        }
    }

    private void OnDisable()
    {
        if (_firstContactEnemy != null)
        {
            _firstContactEnemy = null;
            _firstContactEnemy.SendMessage("EnemyEscaped");
        }
    }
}