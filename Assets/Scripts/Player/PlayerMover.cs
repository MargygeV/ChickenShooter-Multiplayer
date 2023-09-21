using UnityEngine;
using Mirror;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : NetworkBehaviour
{
    public Vector2 MoveDirection => _moveDirection;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private Transform _spriteHandler;

    [SyncVar] private Vector2 _moveDirection;

    private Rigidbody2D _rigidbody; 
    private Animator _animator;
    private Joystick _joystick;

    private bool _isMove => _moveDirection != Vector2.zero;

    public override void OnStartLocalPlayer()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _joystick = Joystick.CurrentJoystick;
    }

    private void Update()
    {
        if(!isLocalPlayer) return;

        //_moveDirection = new Vector2(_joystick.Horizontal, _joystick.Vertical);
        _moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _animator.SetBool("IsMove", _isMove);
        Flip();
    }

    private void FixedUpdate()
    {
        Move();   
    }

    private void Move()
    {
        _rigidbody.MovePosition(_rigidbody.position + _moveDirection * _movementSpeed / 100);
    }

    private void Flip()
    {
        if(_moveDirection.x > 0 && _spriteHandler.localScale.x == -1)
        {
            _spriteHandler.localScale = new Vector3(1, 1, 1);
        }
        else if(_moveDirection.x < 0 && _spriteHandler.localScale.x == 1)
        {
            _spriteHandler.localScale = new Vector3(-1, 1, 1);
        }
    }  
}