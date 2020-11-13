using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class UIManager : MonoBehaviour, IUIManager
{
    private List<IWindow> _windowComponents = new List<IWindow>();
    [SerializeField] 
    private List<GameObject> _windowObjects = new List<GameObject>();
    private Canvas _mainCanvas;
    private Transform _gameRoot;

    public event EventHandler OnShow = (s, e) => { };
    public event EventHandler OnHide = (s, e) => { };
    
    public void Awake()
    {
        foreach (var _windowsObject in _windowObjects)
        {
            _windowComponents.Add(_windowsObject.GetComponent<IWindow>());
        }

        _mainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public T InitializeWindow<T>(GameObject original, Transform transform) where T : Object, IWindow
    {
        var _window = Instantiate(original, transform);
        _window.name = original.name;
        var _windowComponent = _window.GetComponent<IWindow>();

        _windowComponents.Add(_windowComponent);
        _windowObjects.Add(_window);
        
        return (T)_windowComponent;
    }
    
    public Canvas GetMainCanvas()
    {
        return _mainCanvas;
    }

    public bool CloseWindow(string name)
    {
        IWindow window = GetWindow(name);
        
        if (window == null)
            return false;
        
        _windowComponents.Remove(window);
        _windowObjects.Remove(window.GetObject());
        window.Close();
        return true;
    }

    public IWindow GetWindow(string name)
    {
        return _windowComponents.Find(x => x.GetName() == name);
    }

    public bool HideWindow(string name)
    {
        IWindow window = GetWindow(name);
        
        if (window == null)
            return false;

        if (window.IsVisible == true)
        {
            window.Hide();
            return true;
        }
        
        return false;
    }

    public bool ShowWindow(string name)
    {
        IWindow window = GetWindow(name);
        
        if (window == null)
            return false;
        
        if (window.IsVisible == true)
            return true;
        
        window.Show();
        return true;
    }
}
