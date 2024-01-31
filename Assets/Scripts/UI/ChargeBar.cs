using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChargeBar : MonoBehaviour
{
    public Image barraDeCarrega;
    [SerializeField] private float maxCharge = 100;

    [SerializeField] private float actualCharge = 0;

    public float ActualHP { get => actualCharge; set => actualCharge = value; }

    private void Start()
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
            actualCharge += amount;
        else
            EndGameCharge();

        FillBar();

        return;
    }

    public void EndGameCharge()
    {
        SceneManager.LoadScene("EndScene");
    }
}
