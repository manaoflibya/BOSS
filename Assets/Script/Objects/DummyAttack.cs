using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAttack : MonoBehaviour
{
    Animator myAnim;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();


    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("IsHit");
        if (other.tag == "PlayerWeapon" && PlayerManager.I.IsAttack)
        {
            myAnim.SetTrigger("IsHit");
            GameObject go = Instantiate(EffectManager.I.AddDamageText_Slime, this.transform.position, Quaternion.identity, transform);
            go.GetComponent<TextMesh>().text = PlayerManager.I.NormalPower.ToString();

        }
        
    }

}
