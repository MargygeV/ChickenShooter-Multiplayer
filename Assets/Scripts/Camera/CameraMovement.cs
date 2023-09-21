using UnityEngine;
using Mirror;

public class CameraMovement : NetworkBehaviour
{
    [SyncVar] private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = NetworkClient.localPlayer.transform;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.localPosition = new Vector3(_playerTransform.position.x, _playerTransform.position.y, -1f);
    }
}
