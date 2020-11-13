using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private AsteroidTypes _type;
    
    private Asteroid _asteroid;
    
    private IPlayerController _player => Root.PlayerController;

    private bool _initialized = false;

    private void Start()
    {
        Init();
    }
    
    public AsteroidTypes GetAsteroidType()
    {
        return _type;
    }
    
    public void Init()
    {
        var data = Root.DataManager.GetEnemiesData().asteroidTypes.FirstOrDefault(a => a.Name == _type.ToString());
        _asteroid = new Asteroid(data.MaxHealth, data.MoveSpeed, transform);
        
        _asteroid.OnDied += DieEventHandler;

        _initialized = true;
        _asteroid.Move(new Vector3());
    }
    
    private void Update()
    {
        if (_initialized)
        {
            _asteroid.Update(Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (_initialized)
        {
            _asteroid.FixedUpdate(Time.fixedDeltaTime);
            transform.position = GetPlayerPosition();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        _asteroid.Hit(collider);
    }
    
    public Vector3 GetPlayerPosition()
    {
        return _asteroid.GetPosition();
    }

    private void DieEventHandler()
    {
        Destroy(gameObject);
    }
    
}
