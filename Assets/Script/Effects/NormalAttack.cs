using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    //PlayerActions Player;
    GameObject sword;

    private void Awake()
    {
        sword = GameObject.Find("HeavyAttackPos");
        this.transform.rotation = sword.transform.rotation;
       // Vector3 newvec = new Vector3(0f, 0f, 90f);
        //this.transform.rotation = Quaternion.LookRotation(newvec);
        StartCoroutine(EffectTime());
    }


    // Update is called once per frame
    void Update()
    {



    }

    IEnumerator EffectTime()
    {
        float xTime = 1f;
        while (xTime > 0f)
        {
            xTime -= Time.deltaTime;

            yield return null;
        }
        Destroy(this.gameObject);
    }
}
