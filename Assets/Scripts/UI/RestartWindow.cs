using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartWindow : Window
{
    
    
    void Start()
    {
        Button restart = GetComponentInChildren<Button>();
        restart.onClick.AsObservable().Subscribe(_ => Root.LevelManager.ShowLevelsWindow());
    }
}
