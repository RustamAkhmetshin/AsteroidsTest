using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonUIView : MonoBehaviour
{
    public int levelNumber;
    [SerializeField] private Text _numberText;
    [SerializeField] private Color _openedColor;
    [SerializeField] private Color _closedColor;
    [SerializeField] private Color _passedColor;

    public void Init(int levelNumber, LevelState state)
    {
        _numberText.text = levelNumber.ToString();
        this.levelNumber = levelNumber;
        
        switch (state)
        {
            case LevelState.Closed:
                GetComponent<Image>().color = _closedColor;
                break;
            case LevelState.Opened:
                GetComponent<Image>().color = _openedColor;
                break;
            case LevelState.Passed:
                GetComponent<Image>().color = _passedColor;
                break;
        }
        
        if(state != LevelState.Closed)
            GetComponent<Button>().onClick.AsObservable().Subscribe(_ => Root.LevelManager.LoadLevel(levelNumber));
    }
}
