using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public float border;
    public float topBorder;
    
    [SerializeField] private List<GameObject> _objects;

    private IDisposable _subscription;

    public GameObject GetByName(string name)
    {
        return _objects.FirstOrDefault(e => e.name == name);
    }

    public void Spawn(Level level)
    {
        
        List<string> itemsToSpawn = new List<string>();

        foreach (var v in level.Asteroids)
        {
            for (int i = 0; i < v.count; i++)
            {
                itemsToSpawn.Add(v.asteroidType.Name);
            }
        }
        
        foreach (var v in level.Enemies)
        {
            for (int i = 0; i < v.count; i++)
            {
                itemsToSpawn.Add(v.enemyType.Name);
            }
        }
        
        _subscription = Observable.Timer (System.TimeSpan.FromSeconds (1)) 
            .Repeat () 
            .Subscribe (_ =>
            {
                float x = Random.Range(-border, border);
                
                int randomIndex = Random.Range(0, itemsToSpawn.Count);

                Instantiate(GetByName(itemsToSpawn[randomIndex]), new Vector3(x, 0, topBorder), Quaternion.Euler(new Vector3(180,0,0)));

                itemsToSpawn.RemoveAt(randomIndex);

                if (itemsToSpawn.Count <= 0)
                {
                   Unsubscribe();
                   
                   Observable.Timer (System.TimeSpan.FromSeconds (5f))
                       .Subscribe (__ =>
                       {
                           Root.LevelManager.Win();

                       }).AddTo (this); 
                }

            }).AddTo (this); 
    }

    public void Unsubscribe()
    {
        _subscription.Dispose();
    }
}
