
using System;
using System.Data.Common;
using UnityEngine;

public abstract class CharacterUnit : IDamagable
{
    protected float MaxHealth;
    protected float MoveSpeed;
    protected float CurrentHealth;
    
    protected Transform CharacterTransform;
    
    protected float _upBorder = 20;
    protected float _downBorder = -20f;

    protected enum State
    {
        Idle,
        IsDied,
        IsMoving
    }

    protected State CurrentState;

    public CharacterUnit(float maxHealth, float moveSpeed, Transform characterTransform)
    {
        this.MaxHealth = maxHealth;
        this.MoveSpeed = moveSpeed;
        this.CharacterTransform = characterTransform;
        this.CurrentHealth = maxHealth;
        CurrentState = State.Idle;
    }

    public virtual void AddDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public abstract void Move(Vector3 direction);
    public abstract void Update(float deltaTime);
    public abstract void FixedUpdate(float fixedDeltaTime);
    public abstract void Hit(Collider collider);

    public virtual void Die()
    {
        CurrentState = State.IsDied;
    }
}
