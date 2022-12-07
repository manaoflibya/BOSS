using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonController : Monster
{
    Animator myAnim;
    Transform Target;
    Material mat;
    NavMeshAgent nav;

    RangeSystem myRangeSystem = null;
    AnimEvent myAnimEvent = null;
    Vector3 StartPos;

    Transform Dragon_SkillPos;

    float AttackCooltime = 4f;
    float StartAttackCooltime = 0f;
    float normaltime;

    enum STATE
    {
        NORMAL, SEARCH, CLOSER, BATTLE, DIE
    }

    [SerializeField]
    STATE myState = STATE.NORMAL;


    //GameObject AddDamageText;
    private void Awake()
    {
        Dragon_SkillPos = GameObject.Find("Dragon_SkillPos").transform;
        myRangeSystem = GetComponentInChildren<RangeSystem>();
        myAnimEvent = GetComponent<AnimEvent>();
        Target = GameObject.Find("Player").transform;
        myAnim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

        // ±âº» ½ºÅÝ
        StartPos = this.transform.position;

        StartAttackCooltime = AttackCooltime;
        //
        myRangeSystem.battle += this.OnBattle;
        myAnimEvent.Attack += this.OnAttackTarget;
    }
    private void Start()
    {
        OnSet(MonsterManager.I.Dragon_Health, MonsterManager.I.Dragon_Power, MonsterManager.I.Dragon_name);

    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead)
            ChangeState(STATE.DIE);
        else if (!IsDead)
            StateProcess();

        if (MonsterManager.I.BossDead)
            ChangeState(STATE.DIE);
    }
    void ChangeState(STATE s)
    {
        if (s == myState) return;

        myState = s;
        switch (myState)
        {
            case STATE.NORMAL:
                myAnim.SetFloat("Speed", 0f);
                break;
            case STATE.SEARCH:
                myAnim.SetFloat("Speed", 1f);

                Vector3 dir = Vector3.zero;
                dir.x = Random.Range(-1.0f, 1.0f);
                dir.z = Random.Range(-1.0f, 1.0f);
                dir.Normalize(); 

                nav.SetDestination(StartPos + dir * Random.Range(2.0f, 2.0f));
                //Debug.Log(nav.remainingDistance);
                break;
            case STATE.BATTLE:
                nav.stoppingDistance = 5f;
                myAnim.SetFloat("Speed", 1f);

                break;
            case STATE.DIE:
                MonsterManager.I.KillingDragon();
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
                if (Vector3.Distance(myRangeSystem.Target.position, this.transform.position) > 8f)
                {
                    ChangeState(STATE.SEARCH);
                }
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.smoothDeltaTime * 10.0f); ;

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
        int RandomSkill = Random.Range(0, 4);

        if (nav.remainingDistance <= nav.stoppingDistance)
        {

            switch (RandomSkill)
            {
                case 0:
                case 1:
                case 2:
                    myAnim.SetTrigger("IsFire");

                    break;
                case 3:
                case 4:

                    myAnim.SetTrigger("IsBreath");
                    break;

            }
            AttackCooltime = StartAttackCooltime;
            //PlayerManager.I.PlayerOnDamage(Power);
        }


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
    }
    public void AddBreath()
    {
        nav.angularSpeed = 0f;
        nav.speed = 0f;
        Vector3 offset = new Vector3(0, 1f, 0f);

        Instantiate(EffectManager.I.FireBreath,this.transform.position+offset,this.transform.rotation);

    }
    public void FinishBreath()
    {
        nav.angularSpeed = 120f;
        nav.speed = 3.5f;

    }
    public void AddFireBall()
    {
        Vector3 offset = new Vector3(0, 1f, 0f);
        Instantiate(EffectManager.I.FireBall, this.transform.position + offset, Quaternion.identity);

    }
    public float GetDragonHealth()
    {
        return curHealth;
    }
    public bool GetDragonDead()
    {
        return IsDead;
    }
}
