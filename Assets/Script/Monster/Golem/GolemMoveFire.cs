using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMoveFire : MonoBehaviour
{
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
            //Debug.Log("ㅋ 그건 벽이다");

        }
        // Debug.Log(other.ToString());
    }
    IEnumerator BreathAttack()
    {

        PlayerManager.I.PlayerOnDamage(1f);
        yield return new WaitForSeconds(1.5f);
    }
}
