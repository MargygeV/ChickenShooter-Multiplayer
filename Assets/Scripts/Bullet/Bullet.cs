using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : NetworkingInstanceLifeCycle
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _damage;
    [SerializeField] private ParticleSystem _brokenEggEffect;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _rigidbody.velocity = transform.up * _movementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
            player.ApplyDamage(_damage);

        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        _collider.enabled = false;
        _spriteRenderer.enabled = false;
        _rigidbody.simulated = false;
        _brokenEggEffect.Play();

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}
