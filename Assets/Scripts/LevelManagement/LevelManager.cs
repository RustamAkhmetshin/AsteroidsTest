using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour, ILevelManager
{
    [SerializeField] private GameObject _restartWindow;
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _levelsWindow;
    [SerializeField] private GameObject _gameUIWindow;
    [SerializeField] private EnemySpawner _spawner;

    private bool _gameStarted = false;
    private List<Level> _levels;
    private int _currentLevel;

    public bool IsGameStarted()
    {
        return _gameStarted;
    }

    public void StartGame()
    {
        var UIManager = Root.UIManager;
        UIManager.CloseWindow("LevelsWindow");
        Root.BulletPool.Preload(20);
        Root.PlayerController.Init();
        Root.UIManager.InitializeWindow<GameUIWindow>(_gameUIWindow,
            Root.UIManager.GetMainCanvas().transform);
        _gameStarted = true;
    }

    public void ShowLevelsWindow()
    {
        _levels = Root.DBManager.DeserializeAndLoad();

        if (_levels == null)
        {
            _levels = new List<Level>();
            _levels.Add(GenerateRandomLevel());
            Root.DBManager.SerializeAndSave(_levels);
        }

        var UIManager = Root.UIManager;
        UIManager.CloseWindow("RestartWindow");
        UIManager.CloseWindow("WinWindow");
        UIManager.CloseWindow("GameUIWindow");
        UIManager.InitializeWindow<LevelsWindow>(_levelsWindow,
            Root.UIManager.GetMainCanvas().transform);
    }

    public void LoadLevel(int level)
    {
        if (_levels.Count < level)
        {
            _levels.Add(GenerateRandomLevel());
            Root.DBManager.SerializeAndSave(_levels);
        }

        Level l = _levels[level - 1];

        _currentLevel = level - 1;
        
        Instantiate(_spawner.gameObject);
        _spawner.Spawn(l);
        
        StartGame();
    }

    public List<Level> GetLevelsList()
    {
        return _levels;
    }

    public GameObject GetRestartWindow()
    {
        return _restartWindow;
    }

    public Level GenerateRandomLevel()
    {
      
        List<LevelEnemyData> e = new List<LevelEnemyData>();
        List<LevelAsteroidData> a = new List<LevelAsteroidData>();
        int enemyCount = Random.Range(3, 6);
        int asteroidCount = Random.Range(3, 6);

        var enemyTypesList = Root.DataManager.GetEnemiesData().enemyTypes;
        var asteroisTypesList = Root.DataManager.GetEnemiesData().asteroidTypes;
        
        for (int i = 0; i < enemyCount; i++)
        {
            LevelEnemyData e_data = new LevelEnemyData();
            e_data.enemyType = enemyTypesList[Random.Range(0, enemyTypesList.Count)];
            e_data.count = 1;
            e.Add(e_data);
        }
        
        for (int i = 0; i < asteroidCount; i++)
        {
            LevelAsteroidData a_data = new LevelAsteroidData();
            a_data.asteroidType = asteroisTypesList[Random.Range(0, asteroisTypesList.Count)];
            a_data.count = 1;
            a.Add(a_data);
        }
        
        Level l = new Level(e, a, LevelState.Opened);

        return l;
    }

    public void Win()
    {
        Root.UIManager.CloseWindow("JoystickUIComponent");

        _levels[_currentLevel].LevelState = LevelState.Passed;
        if (_levels.Count > _currentLevel + 1)
        {
            _levels[++_currentLevel].LevelState = LevelState.Opened;
        }
        else
        {
            _levels.Add(GenerateRandomLevel());
        }
        
        Root.DBManager.SerializeAndSave(_levels);

        Root.UIManager.InitializeWindow<WinWindow>(_winWindow,
            Root.UIManager.GetMainCanvas().transform);
    }

    public void GameOver()
    {
        _spawner.Unsubscribe();

        Root.UIManager.CloseWindow("JoystickUIComponent");
        
        Root.UIManager.InitializeWindow<RestartWindow>(_restartWindow,
            Root.UIManager.GetMainCanvas().transform);
    }
}
