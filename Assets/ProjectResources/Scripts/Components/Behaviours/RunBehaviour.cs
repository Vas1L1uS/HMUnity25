using UnityEngine;
using UnityEngine.AI;

public class RunBehaviour : MonoBehaviour, IBehaviour
{
    public CharacterHealthAbility CharacterHealth;
    public NavMeshAgent Agent;

    public float Evaluate()
    {
        return 0.5f;
    }

    public void Behave()
    {
        Agent.destination = CharacterHealth.transform.position;
        Agent.isStopped = false;
    }

    public void Stop()
    {
        Agent.isStopped = true;
    }
}