using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : CharacterUnit
{
    
    public Asteroid(float maxHealth, float moveSpeed, Transform characterTransform) : base(maxHealth, moveSpeed, characterTransform)
    {
        
    }

    public event Action OnDied = () => { };
    public event Action<Transform> OnReadyToShoot = (target) => { };
    
    public override void Move(Vector3 direction)
    {
        CurrentState = State.IsMoving;
    }

    public override void Update(float deltaTime)
    {
        if (CharacterTransform.position.z > _upBorder || CharacterTransform.position.z < _downBorder)
        {
            Die();
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
}
