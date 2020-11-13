using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "PlayerData", menuName = "PlayerData" )]
public class PlayerData : ScriptableObject
{
    public float MaxHealth;
    public float MoveSpeed;
    public float ShootLatency;
    public float BulletSpeed;
    public float DamageStrength;
}
