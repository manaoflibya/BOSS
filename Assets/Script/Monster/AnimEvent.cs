using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public Action Attack = null;


    void OnAttack()
    {
        Attack?.Invoke();

    }
}
