using UnityEngine;

public class FlameEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Material _flameMaterial;

    private Material _startMaterial;
    private bool _isActive;

    private void Awake()
    {
        _startMaterial = _skinnedMeshRenderer.material;
    }

    public void OnClickFlameEffect()
    {
        if (_isActive == false)
        {
            _particleSystem.Play();
            _skinnedMeshRenderer.material = _flameMaterial;
            _isActive = true;
        }
        else
        {
            _particleSystem.Stop();
            _skinnedMeshRenderer.material = _startMaterial;
            _isActive = false;
        }
    }
}
