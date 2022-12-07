using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "joystick")]
public class JoystickValue : ScriptableObject
{
    public Vector2 joyTouch;
    public bool ComboAttackTouch;
    public bool RollingTouch;
    public bool HeavyAttackTouch;
    public bool SkillAttackTouch;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
