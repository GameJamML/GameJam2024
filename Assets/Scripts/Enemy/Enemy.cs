using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovment : MonoBehaviour
{
    public NavMeshAgent enemy;
    private GameObject baby;
    
    public static Action EnemyDeadEvent;
    private bool cached = false;
    public float rotationSpeed;
    private float initialspeed;
    private EnemyGenerator enemyGenerator;

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
        if (cached == false)
        {
            enemy.SetDestination(baby.transform.position);
        }
        else
        {
            //gameObject.transform.position = new Vector3(0, 0, 0);

            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnemyDeadEvent?.Invoke();
        }
    }

    public void EnemyCatched()
    {
        cached = true;
        enemy.isStopped = true;
    }

    public void EnemyEscaped()
    {
        cached = false;
        enemy.isStopped = false;
        enemy.speed = initialspeed;
    }

    public void KillEnemy()
    {
        gameObject.SetActive(false);
        enemyGenerator.pullEnemies.Add(gameObject);
    }

}
