using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject button_pCylinder2;
    public GameObject ShieldBarGO;
    public Shield shieldGO;
    private Light buttonLight;
    private Vector3 pCylinder2_initialPosition;
    private bool canStopped = false;
    [HideInInspector] public bool shieldStopped = true;
    void Start()
    {
        ShieldBarGO.SetActive(false);
        buttonLight = gameObject.GetComponentInChildren<Light>();
        pCylinder2_initialPosition = button_pCylinder2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && canStopped == true)
        {
            StartButton();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canStopped = true;
        }
    }


    public void ResetButton()
    {
        shieldStopped = true;
        canStopped = true;
        ShieldBarGO.SetActive(false);
        button_pCylinder2.transform.position = pCylinder2_initialPosition;
        buttonLight.color = new Color(1, 0, 0);
    }
    
    private void StartButton()
    {
        button_pCylinder2.transform.position = new Vector3(0, 0.1f, 0);
        buttonLight.color = new Color(0, 1, 0);
        shieldGO.ActiveShield(true);
        shieldStopped = false;
        canStopped = false;
        ShieldBarGO.SetActive(true);
    }

}
