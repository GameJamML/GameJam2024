using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    public Image barraDeCarrega;
    private float maxCharge = 100;
    [SerializeField]private float actualCharge;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // if (Input.GetKeyDown(KeyCode.Space))
       // {
       //     ModifCharge(20);
       // }

        FillBar();
    }

    private void FillBar()
    {
        barraDeCarrega.fillAmount = actualCharge / maxCharge;
    }

    public void ModifCharge(float amount)
    {
        if (actualCharge != 100)
        {
            actualCharge += amount;
        }

        return;
    }
}
