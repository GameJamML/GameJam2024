using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Television : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyGenerator spawner;
    public Renderer Color;
    private bool canStopped = false;
    private bool Stopped = false;

    private float normalTime = 0;
    [SerializeField] private float MaxTimer;
    void Start()
    {
        //prova
        Color.material.color = new Color(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && canStopped == true)
        {
            spawner.stop = true;
            Color.material.color = new Color(1, 0, 0);//prova
            Stopped = true;
        }
        else if (Stopped == true)
        {
            StopTelevision();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && spawner.stop == false)
        {
            canStopped = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canStopped = false;
        }
    }

    void StopTelevision()
    {
        canStopped = false;
        normalTime += Time.deltaTime;
        if (normalTime >= MaxTimer)
        {
            Stopped = false;
            canStopped = false;
            spawner.stop = false;
            Color.material.color = new Color(0, 1, 0);//prova
            normalTime = 0f;
        }
    }
}
