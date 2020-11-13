using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour, IDataManager
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private EnemiesData _enemiesData;
    [SerializeField] private GlobalVariables _globalVariables;
    
    public PlayerData GetPlayerData()
    {
        return _playerData;
    }

    public EnemiesData GetEnemiesData()
    {
        return _enemiesData;
    }

    public GlobalVariables GetGlobalVariables()
    {
        return _globalVariables;
    }
}
