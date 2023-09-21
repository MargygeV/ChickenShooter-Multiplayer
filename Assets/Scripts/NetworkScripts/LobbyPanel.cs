using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class LobbyPanel : NetworkBehaviour
{
    [SerializeField] private Transform UIPLayerHandler;
    [SerializeField] private Button _readyButton;
    [SerializeField] private Button _reloadButton;

    private NetworkRoomManager _roomManager;
    private NetworkRoomPlayer _localNetworkRoomPlayer;
    private PlayerUIPanel[] _playerPanels;
    private bool _readyStatus;

    private void Start()
    {
        _roomManager = NetworkManager.singleton.GetComponent<NetworkRoomManager>();
        _localNetworkRoomPlayer = NetworkClient.localPlayer.GetComponent<NetworkRoomPlayer>();
        GetPlayerPanels();
    }

    private void OnEnable()
    {
        _readyButton.onClick.AddListener(ReadyButtonOnClick);
        _reloadButton.onClick.AddListener(StartSetupPlayerPanels);

        NetworkRoomManager.WhenRoomSlotsChange += StartSetupPlayerPanels;
        NetworkRoomPlayer.WhenReadyStateChange += CmdChangeReadyState;
    }

    private void OnDisable()
    {
        _readyButton.onClick.RemoveListener(ReadyButtonOnClick);
        _reloadButton.onClick.RemoveListener(StartSetupPlayerPanels);

        NetworkRoomManager.WhenRoomSlotsChange -= StartSetupPlayerPanels; 
        NetworkRoomPlayer.WhenReadyStateChange -= CmdChangeReadyState;
    }

    private void GetPlayerPanels()
    {
        var count = UIPLayerHandler.childCount;
        _playerPanels = new PlayerUIPanel[count];

        for(int i = 0; i < count; i++)
        {
            if(UIPLayerHandler.GetChild(i).TryGetComponent<PlayerUIPanel>(out PlayerUIPanel playerUI))
            {
                _playerPanels[i] = playerUI;
                _playerPanels[i].gameObject.SetActive(false);
            }
        }
    }

    private void StartSetupPlayerPanels()
    {
        StartCoroutine(SetupPlayerPanels());
    }

    private IEnumerator SetupPlayerPanels()
    {
        var player = _roomManager.roomSlots;
        var count = _roomManager.roomSlots.Count;

        yield return new WaitForSeconds(0.1f);

        for(int i = 0; i < _playerPanels.Length; i++)
        {
            if(i < count)
            {
                if(player[i].TryGetComponent<NetworkingPlayer>(out NetworkingPlayer networkingPlayer))
                {
                    _playerPanels[i].SetPlayer(networkingPlayer, i);
                }
            }
            else
            {
                _playerPanels[i].gameObject.SetActive(false);
            }
        }
    }

    private void CmdChangeReadyState()
    {
        foreach (PlayerUIPanel playerUI in _playerPanels) 
        {
            if(playerUI.gameObject.activeSelf)
                playerUI.OnChangeReadyStatus();
        }
    }

    private void ReadyButtonOnClick()
    {
        _readyStatus = _localNetworkRoomPlayer.readyToBegin;
        _localNetworkRoomPlayer.CmdChangeReadyState(!_readyStatus);
        
        _roomManager.ChangeReadyStatus();
    }
}
