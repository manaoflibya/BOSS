using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAction_Buttons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public JoystickValue value;
    public void OnPointerDown(PointerEventData eventData)
    {
        value.ComboAttackTouch = true;
       // Debug.Log(value.ComboAttackTouch);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //value.ComboAttackTouch = false;
       // Debug.Log(value.ComboAttackTouch);
    }
}
