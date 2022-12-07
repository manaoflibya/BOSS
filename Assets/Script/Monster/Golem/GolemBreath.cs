using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBreath : MonoBehaviour
{
    Transform skillpos;

    void Awake()
    {
        Destroy(this.gameObject, 3f);
        skillpos = GameObject.Find("Attack2Breath_Pos").transform;
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position = skillpos.position;
        this.transform.rotation = skillpos.rotation;
        if(MonsterManager.I.Golem_IsJumping)
        {
            Destroy(this.gameObject);
        }
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
            Debug.Log("Hit Wall!");

        }
    }
    IEnumerator BreathAttack()
    {

        PlayerManager.I.PlayerOnDamage(1f);
        yield return new WaitForSeconds(1f);
    }
}
