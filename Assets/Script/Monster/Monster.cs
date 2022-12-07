using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : MonoBehaviour
{
    protected float curHealth;
    protected float Power;
    protected bool IsDead;
    protected string Monster_name;

    //List<GameObject> Drop_Items = new List<GameObject>();

    void Awake()
    {
    }

    private void Start()
    {

    }
    public virtual void OnSet(float maxHealth, float Power, string Name)
    {
        Monster_name = Name;
        curHealth = maxHealth;
        this.Power = Power;
        IsDead = false;

        //Debug.Log("OnSet");
    }
    public virtual void OnDamage(float damage)
    {
        curHealth -= damage;
       // Debug.Log(curHealth);
        if (curHealth <= Mathf.Epsilon)
        {
            IsDead = true;
      }

    }

    public void OnDestory()
    {

        
        Instantiate(EffectManager.I.Drop_Item_EXP, this.transform.position, Quaternion.identity);
        Instantiate(EffectManager.I.Drop_Item_Heal, this.transform.position, Quaternion.identity);
        //Instantiate(Drop_Items[Random.Range(0,2)], this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        

    }
    public void OnDestoryCollider()
    {
        Collider MonsterCol = this.GetComponent<Collider>();
        Destroy(MonsterCol);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerWeapon" && PlayerManager.I.IsAttack)
        {
            OnDamage(PlayerManager.I.NormalPower);
            if (!IsDead)
            {
                switch (Monster_name)
                {
                    case "slime":
                        {
                            GameObject go = Instantiate(EffectManager.I.AddDamageText_Slime, this.transform.position, Quaternion.identity, transform);
                            go.GetComponent<TextMesh>().text = PlayerManager.I.NormalPower.ToString();
                        }
                        break;
                    case "dragon":
                        {
                            // Debug.Log("Dragon Normal Attack Hit!!");

                            GameObject go = Instantiate(EffectManager.I.AddDamageText_Dragon, this.transform.position, Quaternion.identity, transform);
                            go.GetComponent<TextMesh>().text = PlayerManager.I.NormalPower.ToString();
                        }
                        break;
                    case "golem":
                        {
                            //Debug.Log("golem Normal Attack Hit!!");
                            GameObject go = Instantiate(EffectManager.I.AddDamageText_Golem, this.transform.position, Quaternion.identity, transform);
                            go.GetComponent<TextMesh>().text = PlayerManager.I.NormalPower.ToString();
                        }
                        break;
                    case "goldbox":
                        {
                            Debug.Log(1);

                            GameObject go = Instantiate(EffectManager.I.AddDamageText_Slime, this.transform.position, Quaternion.identity, transform);
                            go.GetComponent<TextMesh>().text = PlayerManager.I.NormalPower.ToString();
                            GetComponent<Animator>().SetTrigger("IsHit");

                        }
                        break;
                }
            }
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HeavyAttack")
        {
            //Debug.Log("HeavyAttack In ");
            OnDamage(PlayerManager.I.HeavyAttackPower);

            if (!IsDead)
            {
                switch (Monster_name)
                {
                    case "slime":
                        {
                            GameObject go = Instantiate(EffectManager.I.AddDamageText_Slime, this.transform.position, Quaternion.identity, transform);
                            go.GetComponent<TextMesh>().text = PlayerManager.I.HeavyAttackPower.ToString();
                        }
                        break;
                    case "dragon":
                        {
                            // Debug.Log("Dragon Heavy Attack Hit!!");
                            GameObject go = Instantiate(EffectManager.I.AddDamageText_Dragon, this.transform.position, Quaternion.identity, transform);
                            go.GetComponent<TextMesh>().text = PlayerManager.I.HeavyAttackPower.ToString();
                        }
                        break;
                    case "golem":
                        {
                            //  Debug.Log("golem Heavy Attack Hit!!");
                            GameObject go = Instantiate(EffectManager.I.AddDamageText_Golem, this.transform.position, Quaternion.identity, transform);
                            go.GetComponent<TextMesh>().text = PlayerManager.I.HeavyAttackPower.ToString();
                        }
                        break;
                    case "goldbox":
                        {
                            GameObject go = Instantiate(EffectManager.I.AddDamageText_Slime, this.transform.position, Quaternion.identity, transform);
                            go.GetComponent<TextMesh>().text = PlayerManager.I.HeavyAttackPower.ToString();
                            GetComponent<Animator>().SetTrigger("IsHit");
                        }
                        break;
                }
            }

        }

    }

}
