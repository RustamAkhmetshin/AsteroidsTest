

using System;
using UnityEngine;

public interface IJoystickUIComponent
{
    event Action OnTouchDown;
    event Action OnTouchUp;
    event Action<Vector3> OnMove;
}
