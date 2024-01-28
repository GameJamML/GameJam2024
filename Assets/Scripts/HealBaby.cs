using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealBaby : MonoBehaviour
{
    public GameObject healText;
    // Start is called before the first frame update
    void Start()
    {
        healText.gameObject.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     ModifCharge(20);
        // }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            healText.gameObject.SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            healText.gameObject.SetActive(false);
        }
    }

}
