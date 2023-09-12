using System;
using UnityEngine;
using Zenject;

public class WorkInstalller : MonoInstaller
{
    [SerializeField] private LoadType _loadType;
    [SerializeField] private PlayerSettingsConfig _playerSettingsConfig;

    public override void InstallBindings()
    {
        switch (_loadType)
        {
            case LoadType.ScriptableObject:

                Func<InjectContext, PlayerSettings> method = ctx =>
                {
                    return _playerSettingsConfig.GetFormatPlayerSettings();
                };

                Container.Bind<IPlayerSettings>().To<PlayerSettings>().FromMethod(method).AsTransient().NonLazy();
                break;

            case LoadType.DummyClass:
                Container.Bind<IPlayerSettings>().To<DummyPlayerSettings>().AsSingle().NonLazy();
                break;
        }
    }

    private class DummyPlayerSettings : IPlayerSettings
    {
        public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public float JumpHeight { get => _jumpHeight; set => _jumpHeight = value; }
        public float Damage { get => _damage; set => _damage = value; }

        private float _maxHealth = 100;
        private float _speed = 25;
        private float _jumpHeight = 10;
        private float _damage = 30;
    }

    private enum LoadType
    {
        ScriptableObject,
        DummyClass
    }
}