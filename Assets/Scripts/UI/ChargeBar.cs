using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChargeBar : MonoBehaviour
{
    public Image barraDeCarrega;
    [SerializeField] private float maxCharge = 100;
    public float actualCharge = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       FillBar();
    }

    private void FillBar()
    {
        barraDeCarrega.fillAmount = actualCharge / maxCharge;
    }

    public void ModifCharge(float amount)
    {
        if (actualCharge <= maxCharge)
        {
            actualCharge += amount;
        }
        else
        {
            EndGameCharge();
        }

        return;
    }
    
    public void EndGameCharge()
    {
        SceneManager.LoadScene("EndScene");
    }
    
    public float ActualHP()
    {
        return actualCharge;
    }
}
