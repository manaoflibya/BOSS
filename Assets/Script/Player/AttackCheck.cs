using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackCheck : MonoBehaviour
{
    public GameObject Slider_HealthBar_Miniboss_;
    public GameObject Slider_HealthBar_Boss_;

    Slider MonsterSlider;
    Slider BossSlider;
    float slimeMax;
    float dragonMax;
    float GolemMax;

    float health;
    float GolemHealth;
    GolemController golem;
    bool IsSliderOn;

    public AudioClip slimeHit_S;
    public AudioClip dragonHit_S;
    public AudioClip GolemHit_S;
    public AudioClip DummyHit_S;

    enum STATE
    {
       NONE, SLIME,DRAGON,GOLEM
    }
    STATE myState = STATE.NONE;

    private void Awake()
    {
        MonsterSlider = Slider_HealthBar_Miniboss_.GetComponent<Slider>();
        BossSlider = Slider_HealthBar_Boss_.GetComponent<Slider>();


    }
    private void Start()
    {
        golem = FindObjectOfType<GolemController>();

        Slider_HealthBar_Boss_.SetActive(false);
        Slider_HealthBar_Miniboss_.SetActive(false);
        IsSliderOn = false;


        slimeMax = 1 / MonsterManager.I.Slime_MaxHealth;
        dragonMax = 1 / MonsterManager.I.Dragon_MaxHealth;
        GolemMax = 1 / MonsterManager.I.Golem_MaxHealth;
        

    }
    private void Update()
    {
        if(MonsterManager.I.Golem_Stage_Start && MonsterManager.I.Golem_Health >= 0.1f)
        {
            Slider_HealthBar_Boss_.SetActive(true);

            GolemHealth = golem.GetGolemHealth();
            BossSlider.value = (GolemHealth * GolemMax);
        }
        else if(IsSliderOn)
        {
            //mySlider.value = (health * slimeMax);
            switch(myState)
            {
                case STATE.SLIME:
                    Slider_HealthBar_Miniboss_.SetActive(true);
                    if (!PlayerManager.I.IsCameraChange)
                    {
                        TextMeshProUGUI MonsterName = GameObject.Find("MonsterName_Text").GetComponent<TextMeshProUGUI>();
                        MonsterName.text = "Slime";
                        MonsterSlider.value = (health * slimeMax);
                    }

                    if (MonsterSlider.value <= Mathf.Epsilon || PlayerManager.I.IsCameraChange)
                    {
                        IsSliderOn = false;
                        Slider_HealthBar_Miniboss_.SetActive(false);
                        myState = STATE.NONE;
                    }


                    break;
                case STATE.DRAGON:
                    Slider_HealthBar_Miniboss_.SetActive(true);
                    if (!PlayerManager.I.IsCameraChange)
                    {
                        TextMeshProUGUI MonsterName = GameObject.Find("MonsterName_Text").GetComponent<TextMeshProUGUI>();
                        MonsterName.text = "Dragon";
                        MonsterSlider.value = (health * dragonMax);

                    }

                    if (MonsterSlider.value <= Mathf.Epsilon || PlayerManager.I.IsCameraChange)
                    {
                        IsSliderOn = false;
                        Slider_HealthBar_Miniboss_.SetActive(false);
                        myState = STATE.NONE;

                    }
                    break;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(PlayerManager.I.IsAttack)
        {
            if (other.tag == "Slime")
            {
                IsSliderOn = true;
                myState = STATE.SLIME;
                Instantiate(EffectManager.I.AttackEffect, other.transform.position,Quaternion.identity);
               // SoundManager.I.PlayEffectSound(slimeHit_S);


                health =  other.GetComponent<SlimeController>().GetSlimeHealth();



            }
            if (other.tag == "Dragon")
            {
                IsSliderOn = true;
                myState = STATE.DRAGON;
                Instantiate(EffectManager.I.AttackEffect, other.transform.position, Quaternion.identity);
               // SoundManager.I.PlayEffectSound(dragonHit_S);

                health = other.GetComponent<DragonController>().GetDragonHealth();



            }
            if (other.tag == "Golem")
            {
                myState = STATE.GOLEM;
                Instantiate(EffectManager.I.AttackEffect, other.transform.position, Quaternion.identity);
              //  SoundManager.I.PlayEffectSound(GolemHit_S);



            }
            if (other.tag == "Dummy")
            {
                Instantiate(EffectManager.I.AttackEffect, other.transform.position, Quaternion.identity);
              //  SoundManager.I.PlayEffectSound(DummyHit_S);

            }
        }
    }


}
