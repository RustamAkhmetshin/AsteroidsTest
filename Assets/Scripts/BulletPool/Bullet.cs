using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    private float _upBorder = 20;
    private float _downBorder = -20f;

    public void Init(float speed, float damage, Quaternion rotation)
    {
        this._speed = speed;
        this._damage = damage;
        transform.rotation = rotation;
    }
    
    private void Update()
    {
        transform.Translate(Vector3.forward * _speed);
        if (transform.position.z > _upBorder || transform.position.z < _downBorder)
        {
            var bulletPool = Root.BulletPool;
            bulletPool.HideToPool(this);
        }
    }

    public float Damage => _damage;

    private void OnTriggerEnter(Collider collider)
    {
        var bulletPool = Root.BulletPool;
        bulletPool.HideToPool(this);
    }
}
