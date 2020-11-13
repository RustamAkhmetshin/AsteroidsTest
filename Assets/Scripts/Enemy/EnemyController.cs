using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyTypes _type;
    [SerializeField] private Transform _bulletPlace;

    private Enemy _enemy;
    
    private IPlayerController _player => Root.PlayerController;
    private IPool _bulletPool => Root.BulletPool;
    
    private bool _initialized = false;

    private void Start()
    {
        Init();
    }

    public EnemyTypes GetEnemyType()
    {
        return _type;
    }
    
    public void Init()
    {
        var data = Root.DataManager.GetEnemiesData().enemyTypes.FirstOrDefault(e => e.Name == _type.ToString());
        _enemy = new Enemy(data.MaxHealth, data.MoveSpeed, data.ShootLatency,
            data.BulletSpeed, data.DamageStrength, transform);
        
        _enemy.OnDied += DieEventHandler;
        _enemy.OnReadyToShoot += ReadyToSpawnBullet;

        _initialized = true;
        _enemy.Move(new Vector3());
    }
    
    private void Update()
    {
        if (_initialized)
        {
            _enemy.Update(Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (_initialized)
        {
            _enemy.FixedUpdate(Time.fixedDeltaTime);
            transform.position = GetPlayerPosition();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        _enemy.Hit(collider);
    }
    
    public Vector3 GetPlayerPosition()
    {
        return _enemy.GetPosition();
    }

    private void DieEventHandler()
    {
        Destroy(gameObject);
    }

    private void ReadyToSpawnBullet(Transform target)
    {
        _bulletPool.Spawn(_bulletPlace.position).Init(_enemy.BulletSpeed, _enemy.DamageStrength, transform.rotation);
    }
}
