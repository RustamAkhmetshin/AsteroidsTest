
using System.Collections.Generic;
using UnityEngine;

public interface ILevelManager
{
    bool IsGameStarted();
    void StartGame();
    void LoadLevel(int level);
    List<Level> GetLevelsList();
    GameObject GetRestartWindow();
    void ShowLevelsWindow();
    Level GenerateRandomLevel();
    void GameOver();
    void Win();
    
}
