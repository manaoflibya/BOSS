using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_FireBall : MonoBehaviour
{
    GameObject player;
    Collider myCol;
    //Vector3 _velocity = Vector3.zero;
    void Awake()
    {
        player = GameObject.Find("Player");

    }
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, Time.deltaTime * Random.Range(5f, 8f));
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Instantiate(EffectManager.I.Attack_Fire_FireballExplosionm, other.transform);

            //Instantiate(EffectManager.I.FireBallExplosion,other.transform.position,other.transform.rotation);
            PlayerManager.I.PlayerOnDamage(MonsterManager.I.Dragon_Power);
            Destroy(this.gameObject);

        }
        else if(other.tag == "Wall")
        {
            //Instantiate(EffectManager.I.FireBallExplosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);


        }
       // Debug.Log(other.ToString());
    }
}
