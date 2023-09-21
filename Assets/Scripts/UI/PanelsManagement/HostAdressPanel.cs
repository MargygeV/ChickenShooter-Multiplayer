using UnityEngine;
using TMPro;
using Mirror;

public class HostAdressPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _serverText;
    [SerializeField] private TMP_Text _adressText;

    private NetworkManager _manager;

    private void Start()
    {
        _manager = NetworkManager.singleton;
        DisplayText();
    }

    private void DisplayText()
    {
        if (NetworkServer.active)
        {
            _serverText.text = "Transport: " + Transport.active;
        }
        if (NetworkClient.isConnected)
        {
            _adressText.text = "ip:" + _manager.networkAddress;
        }
    }
}
