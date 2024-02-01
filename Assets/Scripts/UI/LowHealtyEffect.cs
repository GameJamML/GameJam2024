using UnityEngine;

public class LowHealtyEffect : MonoBehaviour
{
    [SerializeField] private ChargeBar _hpChargeBar;
    [SerializeField] private PanelManager _panelManager;

    private void Start()
    {
        if (_hpChargeBar == null)
            _hpChargeBar = GetComponent<ChargeBar>();

        if (_panelManager == null)
            _panelManager = FindAnyObjectByType<PanelManager>();

        _hpChargeBar._hpChangeEvent += (OnHpChange);
    }

    private void OnDestroy()
    {
        _hpChargeBar._hpChangeEvent -= (OnHpChange);
    }

    private void OnHpChange(float hp)
    {
        if (hp >= 80)
        {
            _panelManager.StartFlashEvent(Color.red);
        }
        else
        {
            _panelManager.CloseFlashEvent();
        }
    }
}
