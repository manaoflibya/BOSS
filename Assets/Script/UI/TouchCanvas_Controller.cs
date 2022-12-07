using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchCanvas_Controller : MonoBehaviour
{
    GameObject Panel_Settings_Stage;
    GameObject Player_Die_Panel_;
    GameObject Player_Boss_Clear_;

    public Button Rolling_Button;
    public Image Rolling_Button_CT;

    public Button SkillAttack_Button;
    public Image SkillAttack_Button_CT;

    public Button HeavyAttack_Button;
    public Image HeavyAttack_Button_CT;

    enum CLOSENAME
    {
        SHOP, SETTING, HEROES, INVENTORY, UPGRADE, NOMOENY, BOSSROOM, BOSSROOM_IN, NOENERGY
    }
    enum COOLTIME
    {
        ROLLING,SPINATTACK,HEAVYATTACK
    }

    private void Awake()
    {
    }
    private void Start()
    {
        Panel_Settings_Stage = GameObject.Find("Panel_Settings_Stage");
        Player_Die_Panel_ = GameObject.Find("Player_Die_Panel_");
        Player_Boss_Clear_ = GameObject.Find("Player_Boss_Clear_");

        Rolling_Button = GameObject.Find("Rolling_Button").GetComponent<Button>();
        Rolling_Button_CT = GameObject.Find("Rolling_Button_CT").GetComponent<Image>();

        SkillAttack_Button = GameObject.Find("SkillAttack_Button").GetComponent<Button>();
        SkillAttack_Button_CT = GameObject.Find("SkillAttack_Button_CT").GetComponent<Image>();

        HeavyAttack_Button = GameObject.Find("HeavyAttack_Button").GetComponent<Button>();
        HeavyAttack_Button_CT = GameObject.Find("HeavyAttack_Button_CT").GetComponent<Image>();



        Panel_Settings_Stage.SetActive(false);
        Player_Die_Panel_.SetActive(false);
        Player_Boss_Clear_.SetActive(false);


    }
    private void Update()
    {
        if (PlayerManager.I.IsDead)
        {
            Player_Die_Panel_.SetActive(true);
        }
        else if (MonsterManager.I.BossDead)
        {
            StartCoroutine(StartTime());    
        }

        if(PlayerManager.I.IsSpinAttack)
        {
            Rolling_Button.enabled = false;
        }
        else
        {
            Rolling_Button.enabled = true;
        }
    }
    public void HeavyAttackButtonClick()
    {
        StartCoroutine(CoolTime(COOLTIME.HEAVYATTACK));
    }
    public void SpinAttackButtonClick()
    {
        StartCoroutine(CoolTime(COOLTIME.SPINATTACK));

    }
    public void RollingButtonClick()
    {
        StartCoroutine(CoolTime(COOLTIME.ROLLING));
        Rolling_Button.enabled = false;
    }
    public void OpenSetting()
    {
        Panel_Settings_Stage.SetActive(true);
        Panel_Settings_Stage.GetComponent<Animator>().SetTrigger("Setting_Start");
        StartCoroutine(StopTime(0.5f));
    }
    public void CloseSetting()
    {
        Time.timeScale = 1f;
        Panel_Settings_Stage.GetComponent<Animator>().SetTrigger("Setting_End");
        StartCoroutine(CloseTime(CLOSENAME.SETTING));
    }

    IEnumerator CoolTime(COOLTIME c)
    {
        float runtime = 0f;
        float curtime = 0f;

        switch (c)
        {
            case COOLTIME.ROLLING:
                runtime = 1f;
                Rolling_Button.enabled = false;
                break;
            case COOLTIME.HEAVYATTACK:
                runtime = 1f;
                HeavyAttack_Button.enabled = false;
                break;
            case COOLTIME.SPINATTACK:
                runtime = 1f;
                SkillAttack_Button.enabled = false;
                break;

        }

        while(runtime > curtime)
        {
            switch (c)
            {
                case COOLTIME.ROLLING:
                    runtime -= Time.deltaTime * 0.85f;

                    Rolling_Button_CT.fillAmount = runtime;
                    break;
                case COOLTIME.HEAVYATTACK:
                    runtime -= Time.deltaTime * 0.1f;
                    HeavyAttack_Button_CT.fillAmount = runtime;
                     break;
                case COOLTIME.SPINATTACK:
                    runtime -= Time.deltaTime * 0.1f;
                    SkillAttack_Button_CT.fillAmount = runtime;
                    break;
            }
            yield return null;
        }
        switch (c)
        {
            case COOLTIME.ROLLING:
                Rolling_Button.enabled = true;
                break;
            case COOLTIME.HEAVYATTACK:
                HeavyAttack_Button.enabled = true;
                break;
            case COOLTIME.SPINATTACK:
                SkillAttack_Button.enabled = true;
                break;


        }

    }
    IEnumerator StopTime(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 0f;
    }
    IEnumerator StartTime(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 1f;

    }
    IEnumerator CloseTime(CLOSENAME name)
    {
        yield return new WaitForSeconds(0.3f);

        switch (name)
        {
            case CLOSENAME.SETTING:
                Panel_Settings_Stage.SetActive(false);

                break;

        }

    }

    IEnumerator StartTime()
    {
        yield return new WaitForSeconds(3f);

        Player_Boss_Clear_.SetActive(true);
        PlayerActions player = FindObjectOfType<PlayerActions>();

    }
    public void MainMenuButton()
    {
        PlayerManager.I.GEM += 3;
        PlayerManager.I.Gold += 1500;
        JsonManager.I.SavePlayerDataToJson_BossClear();
        Time.timeScale = 1f;
        LoadingCanvas_Controller.LoadSene("MainMenu");

    }
    public void MainMenuButton_NoneClear()
    {
        Time.timeScale = 1f;
        LoadingCanvas_Controller.LoadSene("MainMenu");

    }
    public void Start_Player_Boss_Clear_Boss_Loop()
    {
        GameObject loopAnim = GameObject.Find("BossClear_Animator");
        loopAnim.GetComponent<Animator>().SetTrigger("StartLoop");
    }
    public void AddPlayerDieSound()
    {
    }
}
