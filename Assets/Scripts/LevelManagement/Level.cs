

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[Serializable()]
[XmlRoot(ElementName = "Level")]
public class Level
{
    public LevelState LevelState;
    [XmlArrayItem("Enemies")]
    public List<LevelEnemyData> Enemies;
    [XmlArrayItem("Asteroids")]
    public List<LevelAsteroidData> Asteroids;

    public Level(List<LevelEnemyData> enemies, List<LevelAsteroidData> asteroids, LevelState state)
    {
        this.LevelState = state;
        this.Enemies = enemies;
        this.Asteroids = asteroids;
    }
}


