using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttack_1 : MonoBehaviour
{
    GameObject sword;

    private void Awake()
    {
        sword = GameObject.Find("HeavyAttackPos");
        this.transform.rotation = sword.transform.rotation;
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
