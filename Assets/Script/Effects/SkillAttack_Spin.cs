using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack_Spin : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(EffectTime());
    }


    // Update is called once per frame
    void Update()
    {



    }

    IEnumerator EffectTime()
    {
        float xTime = 0.5f;
        while (xTime > 0f)
        {
            xTime -= Time.deltaTime;

            yield return null;
        }
        Destroy(this.gameObject);
    }
}
