using UnityEngine;

[CreateAssetMenu]
public class PlayerAttributes : ScriptableObject
{
    public string PlayerName => _playerName;

    [SerializeField] private string _playerName;

    public void ResetName(string newName)
    {
        _playerName = newName;
    }
}
