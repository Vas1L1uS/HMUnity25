using UnityEngine;

public class AttackBehaviour : MonoBehaviour, IBehaviour
{
    public CharacterHealthAbility CharacterHealth;

    private void Start()
    {
        CharacterHealth = FindObjectOfType<CharacterHealthAbility>();
    }

    public float Evaluate()
    {
        if (CharacterHealth == null) return 0;

        return 1 / (this.gameObject.transform.position - CharacterHealth.transform.position).magnitude;
    }

    public void Behave()
    {
        transform.Rotate(Vector3.up * 1000 * Time.deltaTime);
        CharacterHealth.GetDamage(1);
    }

    public void Stop()
    {
        return;
    }
}
