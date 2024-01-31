using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent enemy;
    private GameObject baby;

    public static Action EnemyDeadEvent;
    private bool cached = false;
    private bool sleep = false;
    public float rotationSpeed;
    private float initialspeed;
    private EnemyGenerator enemyGenerator;
    private Animator animator;
    // When has caught
    private Transform _mirrorTrans;
    private bool _finishedCaughtAnim = false;

    void Start()
    {

        animator = GetComponent<Animator>();
        baby = GameObject.FindGameObjectWithTag("Baby");
        initialspeed = enemy.speed;

        if (enemyGenerator == null)
            enemyGenerator = gameObject.GetComponentInParent<EnemyGenerator>();
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
        if ((!cached || !sleep) && enemy.enabled)
        {
            enemy.SetDestination(baby.transform.position);
        }
        else if (_finishedCaughtAnim)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            transform.Rotate(45f, 0, 0);
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
            transform.rotation = Quaternion.Slerp(beginRot, endRot, t);
            transform.position = Vector3.Lerp(beginPos, endPos, t);

            yield return null;
        }

        _finishedCaughtAnim = true;
    }

    public void EnemyCatched(Transform mirrorTrans)
    {
        animator.SetBool("Suction", true);
        _finishedCaughtAnim = false;

        _mirrorTrans = mirrorTrans;

        cached = true;
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
        cached = false;
        enemy.enabled = true;
        enemy.isStopped = false;
        enemy.speed = initialspeed;

        enemyGenerator.AwakeAllEnemies();
    }

    public void KillEnemy(bool killedByPlayer = true)
    {
        animator.SetBool("Suction", false);
        cached = false;
        enemy.enabled = true;
        enemy.isStopped = false;
        enemy.speed = initialspeed;

        EnemyDeadEvent?.Invoke();

        gameObject.SetActive(false);
        enemyGenerator.pullEnemies.Add(gameObject);

        if (killedByPlayer)
            enemyGenerator.AwakeAllEnemies();
    }

    public void SleepEnemy()
    {
        if (!gameObject.activeSelf)
            return;

        if (cached == false)
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
}
