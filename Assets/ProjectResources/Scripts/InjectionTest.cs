using UnityEngine;
using Zenject;

public class InjectionTest : MonoBehaviour
{
    [SerializeField] private PlayerSettingsConfig _playerSettingsConfig;

    //[Inject]
    //public void Init(ITest t)
    //{
    //    _test = t;
    //}

    //private void Start()
    //{
    //    _test.Echo();
    //}
}