using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
[Serializable]

public class LevelEnemyData
{
    public EnemyData enemyType;
    public int count;
}

[Serializable]
public class LevelAsteroidData
{
    public AsteroidData asteroidType;
    public int count;
}
