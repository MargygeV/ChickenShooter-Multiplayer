using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class HostConnect : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputFieldAdress;
    [SerializeField] private TMP_InputField _inputFieldPlayerName;

    [SerializeField] private GameObject _panelStart;
    [SerializeField] private GameObject _panelStop;

    [SerializeField] private Button _buttonHost;
    [SerializeField] private Button _buttonClient;
    [SerializeField] private Button _buttonStop;

    [SerializeField] private TMP_Text _connectText;

    [SerializeField] private PlayerAttributes _playerAttributes;

    private NetworkManager _manager;

    private void Awake()
    {
        _manager = NetworkManager.singleton;

        Init();
    }

    private void OnEnable()
    {
        _inputFieldAdress.onValueChanged.AddListener(delegate { ValueChangeCheck();} );
        _inputFieldPlayerName.onValueChanged.AddListener(delegate { PlayerNicknameChange();} );

        _buttonHost.onClick.AddListener(StartHost);
        _buttonClient.onClick.AddListener(ConnectToHost);
        _buttonStop.onClick.AddListener(StopConnecting);
    }

    private void OnDisable()
    {
        _inputFieldAdress.onValueChanged.RemoveListener(delegate { ValueChangeCheck();} );
        _inputFieldPlayerName.onValueChanged.RemoveListener(delegate { PlayerNicknameChange();} );

        _buttonHost.onClick.RemoveListener(StartHost);
        _buttonClient.onClick.RemoveListener(ConnectToHost);
        _buttonStop.onClick.RemoveListener(StopConnecting);
    }

    private void Init()
    {
        if(_playerAttributes.PlayerName != "")
            _inputFieldPlayerName.text = _playerAttributes.PlayerName;
    }

    private void ValueChangeCheck()
    {
        _manager.networkAddress = _inputFieldAdress.text;
    }

    private void PlayerNicknameChange()
    {
        _playerAttributes.ResetName(_inputFieldPlayerName.text);
    }

    private void StartHost()
    {
        _manager.StartHost();
        SetupCanvas();
    }

    private void ConnectToHost()
    {
        _manager.StartClient();
        SetupCanvas();
    }

    private void StopConnecting()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
            _manager.StopHost();
        else if (NetworkClient.isConnected)
            _manager.StopClient();
        else if (NetworkServer.active)
            _manager.StopServer();

        SetupCanvas();
    }

    private void SetupCanvas()
    {
        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            if (NetworkClient.active)
            {
                _panelStart.SetActive(false);
                _panelStop.SetActive(true);
                _connectText.text = "Connecting to " + _manager.networkAddress + "..";
            }
            else
            {
                _panelStart.SetActive(true);
                _panelStop.SetActive(false);
            }
        }
    }
}
