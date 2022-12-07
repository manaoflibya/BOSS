using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public enum ITEM_TYPE
    {
        NONE,COIN,HEAL,EXP
    }
    public ITEM_TYPE myItemType = ITEM_TYPE.NONE;
    GameObject Target;
    Rigidbody myRig;
    SphereCollider mycol;

    Vector3 offset = new Vector3(0, 5f, 0);
    float TurnSpeed = 1000f;
    float followSpeed = 50f;

    bool Isfloor = false;
    public AudioClip Coin_S;
    public AudioClip EXP_S;
    public AudioClip Heal_S;

    private void Awake()
    {
    }
    private void Start()
    {
        Vector3 offset = new Vector3(0, 0.5f, 0);
        Vector3 RandomInit = new Vector3(0.25f, 0.25f, 0.25f);

        Target = GameObject.Find("Player");

        myRig = GetComponent<Rigidbody>();
        mycol = GetComponent<SphereCollider>();
        StartCoroutine(throwItem());

        mycol.isTrigger = true;
        this.transform.localPosition += offset;
        this.transform.localPosition += new Vector3(Random.Range(-RandomInit.x, RandomInit.x),
            Random.Range(-RandomInit.y, RandomInit.y),
            Random.Range(-RandomInit.z, RandomInit.z));


        Destroy(this.gameObject, 8f);

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up, Time.deltaTime * TurnSpeed);

        if(Isfloor)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, Target.transform.position, Time.deltaTime * followSpeed);

        }

    }

    //protected void OnSet(ITEM_TYPE t)
    //{
    //    myItemType = t;
    //}

    private void OnDestroy()
    {
        Destroy(this.gameObject);

    }

    IEnumerator throwItem()
    {
        float high = 0f;

        while (this.transform.position.y < 2f)
        {

            high += Time.deltaTime;
            this.transform.position += new Vector3(0, high, 0);

            mycol.isTrigger = false;
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        mycol.isTrigger = true;
        Isfloor = true;
        myRig.useGravity = false;
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            switch (myItemType)
            {
                case ITEM_TYPE.COIN:
                    PlayerManager.I.PlusCoin();
                    Instantiate(EffectManager.I.GetMoneyExplosion, this.transform.position, Quaternion.identity);
                    SoundManager.I.PlayEffectSound(Coin_S);
                    OnDestroy();
                    break;
                case ITEM_TYPE.HEAL:
                    PlayerManager.I.PlusHeal(30f);
                    Instantiate(EffectManager.I.GetHealExplosion, this.transform.position, Quaternion.identity);
                    SoundManager.I.PlayEffectSound(Heal_S);

                    OnDestroy();
                    break;
                case ITEM_TYPE.EXP:
                    PlayerManager.I.PlusEXP(10);
                    Instantiate(EffectManager.I.GetEXP_Explosion, this.transform.position, Quaternion.identity);
                    SoundManager.I.PlayEffectSound(EXP_S);

                    OnDestroy();

                    break;
            }

        }
    }

}
