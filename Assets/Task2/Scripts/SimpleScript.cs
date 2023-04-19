using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleScript : MonoBehaviour
{
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void WantToDestroy()
    {
        Destroy(this.gameObject);
    }

    public void AddForceToObject()
    {
        _rb.AddForce(Vector3.right * 10, ForceMode.Impulse);
    }

    public void SetRandomObjectScale()
    {
        gameObject.transform.localScale = new Vector3(Random.Range(0.5f, 2f), Random.Range(0.5f, 2f), Random.Range(0.5f, 2f));
    }
}
