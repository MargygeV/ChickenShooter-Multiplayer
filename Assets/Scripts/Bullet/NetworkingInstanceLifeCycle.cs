using System.Collections;
using UnityEngine;
using Mirror;

public class NetworkingInstanceLifeCycle : NetworkBehaviour
{
    [SerializeField] private float _lifeTime = 1f;
    
    private void OnEnable()
    {
        StartCoroutine(LifeCycle());
    }

    private void OnDisable()
    {
        StopCoroutine(LifeCycle());
    }

    private IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }
}
