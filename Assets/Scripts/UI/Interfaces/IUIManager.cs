using System;
using UnityEngine;

public interface IUIManager
{
    Canvas GetMainCanvas();
    bool CloseWindow(string name);
    IWindow GetWindow(string name);
    bool HideWindow(string name);
    T InitializeWindow<T>(UnityEngine.GameObject original, UnityEngine.Transform transform) where T : UnityEngine.Object, IWindow;
    bool ShowWindow(string name);
}
