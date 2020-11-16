using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private GameObject _uiManager;
    [SerializeField] private GameObject _joystickUIComponent;
    [SerializeField] private GameObject _playerController;
    [SerializeField] private GameObject _bulletPool;
    [SerializeField] private GameObject _levelManager;
    [SerializeField] private GameObject _dataManager;
    [SerializeField] private GameObject _dbManager;
    
    public IUIManager GetUIManager()
    {
        return Instantiate(_uiManager, transform).GetComponent<IUIManager>();
    }

    public IPlayerController GetPlayerController()
    {
        return Instantiate(_playerController, transform).GetComponent<IPlayerController>();
    }
    
    public IPool GetBulletPool()
    {
        return Instantiate(_bulletPool, transform).GetComponent<IPool>();
    }
    
    public ILevelManager GetLevelManager()
    {
        return Instantiate(_levelManager, transform).GetComponent<ILevelManager>();
    }
    
    public IDataManager GetDataManager()
    {
        return Instantiate(_dataManager, transform).GetComponent<IDataManager>();
    }
    
    public IDBManager GetDBManager()
    {
        return Instantiate(_dbManager, transform).GetComponent<IDBManager>();
    }
}
