using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Breath : MonoBehaviour
{
    Transform skillpos;

    void Awake()
    {
        Destroy(this.gameObject, 1.2f);
        //skillpos = GameObject.Find("Dragon_SkillPos").transform;
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player")
        {

            //PlayerManager.I.PlayerOnDamage();
            StopAllCoroutines();
            StartCoroutine(BreathAttack());
        }
        else if (other.tag == "Wall")
        {

        }
        // Debug.Log(other.ToString());
    }
    IEnumerator BreathAttack()
    {

        PlayerManager.I.PlayerOnDamage(1f);
        yield return new WaitForSeconds(1f);
    }
}
