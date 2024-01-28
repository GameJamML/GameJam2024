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
    bool cached = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Baby");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
                Caught(true);
            Debug.Log("soc dins");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            Caught(false);
            Debug.Log("soc Out");
        }
    }

    void Caught(bool caught)
    {
        cached = caught;
        if (caught == true)
        {
            gameObject.transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
        }

    }

}
