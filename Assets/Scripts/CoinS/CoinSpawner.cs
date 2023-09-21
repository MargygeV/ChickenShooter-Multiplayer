using UnityEngine;
using Mirror;

public class CoinSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject _prefab;

    private void Start()
    {
        if(isServer)
            SpawnObjects();
    }

    private void SpawnObjects()
    {
        for(int i = 0; i < 5; i++)
            {
                Vector2 position = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
                GameObject newObject = Instantiate(_prefab, position, Quaternion.identity);
                NetworkServer.Spawn(newObject);
            }
    }
}