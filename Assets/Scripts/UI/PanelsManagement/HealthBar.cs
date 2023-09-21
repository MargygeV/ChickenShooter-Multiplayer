using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class HealthBar : NetworkBehaviour
{
    public static HealthBar StaticHealthBar;

    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        Player.HealthChanged += HealthChanger;
    }

    private void OnDisable()
    {
        Player.HealthChanged -= HealthChanger;
    }

     private void HealthChanger(float healthPercent)
    {
        _slider.value = healthPercent;
    }
}