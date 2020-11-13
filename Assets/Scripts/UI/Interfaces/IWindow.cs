using System;
using UnityEngine;

public interface IWindow
{
    bool IsVisible { get; set; }
    void Hide();
    void Show();
    void Close();
    string GetName();
    GameObject GetObject();
}
