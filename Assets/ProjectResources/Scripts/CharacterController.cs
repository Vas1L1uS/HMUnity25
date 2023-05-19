using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private GameObject _target; // 10 14    25 21
    private Coroutine _checkCoroutine;
    [SerializeField] private NavMeshPathStatus _status;

    private void Start()
    {
        _target = Instantiate(_target);
        _agent = this.GetComponent<NavMeshAgent>();
        _agent.SetDestination(_target.transform.position);

        ChangeTargetPosition();
        _checkCoroutine = StartCoroutine(TimerToCheck());

        _status = _agent.pathStatus;
    }

    private void ArrivedTarget()
    {
        float sec = Random.Range(3f, 8f);
        StartCoroutine(TimerToChangePosition(sec));
    }

    private void ChangeTargetPosition()
    {
        float x = Random.Range(5f, 25f);
        float z;

        if (x < 10)
        {
            z = Random.Range(0f, 14f);
        }
        else if (x < 20)
        {
            z = Random.Range(0f, 16f);
        }
        else
        {
            z = Random.Range(0f, 21f);
        }

        _target.transform.position = new Vector3(x, 3, z);
        _agent.SetDestination(_target.transform.position);
    }

    private IEnumerator TimerToChangePosition(float sec)
    {
        yield return new WaitForSeconds(sec);
        ChangeTargetPosition();
        _checkCoroutine = StartCoroutine(TimerToCheck());
    }

    private IEnumerator TimerToCheck()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForEndOfFrame();
        }

        if (Vector3.Distance(this.transform.position, _target.transform.position) <= _agent.stoppingDistance || _agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            ArrivedTarget();
        }
        else
        {
            _checkCoroutine = StartCoroutine(TimerToCheck());
        }
        _status = _agent.pathStatus;
    }
}
