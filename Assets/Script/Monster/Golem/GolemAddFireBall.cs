using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAddFireBall : MonoBehaviour
{

    public void OnDestroy()
    {
        Destroy(this.gameObject);
    }
    public void Add_GolemFilreBall()
    {
        Instantiate(EffectManager.I.Attack_Fire_FireBall, this.transform.position, this.transform.rotation);

    }
}
