using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class JoystickController : MonoBehaviour, IPointerDownHandler,IPointerUpHandler,IDragHandler
{

    RectTransform Background;
    public GameObject joystick;
    float MyRect;
    public JoystickValue value;

    private float background_radius;
    private Vector3 center;
    private Vector3 Axis;
    private Vector2 touch = Vector2.zero;
    void Start()
    {
        Background = GetComponent<RectTransform>();

        MyRect = Background.sizeDelta.x * 0.25f;
        center = this.transform.position;
    }


    public void OnDrag(PointerEventData eventData)
    {
        Vector3 fingerPos = Input.mousePosition;

        if (Input.touchCount > 1)
        {
            //Debug.Log(Input.GetTouch(1));
            return;
        }
        Axis = (fingerPos - center).normalized;

        float fDistance = Vector3.Distance(fingerPos, center);
        if (fDistance > MyRect) joystick.transform.position = center + Axis * MyRect;
        else joystick.transform.position = center + Axis * fDistance;

         value.joyTouch.x = touch.x = Axis.x;
         value.joyTouch.y = touch.y = Axis.y;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //if(Input.touchCount >1)
        //{
        //    Debug.Log(Input.touchCount);
            
        //}
        joystick.transform.position = Input.mousePosition;
        Vector3 fingerPos = Input.mousePosition;

        Axis = (fingerPos - center).normalized;

        value.joyTouch.x = touch.x = Axis.x;
        value.joyTouch.y = touch.y = Axis.y;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystick.transform.position = center;
        Axis = Vector3.zero;
        value.joyTouch.x = touch.x = 0f;
        value.joyTouch.y = touch.y = 0f;

    }
    public void ResetPoint()
    {
       // Debug.Log("resetPoint()");
        joystick.transform.position = center;
        Axis = Vector3.zero;
        value.joyTouch.x = touch.x = 0f;
        value.joyTouch.y = touch.y = 0f;


    }

}
