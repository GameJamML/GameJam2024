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
    public Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Baby");
        initialspeed = enemy.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (cached == false)
        {
            enemy.SetDestination(player.transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            EnemyDeadEvent();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            Caught(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            Caught(false);
            enemy.speed = initialspeed;

        }
    }

    void Caught(bool caught)
    {
        cached = caught;
        enemy.isStopped = caught;

        if (caught == true)
        {
            //gameObject.transform.position = new Vector3(0, 0, 0);
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    
}
