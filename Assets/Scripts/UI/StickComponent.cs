using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StickComponent : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private JoystickUIComponent _joystickUi;
    [SerializeField] private float _moveLimit;
    private float _zAxis;
    private Vector3 _clickOffset;



    private void Start()
    {
        _zAxis = 0;
        _clickOffset = Vector3.zero;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _joystickUi.StickTouched(true);
        _clickOffset = transform.position - (new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zAxis));
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 tempVec = new Vector3(Input.mousePosition.x + _clickOffset.x, Input.mousePosition.y + _clickOffset.y, _zAxis);

        if ((tempVec.x * tempVec.x + tempVec.y * tempVec.y) < _moveLimit * _moveLimit)
        {
            transform.localPosition = tempVec;
        }
        else
        {
            transform.localPosition = tempVec.normalized * _moveLimit;
        }

        Vector3 direction = new Vector3(transform.localPosition.x, _zAxis,transform.localPosition.y);
        _joystickUi.StickMoved(direction.normalized);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _joystickUi.StickTouched(false);
        transform.localPosition = Vector3.zero;
    }
}
