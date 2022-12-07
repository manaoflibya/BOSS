using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    public enum MOVESTATE
    {
        IDLE, RUN, STOP, ROLLING, SKILLATTACK,DIE,VICTORY
    }
    public JoystickValue joystick;

    public MOVESTATE MyState = MOVESTATE.IDLE;
    public Animator myAnim;

    //플레이어 이동속도
    public float runSpeed = 2.5f;
    public float MaxSpeed = 3f;
    float lookX;
    float lookZ;

    //플레이어 방향
    public Vector3 lookDirection;

    // 기본공격(연속 동작에서 사용)
    public int noOfClicks = 0;
    float lastclikedTime = 0;
    public float maxComboDelay = 0.5f;

    // 구르기
    Rigidbody rig;
    GameObject RollingDIR;
    // 공격 이펙트
    Transform EffectPos;

    Transform HeavyAttackPos;
    Transform HeavyAttackPos_2;

    Transform SkillAttackPos_1;
    Transform SkillAttackPos_2;
    Transform SkillAttackPos_3;

    Transform MoveEffectPos;

    //Material mat;
    //Material Startmat;
    GolemController Boss;
    
    //이동 관련 laycast
    Vector3 layVec;
    Vector3 offset;

    Transform RayPos;
    Quaternion quaternion;
    public LayerMask Mask;
    RaycastHit hit;
    bool IsFloor;
    bool IsVictory = false;

    //audio
    public AudioClip Rolling_S;
    public AudioClip Run_S;
    public AudioClip Victory_S_1;
    public AudioClip Die_S;
   // public AudioClip BGM;
    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();

        Boss = FindObjectOfType<GolemController>();

        HeavyAttackPos = GameObject.Find("HeavyAttackPos").transform;
        HeavyAttackPos_2 = GameObject.Find("HeavyAttackPos_2").transform;
        EffectPos = GameObject.Find("EffectPos").transform;
        SkillAttackPos_1 = GameObject.Find("Hand_R").transform;
        SkillAttackPos_2 = GameObject.Find("center").transform;
        SkillAttackPos_3 = GameObject.Find("SkillAttackPos_3").transform;
        MoveEffectPos = GameObject.Find("MoveEffectPos").transform;

        RayPos = GameObject.Find("RayPos").transform;
        RollingDIR = GameObject.Find("RollingDir");
        // mat = GetComponent<Material>();
        // mat.color = Color.red;
        //Startmat = GetComponent<Material>();
        //SoundManager.I.PlayBMG(BGM);
    }



    // Update is called once per frame
    void Update()
    {
       // StopAllCoroutines();
        StartCoroutine(ShootRay());


        if (PlayerManager.I.IsHit && !PlayerManager.I.IsDead)
        {
            myAnim.SetTrigger("IsHit");
            Instantiate(EffectManager.I.HitEffect, SkillAttackPos_2.position, Quaternion.identity);

            PlayerManager.I.IsHit = false;
        }
        else if(PlayerManager.I.IsDead)
        {
            ChangeState(MOVESTATE.DIE);

        }
        //보스 있을 시
        if (Boss.isBossDead)
        {
            ChangeState(MOVESTATE.VICTORY);

        }


        // if (!PlayerManager.I.IsDead && !Boss.isBossDead)
        if (!PlayerManager.I.IsDead)
        {

            //if (Input.GetKeyDown(KeyCode.Space) && !PlayerManager.I.IsAttack && !PlayerManager.I.IsRolling)
            //{
            //    ChangeState(MOVESTATE.ROLLING);

            //}
            //else if (Input.GetAxis("Horizontal") > 0.2f ||
            // Input.GetAxis("Vertical") > 0.2f ||
            // Input.GetAxis("Horizontal") < -0.2f ||
            // Input.GetAxis("Vertical") < -0.2f)
            //{
            //    if (!PlayerManager.I.IsRolling)
            //        ChangeState(MOVESTATE.RUN);
            //}
            //else if (Input.GetAxis("Vertical") == 0f &&
            //    Input.GetAxis("Horizontal") == 0f)
            //{

            //    ChangeState(MOVESTATE.IDLE);
            //}

            // joy stick

            if (Input.GetKeyDown(KeyCode.Space) && !PlayerManager.I.IsAttack && !PlayerManager.I.IsRolling)
            {
                ChangeState(MOVESTATE.ROLLING);
            }
            else if (joystick.joyTouch.x > 0.2f ||
             joystick.joyTouch.y > 0.2f ||
             joystick.joyTouch.x < -0.2f ||
             joystick.joyTouch.y < -0.2f)
            {

                ChangeState(MOVESTATE.RUN);
            }
            else if (joystick.joyTouch.x == 0f &&
                joystick.joyTouch.y == 0f)
            {

                ChangeState(MOVESTATE.IDLE);
            }

            HeavyAttack();
            ComboAttack();
            SkillAttack();
            StateProcess();

        }
    }
    IEnumerator ShootRay()
    {
        layVec = quaternion *  Vector3.down;
        Debug.DrawRay(RayPos.position, layVec, Color.red);
        quaternion = Quaternion.Euler(0f, this.transform.eulerAngles.y, 0f);
        if (Physics.Raycast(RayPos.position, layVec, out hit, 4))
        {
            //Debug.Log(hit.transform.gameObject.layer.ToString());
            if (hit.transform.gameObject.layer == 14 || hit.transform.gameObject.layer == 24|| hit.transform.gameObject.layer == 15||
                hit.transform.gameObject.layer ==19 || hit.transform.gameObject.layer ==0 || hit.transform.gameObject.layer == 27||
                hit.transform.gameObject.layer == 20 || hit.transform.gameObject.layer == 26|| hit.transform.gameObject.layer == 30
                || hit.transform.gameObject.layer == 29)
            {
                // Debug.Log(hit.transform.gameObject.layer.ToString());
                //Debug.Log("맞음");
                PlayerManager.I.StopRolling = false;
                if (PlayerManager.I.IsRolling)
                    runSpeed = 5.5f;
                else if (PlayerManager.I.IsSpinAttack)
                    runSpeed = 2f;
                else
                    runSpeed = 3.5f;

            }
            else
            {
                PlayerManager.I.StopRolling = true;
                runSpeed = 0f;
            }
        }
        else // ray를 쐇는데도 불구하고 안맞는 경우
        {
            //Debug.Log("안맞음");
            PlayerManager.I.StopRolling = true;
            runSpeed = 0f;

        }
            // Debug.Log(runSpeed);

        yield return new WaitForSeconds(0.00001f);
    }


    void ChangeState(MOVESTATE s)
    {
        if (s == MyState) return;
        MyState = s;

        switch (MyState)
        {
            case MOVESTATE.IDLE:
                break;
            case MOVESTATE.RUN:
                //StartCoroutine(FastPlayerSpeed());
                PlayerManager.I.IsAttack = false;

                break;
            case MOVESTATE.ROLLING:

                StartCoroutine(RollingFunc());
                myAnim.SetTrigger("Rolling");
                PlayerManager.I.IsAttack = false;
                // *= 2f;

                break;
            case MOVESTATE.SKILLATTACK:
                PlayerManager.I.IsAttack = true;

                myAnim.SetBool("IsSkillAttack", true);
                StartCoroutine(SkillAttackTime());
                break;
            case MOVESTATE.DIE:
                myAnim.SetBool("IsDead", true);
                break;
            case MOVESTATE.VICTORY:
                // Debug.Log(1);
                if (IsVictory)
                    break;
                else
                {
                    SoundManager.I.PlayEffectSound(Victory_S_1);
                    Debug.Log(1);
                    myAnim.SetTrigger("IsVictory");
                    IsVictory = true;
                    break;

                }
        }
    }
    void StateProcess()
    {
        switch (MyState)
        {
            case MOVESTATE.IDLE:

                myAnim.SetFloat("x", 0f);
                myAnim.SetFloat("y", 0f);


                break;
            case MOVESTATE.RUN:
                if (!PlayerManager.I.IsStop && !PlayerManager.I.IsRolling && !PlayerManager.I.IsCameraChange)
                {
                    Vector3 StartRot = this.transform.rotation.eulerAngles;

                    ///
                    myAnim.SetFloat("x", joystick.joyTouch.x);
                    myAnim.SetFloat("y", joystick.joyTouch.y);


                    lookX = joystick.joyTouch.y;
                    lookZ = joystick.joyTouch.x;

                    ///////////

                    //myAnim.SetFloat("x", Input.GetAxis("Vertical"));
                    //myAnim.SetFloat("y", Input.GetAxis("Horizontal"));

                    //lookX = Input.GetAxis("Vertical");
                    //lookZ = Input.GetAxis("Horizontal");


                    lookDirection = lookX * Vector3.forward + lookZ * Vector3.right;
                    lookDirection.Normalize();


                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * 25);
                    this.transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);

                }

                break;
            case MOVESTATE.ROLLING:

                break;
        }
    }
    IEnumerator RollingFunc()
    {
        yield return new WaitForSeconds(0.1f);
        float startTime = Time.time;
        float rollingtime = 1f;
        while(Time.time < startTime + rollingtime)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, RollingDIR.transform.position, Time.deltaTime*runSpeed);

            if(PlayerManager.I.StopRolling)
            {
                PlayerManager.I.StopRolling = false;

                yield break;
            }
            yield return null;
        }

    }
    public void OnRolling()
    {
        PlayerManager.I.IsRolling = true;
        SoundManager.I.PlayEffectSound(Rolling_S);
    }
    public void OFFRolling()
    {
        PlayerManager.I.IsRolling = false;
        //if (IsFloor)
        //    runSpeed = 5f;
        //else
        //    runSpeed = 0f;
        ChangeState(MOVESTATE.IDLE);
    }


    IEnumerator SkillAttackTime()
    {
        float SkillAttackTime = 3f;

        while (SkillAttackTime > 0f)
        {

            PlayerManager.I.IsAttack = true;
            SkillAttackTime -= Time.deltaTime;

            yield return null;
        }

        myAnim.SetBool("IsSkillAttack", false);
        PlayerManager.I.IsAttack = false;

    }

    void SkillAttack()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            myAnim.SetTrigger("SkillAttack");
            ChangeState(MOVESTATE.SKILLATTACK);
        }
    }
    void HeavyAttack()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            myAnim.SetTrigger("HeavyAttack");
        }
    }
    void ComboAttack()
    {
        if (Time.time - lastclikedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerManager.I.IsAttack = true;
            lastclikedTime = Time.time;
            noOfClicks++;
            if (noOfClicks == 1)
            {
                myAnim.SetBool("Attack1", true);
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
        }
        else if(joystick.ComboAttackTouch)
        {
            PlayerManager.I.IsAttack = true;
            joystick.ComboAttackTouch = false;
            lastclikedTime = Time.time;
            noOfClicks++;
            if (noOfClicks == 1)
            {
                myAnim.SetBool("Attack1", true);
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
        }
    }
    public void ComboAttackButtonClick()
    {
        PlayerManager.I.IsAttack = true;
        lastclikedTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 1)
        {
            myAnim.SetBool("Attack1", true);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
    }
    public void HeavyAttackButtonClick()
    {
        myAnim.SetTrigger("HeavyAttack");

    }
    public void SkillAttackButtonClick()
    {
        myAnim.SetTrigger("SkillAttack");
        ChangeState(MOVESTATE.SKILLATTACK);

    }
    public void RollingButtonClick()
    {
        if(!PlayerManager.I.IsAttack && !PlayerManager.I.IsRolling)
        {
            ChangeState(MOVESTATE.ROLLING);

        }    
    }
    ////////////애니메이션에서 사용 //////////////
    public void Attack1()
    {
        if (noOfClicks >= 2)
        {
            PlayerManager.I.IsAttack = true;

            myAnim.SetBool("Attack2", true);

        }
        else
        {
            PlayerManager.I.IsAttack = false;

            myAnim.SetBool("Attack1", false);
            noOfClicks = 0;
        }
    }
    public void Attack2()
    {
        if (noOfClicks >= 2)
        {
            PlayerManager.I.IsAttack = true;

            myAnim.SetBool("Attack3", true);

        }
        else
        {
            PlayerManager.I.IsAttack = false;

            myAnim.SetBool("Attack1", false);

            myAnim.SetBool("Attack2", false);
            noOfClicks = 0;
        }
    }
    public void Attack3()
    {
        PlayerManager.I.IsAttack = false;

        myAnim.SetBool("Attack1", false);
        myAnim.SetBool("Attack2", false);
        myAnim.SetBool("Attack3", false);
        noOfClicks = 0;
    }
    public void HeavyAttackStart()
    {
        PlayerManager.I.IsAttack = true;

    }
    public void HeavyAttackFinish()
    {
        PlayerManager.I.IsAttack = false;

    }
    public void DontMoveCharacter()
    {
        PlayerManager.I.IsStop = true;

    }
    public void MoveCharacter()
    {
        PlayerManager.I.IsStop = false;
    }
    public void AddEffect_NORMAL_Attack()
    {
        Instantiate(EffectManager.I.NormalAttackEffect, HeavyAttackPos_2);

    }
    public void AddEffect_NORMAL_Attack_1()
    {
        Instantiate(EffectManager.I.NormalAttackEffect1, EffectPos.position, Quaternion.identity);
    }
    public void AddEffect_NORMAL_Attack_2()
    {
        Instantiate(EffectManager.I.NormalAttackEffect2, EffectPos.position, Quaternion.identity);
    }
    public void AddEffect_Heavy_Attack_1()
    {
        //Instantiate(HeavyAttackEffect1, HeavyAttackPos.position, Quaternion.LookRotation(this.transform.rotation.eulerAngles));
        Instantiate(EffectManager.I.HeavyAttackEffect1, HeavyAttackPos.position, Quaternion.identity);
    }
    public void AddEffect_Heavy_Attack_2()
    {
        //Instantiate(HeavyAttackEffect1, HeavyAttackPos.position, Quaternion.LookRotation(this.transform.rotation.eulerAngles));
        Instantiate(EffectManager.I.HeavyAttackEffect2, HeavyAttackPos_2.position, Quaternion.identity);
        Instantiate(EffectManager.I.HeavyAttackEffect3, HeavyAttackPos_2.position, Quaternion.identity);
    }
    public void AddEffect_Skill_Charge()
    {
        Instantiate(EffectManager.I.SkillCharge, SkillAttackPos_1.position,Quaternion.identity);
    }
    public void AddEffect_Skill_Spin()
    {
       // Instantiate(EffectManager.I.SkillSpin, SkillAttackPos_2.position, Quaternion.identity);
        Instantiate(EffectManager.I.SkillSpin, SkillAttackPos_2.position, Quaternion.LookRotation(this.transform.rotation.eulerAngles));
    }
    public void OnSpinAttack()
    {
        PlayerManager.I.IsSpinAttack = true;

    }
    public void OffSpinAttack()
    {
        PlayerManager.I.IsSpinAttack = false;

    }
    public void AddEffect_Skill_Star()
    {
        Instantiate(EffectManager.I.SkillStar, SkillAttackPos_3.position, Quaternion.LookRotation(this.transform.rotation.eulerAngles));
    }
    public void AddEffect_MoveEffect()
    {
        Instantiate(EffectManager.I.MoveEffectPlayer, MoveEffectPos.position, Quaternion.identity);
        //SoundManager.I.PlayEffectSound(Run_S);
    }
    public void AddVictoryEmoji()
    {
        Vector3 upVec = new Vector3(0, 3f, 0f);
        Instantiate(EffectManager.I.Player_VictoryEmoji, this.transform.position + upVec, Quaternion.identity);

    }

}
