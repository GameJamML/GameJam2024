using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovment : MonoBehaviour
{
    public NavMeshAgent enemy;
    private GameObject baby;

    public static Action EnemyDeadEvent;
    private bool cached = false;
    private bool sleep = false;
    public float rotationSpeed;
    private float initialspeed;
    private EnemyGenerator enemyGenerator;

    // When has caught
    private Transform _mirrorTrans;
    private bool _finishedCaughtAnim = false;

    // Start is called before the first frame update
    void Start()
    {
        baby = GameObject.FindGameObjectWithTag("Baby");
        initialspeed = enemy.speed;
        enemyGenerator = gameObject.GetComponentInParent<EnemyGenerator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            transform.Rotate(45f, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!cached || !sleep)
        {
            enemy.SetDestination(baby.transform.position);
        }
        else
        {
            if (_finishedCaughtAnim)
            {
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnemyDeadEvent?.Invoke();
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
        _finishedCaughtAnim = false;

        _mirrorTrans = mirrorTrans;

        cached = true;
        enemy.speed = 0;
        enemy.isStopped = true;
        enemy.enabled = false;

        // In coroutine
        StartCoroutine(CaughtAnim());
    }

    public void EnemyEscaped()
    {
        cached = false;
        enemy.enabled = true;
        enemy.isStopped = false;
        enemy.speed = initialspeed;
    }

    public void KillEnemy()
    {
        gameObject.SetActive(false);
        enemyGenerator.pullEnemies.Add(gameObject);
    }
    public void SleepEnemy()
    {
        if (cached == false)
        {
            enemy.speed = 0;
            enemy.isStopped = true;
            sleep = true;
        }
    }
    
    public void AwakeEnemy()
    {
        enemy.speed = initialspeed;
        enemy.isStopped = false;
        sleep = false;
    }

}
