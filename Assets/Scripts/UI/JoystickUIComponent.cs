using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickUIComponent : Window, IJoystickUIComponent
{
    public event Action OnTouchDown = () => { };
    public event Action OnTouchUp = () => { };
    public event Action<Vector3> OnMove = (v) => { };

    public void Move()
    {
        OnMove(new Vector3());
    }

    public void StickTouched(bool touch)
    {
        if (touch)
        {
            OnTouchDown();
        }
        else
        {
            OnTouchUp();
        }
    }

    public void StickMoved(Vector3 vector)
    {
        OnMove(vector);
    }
}
