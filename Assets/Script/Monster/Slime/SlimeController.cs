using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeController : Monster, BattleSystem
{
    Camera myCamera;

    Animator myAnim;
    CapsuleCollider myCol;
    Transform Target;
    Material mat;
    NavMeshAgent nav;

    RangeSystem myRangeSystem = null;
    AnimEvent myAnimEvent = null;
    Vector3 StartPos;

    float AttackCooltime = 2f;
    float StartAttackCooltime = 0f;
    float normaltime;

    public AudioClip Hit_S;
    public AudioClip Step_S;
    public AudioClip Die_S;
    public AudioClip Attack_S;

    enum STATE
    {
        NORMAL,SEARCH, FIND, CLOSER, BATTLE, DIE   
    }

    [SerializeField]
    STATE myState = STATE.NORMAL;

    // effect
    Transform MoveEffectPos;

    //GameObject AddDamageText;
    private void Awake()
    {
        myRangeSystem = GetComponentInChildren<RangeSystem>();
        myAnimEvent = GetComponent<AnimEvent>();
        Target = GameObject.Find("Player").transform;
        myAnim = GetComponent<Animator>();
        myCol = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        //AddDamageText = Resources.Load("FloatingText") as GameObject;
        
        
        // 기본 스텟
        StartPos = this.transform.position;
        MoveEffectPos = GameObject.Find("Slime_MoveEffectPos").transform;

        StartAttackCooltime = AttackCooltime;
        //
        myRangeSystem.battle += this.OnBattle;
        myAnimEvent.Attack += this.OnAttackTarget;
        // Debug.Log(SlimeCount);

    }
    private void Start()
    {
        OnSet(MonsterManager.I.Slime_Health, MonsterManager.I.Slime_Power, MonsterManager.I.Slime_name);

        //Debug.Log(curHealth);
    }
    // Update is called once per frame
    void Update()
    {
        if (IsDead)
            ChangeState(STATE.DIE);
        else if(!IsDead)
            StateProcess();

        if (MonsterManager.I.BossDead)
            ChangeState(STATE.DIE);
    }
    void ChangeState(STATE s)
    {
        if (s == myState) 
            return;

        myState = s;

        switch (myState)
        {
            case STATE.NORMAL:
                myAnim.SetFloat("Speed", 0f);
                break;
            case STATE.SEARCH:

                Vector3 dir = Vector3.zero;
                dir.x = Random.Range(-1.0f, 1.0f);
                dir.z = Random.Range(-1.0f, 1.0f);
                dir.Normalize(); // 정규화된 벡터로 생성

                nav.SetDestination(StartPos + dir * Random.Range(2.0f, 2.0f));
                //Debug.Log(nav.remainingDistance);
                break;
            case STATE.BATTLE:
                nav.stoppingDistance = 2f;

                break;
            case STATE.DIE:
                MonsterManager.I.KillingSlime();

                myAnim.SetTrigger("IsDie");
                break;
        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.NORMAL:
                normaltime += Time.deltaTime;
                if (normaltime > 2f)
                {
                    normaltime = 0f;
                    ChangeState(STATE.SEARCH);
                }
                break;
            case STATE.SEARCH:
                myAnim.SetFloat("Speed", 1f);
                //Debug.Log(nav.remainingDistance);
                if (nav.remainingDistance < 0.1f)
                {
                    ChangeState(STATE.NORMAL);
                }
                break;

            case STATE.BATTLE:
                nav.SetDestination(myRangeSystem.Target.position);
                Vector3 dir = myRangeSystem.Target.position - this.transform.position;
                dir.y = 0f;
                dir.Normalize();
                if (Vector3.Distance(myRangeSystem.Target.position, this.transform.position) > 5f)
                {
                    ChangeState(STATE.SEARCH);
                }
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.smoothDeltaTime * 10.0f); ;
                //myAnim.SetFloat("Speed", nav.velocity.magnitude / nav.speed);
                myAnim.SetFloat("Speed", 1f);

                if (AttackCooltime > Mathf.Epsilon)
                {
                    AttackCooltime -= Time.deltaTime;
                }
                else
                {
                    OnAttack();
                }

                break;
            case STATE.DIE:
                break;
        }
    }
    void OnAttack()
    {
        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            myAnim.SetTrigger("IsAttack");
            AttackCooltime = StartAttackCooltime;
            PlayerManager.I.PlayerOnDamage(Power);
            SoundManager.I.PlayEffectSound(Attack_S);
        }
        //if (Vector3.Distance(myRangeSystem.Target.position, this.transform.position) < 1.5f)
        //{
        //    myAnim.SetTrigger("IsAttack");
        //    AttackCooltime = StartAttackCooltime;
        //}


    }


    void OnBattle()
    {
        ChangeState(STATE.BATTLE);
    }
    void OnAttackTarget()
    {
        this.myRangeSystem.Target.GetComponent<BattleSystem>()?.OnDamage();

    }


    public void OnDamage()
    {
        myAnim.SetTrigger("IsHit");
        SoundManager.I.PlayEffectSound(Hit_S);
    }


    public void AddEffect_MoveEffect()
    {
        Instantiate(EffectManager.I.MoveEffect, this.transform.position, Quaternion.identity);
        //SoundManager.I.PlayEffectSound(Step_S);

    }
    public void AddDieSound()
    {
        SoundManager.I.PlayEffectSound(Die_S);

    }

    public float GetSlimeHealth()
    {
        return curHealth;
    }
    public bool GetSlimeDead()
    {
        return IsDead;
    }
}
