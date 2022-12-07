using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAddMonster_Portal : MonoBehaviour
{

    List<GameObject> monsters = new List<GameObject>();

    private void Awake()
    {
        monsters.Add(EffectManager.I.myDragon);
        //monsters.Add(EffectManager.I.MySlime);
    }

    private void Start()
    {
  

    }
    public void OnAddMonster()
    {
        Debug.Log("AddMonster!");
        Instantiate(monsters[Random.Range(0,1)], this.transform.position, Quaternion.identity);

    }
    public void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}
