using UnityEngine;
using TMPro;

public class MoneyBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _textField;

    private void OnEnable()
    {
        Player.MoneyChanged += MoneyChanger;
    }

    private void OnDisable()
    {
        Player.MoneyChanged -= MoneyChanger;
    }

    private void MoneyChanger(int money)
    {
        _textField.text = "" + money;
    }
}
