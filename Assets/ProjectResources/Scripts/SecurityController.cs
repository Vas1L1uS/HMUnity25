using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecurityController : MonoBehaviour
{
    private List<Transform> _visitors;
    private Coroutine _checkCoroutine;

    private NavMeshAgent _agent;
    [SerializeField] private Transform _target;

    private void Start()
    {
        _visitors = new List<Transform>();
        var a = FindObjectsOfType<CharacterController>();
        _agent = this.GetComponent<NavMeshAgent>();

        for (int i = 0; i < a.Length; i++)
        {
            _visitors.Add(a[i].GetComponent<Transform>());
        }

        ArrivedTarget();
        _checkCoroutine = StartCoroutine(TimerToCheck());
    }

    private IEnumerator TimerToCheck()
    {
        yield return new WaitForSeconds(0.5f);

        if (Vector3.Distance(this.transform.position, _target.position) <= _agent.stoppingDistance || _agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            ArrivedTarget();
        }
        _agent.SetDestination(_target.transform.position);
        _checkCoroutine = StartCoroutine(TimerToCheck());
    }

    private void ArrivedTarget()
    {
        _target = _visitors[Random.Range(0, _visitors.Count)];
    }
}
