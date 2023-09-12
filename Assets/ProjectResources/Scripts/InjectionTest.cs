using UnityEngine;
using Zenject;

public class InjectionTest : MonoBehaviour
{
    private IPlayerSettings _playerSettings;

    [Inject]
    public void Init(IPlayerSettings playerSettings)
    {
        _playerSettings = playerSettings;
    }

    private void Start()
    {
        Debug.Log($"MaxHealth:{_playerSettings.MaxHealth}, Speed:{_playerSettings.Speed}," +
            $" JumpHeight:{_playerSettings.JumpHeight}, Damage:{_playerSettings.Damage}");
    }
}