using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class PlayerUIPanel : NetworkBehaviour
{
    [SerializeField] private TMP_Text _nicknameField;
    [SerializeField] private Toggle _readyToggle;

    private NetworkingPlayer _networkingPlayer;
    private int _count;
    private string _playerNickname;

    public void SetPlayer(NetworkingPlayer networkingPlayer, int count)
    {
        _networkingPlayer = networkingPlayer;
        _playerNickname = _networkingPlayer.Nickname;
        _count = ++count;

        Init();
        OnChangeReadyStatus();
    }

    public void Init()
    {
        if(_playerNickname != "")
            _nicknameField.text = _networkingPlayer.Nickname;
        else
            _nicknameField.text = "Player " + _count;

        gameObject.SetActive(true);
    }

    public void OnChangeReadyStatus()
    {
        if(_networkingPlayer.IsReady)
            _readyToggle.isOn = true;
        else
            _readyToggle.isOn = false;
    }
}
