using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ExitGameButton : NetworkBehaviour
{
    [SerializeField] private Button _exitGameButton;

    private void OnEnable()
    {
        _exitGameButton.onClick.AddListener(StopGame);
    }

    private void OnDisable()
    {
        _exitGameButton.onClick.RemoveListener(StopGame);
    }

    private void StopGame()
    {
        if(NetworkServer.active && NetworkClient.isConnected)
            NetworkManager.singleton.StopHost();
        else if(NetworkClient.isConnected)
            NetworkManager.singleton.StopClient();
        else if(NetworkServer.active)
            NetworkManager.singleton.StopServer();
    }
}
