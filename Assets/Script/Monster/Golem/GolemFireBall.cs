using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemFireBall : MonoBehaviour
{

    GameObject target;

    void Awake()
    {
        target = GameObject.Find("Player");
        //Destroy(this.gameObject, 2f);
    }
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, Time.deltaTime * Random.Range(5f, 8f));
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {

            Instantiate(EffectManager.I.Attack_Fire_FireballExplosionm, other.transform);
            PlayerManager.I.PlayerOnDamage(MonsterManager.I.Dragon_Power);
            Destroy(this.gameObject);

        }
        else if (other.tag == "Wall")
        {
            Instantiate(EffectManager.I.Attack_Fire_FireballExplosionm, other.transform);
            Destroy(this.gameObject);


        }
    }
}
