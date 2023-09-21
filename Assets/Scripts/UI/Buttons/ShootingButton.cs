using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ShootingButton : NetworkBehaviour
{
    [SerializeField] private Button _shootingButton;

    private PlayerShooting _playerShooting;

    private void Start()
    {
        _playerShooting = NetworkClient.localPlayer.gameObject.GetComponent<PlayerShooting>();
    }

    private void OnEnable()
    {
        _shootingButton.onClick.AddListener(OnShootButtonClick);
    }

    private void OnDisable()
    {
        _shootingButton.onClick.RemoveListener(OnShootButtonClick);
    }

    private void OnShootButtonClick()
    {
        if(_playerShooting == null) return;
        _playerShooting.Shoot();
    }
}
