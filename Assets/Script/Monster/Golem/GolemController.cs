using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemController : Monster
{
    Animator myAnim;
    NavMeshAgent nav;

    RangeSystem myRangeSystem = null;
    AnimEvent myAnimEvent = null;

    Transform Attack2Breath_Pos;
    Transform Attack2Hole_Left;
    Transform Attack2Hole_Right;
    Transform Attack1Punch_Pos;

    List<Transform> AddMonsterPos = new List<Transform>();
    public float GolemHealth;

    float Boss_half_health;

    float AttackCooltime = 5f;
    float StartAttackCooltime = 0f;

    public bool isBossDead = false;

    public AudioClip Die_S;
    public AudioClip Shouting_S;

    enum STATE
    {
        NORMAL, CLOSER, BATTLE, DIE,VICTORY,JUMPATTACK,DIEBACK
    }

    [SerializeField]
    STATE myState = STATE.NORMAL;


    //GameObject AddDamageText;
    private void Awake()
    {
        myRangeSystem = GetComponentInChildren<RangeSystem>();
        myAnimEvent = GetComponent<AnimEvent>();
        myAnim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

        AttackCooltime = 5f;
        StartAttackCooltime = 0f;


        // 기본 스텟

        StartAttackCooltime = AttackCooltime;
        myRangeSystem.battle += this.OnBattle;
        myAnimEvent.Attack += this.OnAttackTarget;


        AddMonsterPos.Add(GameObject.Find("AddMonster_3").transform);
        AddMonsterPos.Add(GameObject.Find("AddMonster_4").transform);
        AddMonsterPos.Add(GameObject.Find("AddMonster_5").transform);

        Attack2Breath_Pos = GameObject.Find("Attack2Breath_Pos").transform;
        Attack2Hole_Left = GameObject.Find("Attack2Hole_Left").transform;
        Attack2Hole_Right = GameObject.Find("Attack2Hole_Right").transform;

        Attack1Punch_Pos = GameObject.Find("Attack1Punch_Pos").transform;
       // OnSet(MonsterManager.I.Golem_Health, MonsterManager.I.Golem_Power, MonsterManager.I.Golem_name);
    }
    private void Start()
    {
        OnSet(MonsterManager.I.Golem_Health, MonsterManager.I.Golem_Power, MonsterManager.I.Golem_name);

        Boss_half_health = curHealth * 0.6f;
        //Debug.Log(Boss_half_health);

    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead)
            ChangeState(STATE.DIE);
        else if (!IsDead)
            StateProcess();
        if (PlayerManager.I.IsDead)
            ChangeState(STATE.VICTORY);


    }
    void ChangeState(STATE s)
    {
        if (s == myState) return;

        myState = s;
        switch (myState)
        {
            case STATE.NORMAL:
                break;
            case STATE.BATTLE:
               // Debug.Log("BATTLE!");

                nav.stoppingDistance = 5f;
                myAnim.SetFloat("Speed", 1f);

                break;
            case STATE.DIE:
                OnDestoryCollider();
                myAnim.SetTrigger("IsDie");
                isBossDead = true;
                MonsterManager.I.BossDead = true;
                // 하나씩 생성하는 코루틴 필요.
                StopAllCoroutines();
                StartCoroutine(DropItems());
                //ChangeState(STATE.DIEBACK);
                break;
            case STATE.VICTORY:
                myAnim.SetTrigger("IsVictory");
                break;
            case STATE.JUMPATTACK:
                StopAllCoroutines();
                StartCoroutine(JumpAttack());
                break;
            case STATE.DIEBACK:
                //myAnim.SetTrigger("IsDie");

                break;

        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.BATTLE:
                nav.SetDestination(myRangeSystem.Target.position);
                Vector3 dir = myRangeSystem.Target.position - this.transform.position;
                dir.y = 0f;
                dir.Normalize();

                if (Vector3.Distance(myRangeSystem.Target.position, this.transform.position) > 10f)
                {
                   // Debug.Log("멀어영");
                    ChangeState(STATE.JUMPATTACK);
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
        }
    }
    IEnumerator DropItems()
    {
        int count = 0;
        Vector3 pung = new Vector3(0, 0, 0);

        Instantiate(EffectManager.I.Goldbox, this.transform.position + pung, Quaternion.identity);

        while (count <= 100)
        {
            
            Instantiate(EffectManager.I.Drop_Item_Coin, this.transform.position + pung, Quaternion.identity);
            Instantiate(EffectManager.I.Drop_Item_EXP, this.transform.position + pung, Quaternion.identity);

            yield return new WaitForSeconds(0.01f);

            
            count++;
        }
    }
    IEnumerator JumpAttack()
    {
        MonsterManager.I.Golem_IsJumping = true;
        nav.stoppingDistance = 2f;
        Instantiate(EffectManager.I.Attack_Jump_WarningZone, myRangeSystem.Target.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        float high = 0f;
        nav.speed = 300f;
        myAnim.SetTrigger("Attack_JumpStart");
        while (high < 5f)
        {
            high += Time.deltaTime * 3f;
            this.transform.position += new Vector3(0, high, 0);

            yield return null;
        }
        myAnim.SetTrigger("Attack_Jump");
        Instantiate(EffectManager.I.Attack_Jump_Explosion, this.transform.position, Quaternion.identity);

        nav.stoppingDistance = 2f;

        float DOWN = high;
        while (DOWN > 0f)
        {
            DOWN -= Time.deltaTime * 50f;
            this.transform.position += new Vector3(0, DOWN, 0);

            yield return null;
        }
        nav.speed = 0;
        yield return new WaitForSeconds(3f);
        
        nav.speed = 3.5f;
        nav.stoppingDistance = 5f;
        MonsterManager.I.Golem_IsJumping = false;
        ChangeState(STATE.BATTLE);
    }
    void OnAttack()
    {

        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            int RandomSkill = Random.Range(0, 5);
            if(curHealth >= Boss_half_health)
            {
                switch (RandomSkill)
                {
                    case 0:
                    case 1:
                    case 2:
                        myAnim.SetTrigger("Attack_Punch");
                        break;
                    case 3:
                    case 4:
                        nav.stoppingDistance = 10f;
                        myAnim.SetTrigger("Attack_Fire");
                        break;
                }
            }
            else
            {
                AttackCooltime = 4f;
                switch (RandomSkill)
                {
                    case 0:
                       myAnim.SetTrigger("IsAddMonster");
                       break;
                    case 1:
                    case 2:
                       myAnim.SetTrigger("Attack_Punch");
                       break;
                    case 3:
                    case 4:
                        nav.stoppingDistance = 10f;
                        myAnim.SetTrigger("Attack_Fire");
                        break;
                }

            }
            AttackCooltime = StartAttackCooltime;
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
    public void Add_Attack_Punch()
    { 
        Instantiate(EffectManager.I.Attack_Punch_Explosion, Attack1Punch_Pos.position, Attack1Punch_Pos.rotation);

    }
    public void Add_Attack_Fire()
    {
        Instantiate(EffectManager.I.Attack_Fire_Potal, Attack2Hole_Left.position, Attack2Hole_Left.rotation);
        Instantiate(EffectManager.I.Attack_Fire_Potal, Attack2Hole_Left.position, Attack2Hole_Left.rotation);

    }
    public void Add_Attack_Fire_missile()
    {
        Instantiate(EffectManager.I.Attack_Fire_Potal, Attack2Hole_Right.position, Attack2Hole_Right.rotation);
        Instantiate(EffectManager.I.Attack_Fire_Potal, Attack2Hole_Left.position, Attack2Hole_Left.rotation);

    }
    public void Add_Attack_Breath()
    {
        Instantiate(EffectManager.I.Attack_Fire_Breath, Attack2Breath_Pos.position, Attack2Breath_Pos.rotation);

    }
    public void Add_Monster()
    {
       for(int i = 0;i<AddMonsterPos.Count;i++)
        {
            Instantiate(EffectManager.I.AddMonster_Portal, AddMonsterPos[i].position, AddMonsterPos[i].rotation);


        }
     
    }
    public void Add_Shouting()
    {
        GameObject camera = GameObject.Find("PlayerCamera");
        StartCoroutine(camera.GetComponent<FollowPlayer_Camera>().Shake(1f, 0.5f));
        SoundManager.I.PlayEffectSound(Shouting_S);
    }

    public void Add_DieSound()
    {
        SoundManager.I.PlayEffectSound(Die_S);
    }
    public float GetGolemHealth()
    {
        return curHealth;
    }
    public bool GetGolemDead()
    {
        return isBossDead;
    }
    
}
