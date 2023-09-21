using UnityEngine;
using Mirror;

public class NetworkingPlayer : NetworkBehaviour
{
    public string Nickname => _nickname;
    public bool IsReady => _isReady;

    [SerializeField] private PlayerAttributes _playerAttributes;

    [SyncVar] private string _playroomName;
    [SyncVar] private string _nickname;
    [SyncVar] private NetworkRoomPlayer _networkRoomPlayer;
    
    private bool _isReady => _networkRoomPlayer.readyToBegin;

    private void Start()
    {
        if(isLocalPlayer)
            InitAttributes();

        _networkRoomPlayer = gameObject.GetComponent<NetworkRoomPlayer>();
    }

    private void InitAttributes()
    {
        _nickname = _playerAttributes.PlayerName;
    }
}
