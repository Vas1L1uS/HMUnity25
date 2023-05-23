using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    public GameObject Bullet;
    public GameObject BulletSpawner;
    public float ShootDelay;

    private float _shootTime = float.MinValue;

    public void Execute()
    {
        if (Time.time < _shootTime + ShootDelay)
        {
            return;
        }
        _shootTime = Time.time;

        if (Bullet != null)
        {
            var newBullet = Instantiate(Bullet, BulletSpawner.transform.position, Quaternion.identity);
            newBullet.GetComponent<BulletComponent>().Direction = new Vector3(newBullet.transform.position.x - this.transform.position.x, 0, newBullet.transform.position.z - this.transform.position.z);
        }
        else
        {
            Debug.LogError("[ShootAbility] No bullet prefab link");
        }
    }
}