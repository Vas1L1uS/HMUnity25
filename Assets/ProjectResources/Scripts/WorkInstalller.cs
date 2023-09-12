using UnityEngine;
using Zenject;

public class WorkInstalller : MonoInstaller
{
    [SerializeField] private LoadType _loadType;

    //public override void InstallBindings()
    //{
    //    switch (_loadType)
    //    {
    //        case LoadType.ScriptableObject:

    //    }
    //}

    private enum LoadType
    {
        ScriptableObject,
        DummyClass
    }
}