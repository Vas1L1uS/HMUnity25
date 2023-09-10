using UnityEngine;

public interface IBullet
{
    public float Force { get; set; }
    public Vector3 Direction { get; set; }
}