using UnityEngine;

public class PlayerAttackRange : MonoBehaviour
{
    private GameObject _firstContactEnemy = null;
    private EnemyCatch _enemyCatch;
    private PlayerAttack _playerAttack;

    private void Start()
    {
        _enemyCatch = FindAnyObjectByType<EnemyCatch>();
        _enemyCatch.endToCatch += CatchEnd;

        _playerAttack = GetComponentInParent<PlayerAttack>();
    }

    private void OnDestroy()
    {
        _enemyCatch.endToCatch -= CatchEnd;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_firstContactEnemy != null)
            return;

        if (other.CompareTag("Enemy"))
        {
            _firstContactEnemy = other.gameObject;
            _firstContactEnemy.SendMessage("EnemyCatched", transform);
            _enemyCatch.StartToCatch();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_firstContactEnemy != null && other.gameObject == _firstContactEnemy)
        {
            _firstContactEnemy.SendMessage("EnemyEscaped");
            _firstContactEnemy = null;
            _enemyCatch.CatchFaild();
        }
    }

    private void OnDisable()
    {
        if (_firstContactEnemy != null)
        {
            _firstContactEnemy.SendMessage("EnemyEscaped");
            _firstContactEnemy = null;
            _enemyCatch.CatchFaild();
        }
    }

    private void CatchEnd(bool successful)
    {
        if (_firstContactEnemy == null)
            return;

        if (successful)
        {
            _firstContactEnemy.SendMessage("KillEnemy", true);
        }
        else
        {
            _firstContactEnemy.SendMessage("EnemyEscaped");
        }

        _firstContactEnemy = null;

        _playerAttack.BreakAttack();
    }
}
