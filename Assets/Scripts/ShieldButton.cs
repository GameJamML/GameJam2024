using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private float timer = 0;

    [SerializeField] private float cooldownButton;
    [SerializeField] private TextMeshProUGUI _canInteractText;

    private bool resetButton = false;

    void Start()
    {
        ShieldBarGO.SetActive(false);
        buttonLight = gameObject.GetComponentInChildren<Light>();
        pCylinder2_initialPosition = button_pCylinder2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canStopped == true && button_pCylinder2.transform.position == pCylinder2_initialPosition)
        {
            StartButton();
        }

        if (resetButton == true)
        {
            timer += Time.deltaTime;

            if (timer >= cooldownButton)
            {
                shieldStopped = true;
                canStopped = false;
                button_pCylinder2.transform.position = pCylinder2_initialPosition;
                buttonLight.color = new Color(1, 0, 0);
                timer = 0f;
                resetButton = false; 
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Player"))
        {
            _canInteractText.gameObject.SetActive(true);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canStopped == false)
        {
            canStopped = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            _canInteractText.gameObject.SetActive(false);
        }

    }


    public void ResetButton()
    {
        resetButton = true;
        ShieldBarGO.SetActive(false);

    }
    
    private void StartButton()
    {
        button_pCylinder2.transform.localPosition = new Vector3(0, 0.071f, 0);
        buttonLight.color = new Color(0, 1, 0);
        shieldGO.ActiveShield(true);
        shieldStopped = false;
        canStopped = false;
        ShieldBarGO.SetActive(true);
        _canInteractText.gameObject.SetActive(false);
    }

}
