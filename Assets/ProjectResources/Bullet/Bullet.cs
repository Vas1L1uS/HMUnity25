using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    [HideInInspector] public Vector3 Direction;

    private void Start()
    {
        StartCoroutine(TimerToDestroy(5));
    }

    private void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, Direction * 100000, Speed * Time.deltaTime);
    }

    private IEnumerator TimerToDestroy(float sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(this.gameObject);
    }
}

