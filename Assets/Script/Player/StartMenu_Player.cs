using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu_Player : MonoBehaviour
{
    Animator myAnim;
    Transform MoveEffectPos;

    private void Awake()
    {
        MoveEffectPos = GameObject.Find("MoveEffectPos").transform;

        myAnim = GetComponent<Animator>();

        myAnim.SetFloat("y", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddEffect_MoveEffect()
    {
        Instantiate(EffectManager.I.MoveEffect_StartMenu, MoveEffectPos.position, Quaternion.identity);

    }

}
