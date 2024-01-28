using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovment : MonoBehaviour
{
    public NavMeshAgent enemy;
    private GameObject player;
    public delegate void OnEnemyDeath();
    public static event OnEnemyDeath EnemyDeadEvent;
    private bool cached = false;
    public float rotationSpeed;
    private float initialspeed;
    private EnemyGenerator enemyGenerator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Baby");
        initialspeed = enemy.speed;
        enemyGenerator = gameObject.GetComponentInParent<EnemyGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cached == false)
        {
            enemy.SetDestination(player.transform.position);
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
            if (EnemyDeadEvent != null)
            {
                EnemyDeadEvent();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enemy trigger");
    }
    public void EnemyCatched()
    {
        cached = true;
        enemy.isStopped = true;
        Debug.Log("catched");
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
