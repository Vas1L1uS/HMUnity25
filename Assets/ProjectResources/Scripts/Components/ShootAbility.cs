using System;
using System.Collections;
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    public event EventHandler Destroyed;

    public GameObject Bullet;
    public GameObject ReflectBullet;
    public GameObject BulletSpawner;
    public float ShootDelay;


    private bool _isReflectedBullet;
    private float _shootTime = float.MinValue;
    private Coroutine _reflectBulletCoroutine;

    public void Execute()
    {
        if (Time.time < _shootTime + ShootDelay)
        {
            return;
        }
        _shootTime = Time.time;

        if (Bullet != null)
        {
            GameObject newBullet;

            if (_isReflectedBullet)
            {
                newBullet = Instantiate(ReflectBullet, BulletSpawner.transform.position, Quaternion.identity);
            }
            else
            {
                newBullet = Instantiate(Bullet, BulletSpawner.transform.position, Quaternion.identity);
            }

            newBullet.GetComponent<IBullet>().Direction = new Vector3(newBullet.transform.position.x - this.transform.position.x, 0, newBullet.transform.position.z - this.transform.position.z);
        }
        else
        {
            Debug.LogError("[ShootAbility] No bullet prefab ref");
        }
    }

    public void TakeReflectBulletItem(float time)
    {
        _reflectBulletCoroutine = StartCoroutine(ReflectBulletTimer(time));
    }

    private IEnumerator ReflectBulletTimer(float time)
    {
        _isReflectedBullet = true;
        yield return new WaitForSeconds(time);
        _isReflectedBullet = false;
    }
}