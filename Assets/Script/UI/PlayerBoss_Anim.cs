using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoss_Anim : MonoBehaviour
{
    Animator myAnim;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();


    }

    public void LoopAnimStart()
    {
        myAnim.SetTrigger("StartLoop");
    }
}
