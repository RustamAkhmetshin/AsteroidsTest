using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsWindow : Window
{
    [SerializeField] private List<LevelButtonUIView> _levelButtons;

    private void OnEnable()
    {
        var levels = Root.LevelManager.GetLevelsList();
        
        for (int i = 0; i < _levelButtons.Count; i++)
        {
             _levelButtons[i].Init(i + 1, levels.Count > i ? levels[i].LevelState : LevelState.Closed);
        }
    }
}
