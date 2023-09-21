using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : NetworkBehaviour
{
    public static event UnityAction<float> HealthChanged;
    public static event UnityAction<int> MoneyChanged;

    [SerializeField] private float _maxHealth;

    [SyncVar] private float _currentHealth;
    [SyncVar] private int _money;

    private Animator _animator;
    private Collider2D _collider;
    private Rigidbody2D _rigidbody;

    private List<Transform> _startPositions;

    public override void OnStartLocalPlayer()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if(!isLocalPlayer) return;

        _startPositions = NetworkRoomManager.startPositions;
        MoneyChanged?.Invoke(_money);
        HealthChange(_maxHealth);
    }

    public void AddCoin()
    {
        if(!isLocalPlayer) return;

        _money++;
        MoneyChanged?.Invoke(_money);
    }

    public void ApplyDamage(float damage)
    {   
        if(!isLocalPlayer) return;

        if(damage < _currentHealth)
            _currentHealth -= damage;
        else
        {
            _currentHealth = 0;
            Die();
        }
        HealthChange(_currentHealth);
    }

    private void HealthChange(float newHealth)
    {
        _currentHealth = newHealth;
        var healthPercent = _currentHealth / _maxHealth;
        HealthChanged?.Invoke(healthPercent);
    }

    private void Die()
    {
        _collider.enabled = false;
        _rigidbody.simulated = false;

        PlayerRespawn();        
    }

    private void PlayerRespawn()
    {
        var positionsCount = _startPositions.Count;
        var randomPositionNumber = Random.Range(0, positionsCount);
        transform.position = _startPositions[randomPositionNumber].position;

        _collider.enabled = true;
        _rigidbody.simulated = true;

        HealthChange(_maxHealth);
    }
}