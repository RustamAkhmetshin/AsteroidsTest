using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterUnit, IShoot
{
    public float BulletSpeed;
    public float DamageStrength;
    
    private float ShootLatency;
    private float _shootingTime;
    private float _shootTime = 0;


    public Enemy(float maxHealth, float moveSpeed, float shootLatency, float bulletSpeed, float damageStrength, Transform characterTransform)
        : base(maxHealth, moveSpeed, characterTransform)
    {
        this.BulletSpeed = bulletSpeed;
        this.ShootLatency = shootLatency;
        this.DamageStrength = damageStrength;
    }
    
    public event Action OnDied = () => { };
    public event Action<Transform> OnReadyToShoot = (target) => { };
    
    public override void Move(Vector3 direction)
    {
        CurrentState = State.IsMoving;
    }

    public override void Update(float deltaTime)
    {
        if (CurrentState == State.IsDied)
        {
            return;
        }
        
        if (CurrentState == State.IsMoving)
        {
            _shootingTime += deltaTime;
            _shootTime += deltaTime;

            if (_shootTime >= ShootLatency)
            {
                Shoot();
                _shootTime = 0;
            }

            if (CharacterTransform.position.z > _upBorder || CharacterTransform.position.z < _downBorder)
            {
                Die();
            }
        }
    }

    public override void FixedUpdate(float fixedDeltaTime)
    {
        if (CurrentState == State.IsDied)
        {
            return;
        }
        
        if (CurrentState == State.IsMoving)
        {
            float step =  MoveSpeed * fixedDeltaTime;
            CharacterTransform.position = Vector3.MoveTowards(CharacterTransform.position, CharacterTransform.position - Vector3.forward, step);
        }
    }

    public void StopGame()
    {
        CurrentState = State.IsDied;
    }
    

    public override void Hit(Collider collider)
    {
        AddDamage(1);
    }
    
    public override void Die()
    {
        CurrentState = State.IsDied;
        OnDied();
    }
    
    public Vector3 GetPosition()
    {
        return CharacterTransform.position;
    }
    
    public void Shoot()
    {
        OnReadyToShoot(CharacterTransform);
    }
}
