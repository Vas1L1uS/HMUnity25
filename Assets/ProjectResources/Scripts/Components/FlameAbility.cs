using System;
using UnityEngine;

public class FlameAbility : MonoBehaviour, IAbility
{
    public event EventHandler Destroyed;

    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Material _flameMaterial;

    private Material _startMaterial;

    private bool _flamed;

    private void Awake()
    {
        _startMaterial = _skinnedMeshRenderer.material;
    }

    public void Execute()
    {
        _flamed = !_flamed;

        if (_flamed)
        {
            _particleSystem.Play();
            _skinnedMeshRenderer.material = _flameMaterial;
        }
        else
        {
            _particleSystem.Stop();
            _skinnedMeshRenderer.material = _startMaterial;
        }
    }
}