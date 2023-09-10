using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ReflectBullet : MonoBehaviour, IBullet
{
    public float Force { get => _force; set => _force = value; }
    public Vector3 Direction { get; set; }

    [SerializeField] private float _force = 50;

    private Rigidbody _myRB;

    private void Awake()
    {
        _myRB = this.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _myRB.AddForce(Direction * Force, ForceMode.Impulse);
        StartCoroutine(TimerToDestroy(5));
    }

    private IEnumerator TimerToDestroy(float sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(this.gameObject);
    }
}

