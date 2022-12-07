using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBox : MonoBehaviour
{
    Animator myAnim;
    float myHealth = 100f;


    private void Awake()
    {
        myAnim = GetComponent<Animator>();


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerWeapon" && PlayerManager.I.IsAttack)
        {
            myAnim.SetTrigger("IsHit");
            GameObject go = Instantiate(EffectManager.I.AddDamageText_Slime, this.transform.position, Quaternion.identity, transform);
            go.GetComponent<TextMesh>().text = PlayerManager.I.NormalPower.ToString();
            myHealth -= PlayerManager.I.NormalPower;

            if (myHealth < Mathf.Epsilon)
            {
                myAnim.SetTrigger("IsOpen");
                GetComponent<BoxCollider>().enabled = false;
            }
        }

    }
    public void OpenGoldBox()
    {
        StartCoroutine(AddItemDrops());

    }
    public void OnDestroy()
    {
        Destroy(this.gameObject);
    }

    IEnumerator AddItemDrops()
    {
        int count = 0;
        while(count <= 50)
        {
            Instantiate(EffectManager.I.Drop_Item_Coin, this.transform.position, Quaternion.identity);
            Instantiate(EffectManager.I.Drop_Item_EXP, this.transform.position, Quaternion.identity);

            count++;
            yield return null;
        }
    }
 
}
