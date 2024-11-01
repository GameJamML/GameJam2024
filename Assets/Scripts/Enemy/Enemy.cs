using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent enemy;
    private GameObject baby;

    public static Action EnemyDeadEvent;
    private bool caught = false;
    private bool sleep = false;
    public float rotationSpeed;
    private float initialspeed;
    private EnemyGenerator enemyGenerator;
    private Animator animator;
    // When has caught
    private Transform _mirrorTrans;
    private bool _finishedCaughtAnim = false;
    private bool atack = false;
    private bool startAtack = false;
    private float timer = 0;
    [SerializeField] private float cooldownAtack;

    private Action _hitToBaby;
    private Func<bool> _hitToShield;

    public bool Atack { get { if (sleep) return false; else return atack; } set => atack = value; }
    void Start()
    {
        animator = GetComponent<Animator>();
        baby = GameObject.FindGameObjectWithTag("Baby");
        initialspeed = enemy.speed;

        if (enemyGenerator == null)
            enemyGenerator = gameObject.GetComponentInParent<EnemyGenerator>();

        caught = false;
    }

    private void OnEnable()
    {
        if (enemyGenerator == null)
            enemyGenerator = gameObject.GetComponentInParent<EnemyGenerator>();

        enemyGenerator.awakeEvent += AwakeEnemy;
        enemyGenerator.sleepEvent += SleepEnemy;
    }

    private void OnDisable()
    {
        enemyGenerator.awakeEvent -= AwakeEnemy;
        enemyGenerator.sleepEvent -= SleepEnemy;
    }

    void Update()
    {
        if (!gameObject.activeSelf)
            return;

        if ((!caught || !sleep) && enemy.enabled)
        {
            enemy.SetDestination(baby.transform.position);
        }
        else if (_finishedCaughtAnim)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (startAtack && !sleep && !caught)
        {
            timer += Time.deltaTime;

            if (timer >= cooldownAtack)
            {
                if (_hitToShield != null)
                {
                    if (!_hitToShield.Invoke())
                        _hitToShield = null;
                }
                else
                {
                    _hitToBaby?.Invoke();
                }
                timer = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
            transform.Rotate(45f, 0, 0);

        if (other.gameObject.CompareTag("Biberon"))
        {
            EnemyDeadEvent?.Invoke();

            gameObject.SetActive(false);
            enemyGenerator.pullEnemies.Add(gameObject);
        }
    }

    public IEnumerator CaughtAnim()
    {
        Vector3 beginPos = transform.position;
        Vector3 endPos = _mirrorTrans.position;

        Quaternion beginRot = transform.rotation;
        Quaternion endRot = Quaternion.LookRotation(Vector3.up, _mirrorTrans.up);

        for (float t = 0; t < 1; t += Time.deltaTime * 2.5f)
        {
            transform.SetPositionAndRotation(Vector3.Lerp(beginPos, endPos, t), Quaternion.Slerp(beginRot, endRot, t));

            yield return null;
        }

        _finishedCaughtAnim = true;
    }

    public void EnemyCatched(Transform mirrorTrans)
    {
        animator.SetBool("Suction", true);
        _finishedCaughtAnim = false;

        _mirrorTrans = mirrorTrans;

        caught = true;
        enemy.speed = 0;
        enemy.isStopped = true;
        enemy.enabled = false;

        enemyGenerator.SleepAllEnemies();

        // In coroutine
        StartCoroutine(CaughtAnim());
    }

    public void EnemyEscaped()
    {
        animator.SetBool("Suction", false);
        caught = false;
        enemy.enabled = true;
        enemy.isStopped = false;
        enemy.speed = initialspeed;

        enemyGenerator.AwakeAllEnemies();
    }

    public void KillEnemy(bool killedByPlayer = true)
    {
        animator.SetBool("Suction", false);
        caught = false;
        enemy.enabled = true;
        enemy.isStopped = false;
        enemy.speed = initialspeed;
        startAtack = false;
        ResetAtack();

        gameObject.SetActive(false);
        enemyGenerator.pullEnemies.Add(gameObject);

        if (killedByPlayer)
        {
            EnemyDeadEvent?.Invoke();
            enemyGenerator.AwakeAllEnemies();
        }
    }

    public void SleepEnemy()
    {
        if (!gameObject.activeSelf)
            return;

        if (caught == false)
        {
            enemy.speed = 0;
            enemy.isStopped = true;
            sleep = true;
        }
    }

    public void AwakeEnemy()
    {
        if (!gameObject.activeSelf || !enemy.enabled)
            return;

        enemy.speed = initialspeed;
        enemy.isStopped = false;
        sleep = false;
    }

    public void AtackEnemy(Action hitToBaby)
    {
        if (_hitToBaby == null)
        {
            _hitToBaby = hitToBaby;
        }
        startAtack = true;
    }

    public void AtackEnemyShield(Func<bool> hitToShield)
    {
        if (_hitToShield == null)
        {
            _hitToShield = hitToShield;
        }
        startAtack = true;
    }

    private void ResetAtack()
    {
        startAtack = false;
        atack = false;
        timer = 0f;
    }

    private void OnDestroy()
    {
        EnemyDeadEvent = null;
        _hitToBaby = null;
        _hitToShield = null;
    }
}
