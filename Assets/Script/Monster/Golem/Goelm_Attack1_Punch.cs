using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goelm_Attack1_Punch : MonoBehaviour
{
    private void Awake()
    {
        Destroy(this.gameObject, 1.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            PlayerManager.I.PlayerOnDamage(MonsterManager.I.Golem_Power);
        }
    }
}

