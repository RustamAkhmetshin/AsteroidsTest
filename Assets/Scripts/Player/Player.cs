using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Player : CharacterUnit, IShoot
{
    public Vector3 moveDirection;
    public float BulletSpeed;
    public float DamageStrength;
    
    private float ShootLatency;
    
    private float _shootTime = 0;
    
    //temporary declaration
    private float minX = -4f;
    private float maxX = 4f;
    private float maxZ = 5f;
    private float minZ = -5f;
    
    public Player(float maxHealth, float moveSpeed, float shootLatency, float bulletSpeed, float damageStrength, Transform characterTransform) 
        : base(maxHealth, moveSpeed, characterTransform)
    {
        this.moveDirection = Vector3.zero;
        this.BulletSpeed = bulletSpeed;
        this.ShootLatency = shootLatency;
        this.DamageStrength = damageStrength;
    }
    
    public event Action OnDied = () => { };
    public event Action OnReadyToShoot = () => { };

    public override void Move(Vector3 direction)
    {
        CurrentState = State.IsMoving;
        moveDirection = direction;
    }

    public override void Update(float deltaTime)
    {
        if (CurrentState == State.IsMoving)
        {
            _shootTime += deltaTime;

            if (_shootTime >= ShootLatency)
            {
                Shoot();
                _shootTime = 0;
            }
        }
    }

    public override void FixedUpdate(float fixedDeltaTime)
    {
        if (CurrentState == State.IsMoving)
        {
            CharacterTransform.Translate(moveDirection * MoveSpeed);
            CharacterTransform.position = new Vector3(Mathf.Clamp(CharacterTransform.position.x, minX, maxX),
                CharacterTransform.position.y, Mathf.Clamp(CharacterTransform.position.z, minZ, maxZ));
        }
    }

    public override void Hit(Collider collider)
    {
        AddDamage(1);
    }

    public int GetCurrentHealth()
    {
        return (int) CurrentHealth;
    }
    
    public void StopMoving()
    {
        CurrentState = State.Idle;
    }

    public Vector3 GetPosition()
    {
        return CharacterTransform.position;
    }
    
    public void SetPosition(Vector3 position)
    {
        CharacterTransform.position = position;
    }
    
    public Quaternion GetRotation()
    {
        return CharacterTransform.rotation;
    }

    public override void Die()
    {
        CurrentState = State.IsDied;
        OnDied();
    }

    public void Shoot()
    {
        OnReadyToShoot();
    }

    private Transform FindMinimalDistanceEnemy(List<Transform> enemies)
    {
        if (enemies.Count == 0) 
            return null;
        
        float minDistance = Vector3.Distance(CharacterTransform.position, enemies[0].position);
        Transform minimalByDistance = enemies[0];
        for (int i = 1; i < enemies.Count; i++)
        {
            float tmpDistance = Vector3.Distance(CharacterTransform.position, enemies[i].position);
            if (tmpDistance < minDistance)
            {
                minDistance = tmpDistance;
                minimalByDistance = enemies[i];
            }
        }

        return minimalByDistance;
    }
}
