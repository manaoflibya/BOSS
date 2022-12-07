using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class MapTriggersController : MonoBehaviour
{
    public List<PlayableDirector> playList;
    GameObject PlayerCamera;
    GameObject Area1Camera;
    GameObject Area2Camera;
    GameObject Area3Camera;
    GameObject Area4Camera;
    
    
    GameObject TouchCanvas;
    GameObject InforCanvas;

    GameObject PlayScene_BossWarning_;

    enum TRIGGER
    {
        AREA0,AREA1,AREA2,AREA3,AREA4

    }
    TRIGGER mytrigger = TRIGGER.AREA0;

    private void Awake()
    {
        Area1Camera = GameObject.Find("Area1Camera");
        Area2Camera = GameObject.Find("Area2Camera");
        Area3Camera = GameObject.Find("Area3Camera");
        Area4Camera = GameObject.Find("Area4Camera");


        TouchCanvas = GameObject.Find("TouchCanvas");
        InforCanvas = GameObject.Find("InforCanvas");

        Area1Camera.SetActive(false);
        Area2Camera.SetActive(false);
        Area3Camera.SetActive(false);
        Area4Camera.SetActive(false);

        PlayerCamera = GameObject.Find("PlayerCamera");
        StartCoroutine(CameraChangeTime());

        playList[0].Play();
        playList[1].Stop();
        playList[2].Stop();
        playList[3].Stop();
        playList[4].Stop();

        PlayScene_BossWarning_ = GameObject.Find("PlayScene_BossWarning_");
        PlayScene_BossWarning_.SetActive(false);


    }
    void Update() 
    {
       // Debug.Log(MonsterManager.I.SlimeCount);
        //Debug.Log(MonsterManager.I.DragonCount);

        if(!MonsterManager.I.Golem_CameraFinish)
        {
            if(MonsterManager.I.DragonCount == 3)
            {
                ChangeState(TRIGGER.AREA3);
            }
            else if(MonsterManager.I.DragonCount == 1 && MonsterManager.I.SlimeCount == 4)
            {
                ChangeState(TRIGGER.AREA2);
            }
            else if (MonsterManager.I.SlimeCount == 4 && mytrigger != TRIGGER.AREA2)
            {
                ChangeState(TRIGGER.AREA1);
            }

        }

        StateProcess();
    }

    void ChangeState(TRIGGER T)
    {
        if (mytrigger == T) return;

        mytrigger = T;
        AttackCheck attackcheck = FindObjectOfType<AttackCheck>();
        attackcheck.Slider_HealthBar_Miniboss_.SetActive(false);
        PlayerManager.I.IsCameraChange = true;

        switch (mytrigger)
        {
            case TRIGGER.AREA1:
                //Debug.Log("Is Area 1 ");
                Area1Camera.SetActive(true);

                StartCoroutine(CameraChangeTime());
                playList[1].Play();

                break;
            case TRIGGER.AREA2:
                //Debug.Log("Is Area 2 ");

                Area2Camera.SetActive(true);

                StartCoroutine(CameraChangeTime());
                playList[2].Play();

                break;
            case TRIGGER.AREA3:
                Area3Camera.SetActive(true);

                StartCoroutine(CameraChangeTime());
                playList[3].Play();
                break;
            case TRIGGER.AREA4:
                MonsterManager.I.Golem_CameraFinish = true;
                Area4Camera.SetActive(true);

                StartCoroutine(CameraChangeTime());
                playList[4].Play();
                break;
        }
    }

    void StateProcess()
    {
        switch (mytrigger)
        {
            case TRIGGER.AREA1:
                break;
            case TRIGGER.AREA2:
                break;
            case TRIGGER.AREA3:
                break;
            case TRIGGER.AREA4:
                break;
        }
    }
    IEnumerator CameraChangeTime()
    {

        //카메라가 이동하면서 캐릭터 멈추게

        if (mytrigger != TRIGGER.AREA0)
        {
            JoystickController joy = FindObjectOfType<JoystickController>();
            joy.ResetPoint();


            TouchCanvas_Controller touch = FindObjectOfType<TouchCanvas_Controller>();
            touch.Rolling_Button_CT.fillAmount = 0;
            touch.SkillAttack_Button_CT.fillAmount = 0;
            touch.HeavyAttack_Button_CT.fillAmount = 0;

            touch.Rolling_Button.enabled = true;
            touch.SkillAttack_Button.enabled = true;
            touch.HeavyAttack_Button.enabled = true;

        }

        PlayerManager.I.IsStop = true;

        TouchCanvas.SetActive(false);
        InforCanvas.SetActive(false);

        yield return new WaitForSeconds(0.25f);

        float curTime;

        if (MonsterManager.I.Golem_CameraFinish)
        {
            curTime = -3f;
            Debug.Log(MonsterManager.I.Golem_CameraFinish);
        }
        else
            curTime = 0f;

        PlayerCamera.SetActive(false);

        while (curTime < 3)
        {
            curTime += Time.deltaTime;
            yield return null;
        }

        PlayerCamera.SetActive(true);
        TouchCanvas.SetActive(true);
        InforCanvas.SetActive(true);

        PlayerManager.I.IsCameraChange = false;
        PlayerManager.I.IsStop = false;

        switch (mytrigger)
        {
            case TRIGGER.AREA0:
                Destroy(GameObject.Find("Area0Camera"));

                break;
            case TRIGGER.AREA1:
                MonsterManager.I.SlimeCount = 0;
                Destroy(GameObject.Find("Area1Camera"));
                break;
            case TRIGGER.AREA2:
                MonsterManager.I.SlimeCount = 0;
                Destroy(GameObject.Find("Area2Camera"));
                break;
            case TRIGGER.AREA3:
                Destroy(GameObject.Find("Area3Camera"));
                ChangeState(TRIGGER.AREA4);
                break;
            case TRIGGER.AREA4:
                Destroy(GameObject.Find("Area4Camera"));
                MonsterManager.I.Golem_Stage_Start = true;
                PlayScene_BossWarning_.SetActive(false);
                break;


        }
    }
}
