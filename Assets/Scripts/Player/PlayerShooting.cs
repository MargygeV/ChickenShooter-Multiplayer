using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerShooting : NetworkBehaviour
{
    [SerializeField] private Transform _targetHandlerTransform;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _offsetRotation;

    private PlayerMover _playerMover;
    private Vector2 _lookingDirection => _playerMover.MoveDirection;
    private Vector3 _rotationBody;

    public override void OnStartLocalPlayer()
    {
        if(TryGetComponent<PlayerMover>(out PlayerMover playerMover))
            _playerMover = playerMover;
    }

    private void Update()
    {
        if(!isLocalPlayer) return;

        if(_lookingDirection != Vector2.zero)
        {
            float rotZ = Mathf.Atan2(_lookingDirection.y, _lookingDirection.x) * Mathf.Rad2Deg;
            _targetHandlerTransform.rotation = Quaternion.Euler(0f, 0f, rotZ + _offsetRotation);
        }
    }

    [Command]
    public void Shoot()
    {
        var newObject = Instantiate(_bulletPrefab, _targetTransform.position, _targetTransform.rotation);
        NetworkServer.Spawn(newObject);
    }
}