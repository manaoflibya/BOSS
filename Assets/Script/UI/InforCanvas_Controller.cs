using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InforCanvas_Controller : MonoBehaviour
{
    GameObject Player_Health_Bar;
    GameObject Player_Level_Text;
    GameObject Player_EXP_Bar;
    //GameObject PlayScene_BossWarning_;

    float PlayerHealthChangeValue;
    float EXP_ChangeValue;

    private void Awake()
    {
        Player_Health_Bar = GameObject.Find("Player_Health_Bar");
        Player_Level_Text = GameObject.Find("Player_Level_Text");
        Player_EXP_Bar = GameObject.Find("Player_EXP_Bar");
        PlayerHealthChangeValue = 1 / PlayerManager.I.Health;
        EXP_ChangeValue = PlayerManager.I.MAX_EXP;
        EXP_ChangeValue = 1/ EXP_ChangeValue;
        //PlayScene_BossWarning_ = GameObject.Find("PlayScene_BossWarning_");

        //PlayScene_BossWarning_.SetActive(false);


    }
    private void Start()
    {
    }
    
    //적들의 체력바 관리는 attackcheck.cs에서 함.
    
    void Update()
    {

        Player_Health_Bar.GetComponent<Slider>().value = PlayerManager.I.Health * PlayerHealthChangeValue;
        Player_Level_Text.GetComponent<TextMeshProUGUI>().text = PlayerManager.I.Level.ToString();
        Player_EXP_Bar.GetComponent<Slider>().value = (PlayerManager.I.Player_EXP * EXP_ChangeValue);
        // Debug.Log(PlayerManager.I.Player_EXP);
        //if 보스가 등장하면 boss warning!
    }
}
