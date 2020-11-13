using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class WinWindow : Window
{
    
    
    void Start()
    {
        Button restart = GetComponentInChildren<Button>();
        restart.onClick.AsObservable().Subscribe(_ => Root.LevelManager.ShowLevelsWindow());
    }
}
