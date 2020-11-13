using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour, IWindow
{
    public bool IsVisible { get { return this.gameObject.activeSelf; } set { this.gameObject.SetActive(value); } }

    public virtual void Hide()
    {
        IsVisible = false;
    }

    public virtual void Show()
    {
        IsVisible = true;
    }

    public string GetName()
    {
        return this.gameObject.name;
    }

    public virtual void Close()
    {
        Destroy(this.gameObject);
    }

    public GameObject GetObject()
    {
        return this.gameObject;
    }
}
