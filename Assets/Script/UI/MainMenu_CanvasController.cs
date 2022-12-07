using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu_CanvasController : MonoBehaviour
{

    GameObject Level_Text;
    GameObject Energy_Text;
    GameObject Gem_Text;
    GameObject Gold_Text;
    GameObject Level_Slider_Text;
    GameObject Level_Slider;
    float EXP_ChangeValue;


    private void Awake()
    {
        Level_Text = GameObject.Find("Level_Text");
        Energy_Text = GameObject.Find("Energy_Text");
        Gem_Text = GameObject.Find("Gem_Text");
        Gold_Text = GameObject.Find("Gold_Text");
        Level_Slider = GameObject.Find("Level_Slider");
        Level_Slider_Text = GameObject.Find("Level_Slider_Text");

    }
    private void Start()
    {
        //Level_Text.GetComponent<TextMeshProUGUI>().text = PlayerManager.I.Level.ToString();
        //Energy_Text.GetComponent<TextMeshProUGUI>().text = PlayerManager.I.Energy + " / " + "5";
        //Gem_Text.GetComponent<TextMeshProUGUI>().text = PlayerManager.I.GEM.ToString();
        //Gold_Text.GetComponent<TextMeshProUGUI>().text = PlayerManager.I.Gold.ToString();
        //Level_Slider.value = PlayerManager.I.Player_EXP * EXP_ChangeValue;
        //Level_Slider_Text.GetComponent<TextMeshProUGUI>().text = PlayerManager.I.Player_EXP +  " / " + PlayerManager.I.MAX_EXP;

        EXP_ChangeValue = PlayerManager.I.MAX_EXP;
        EXP_ChangeValue = 1 / EXP_ChangeValue;


    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(PlayerManager.I.Level);
        Level_Text.GetComponent<TextMeshProUGUI>().text = PlayerManager.I.Level.ToString();
        Energy_Text.GetComponent<TextMeshProUGUI>().text = PlayerManager.I.Energy + " / 5";
        Gem_Text.GetComponent<TextMeshProUGUI>().text = PlayerManager.I.GEM.ToString();
        Gold_Text.GetComponent<TextMeshProUGUI>().text = PlayerManager.I.Gold.ToString();
        //Level_Slider.GetComponent<Slider>().value = PlayerManager.I.Player_EXP * EXP_ChangeValue;
        
        Level_Slider.GetComponent<Slider>().value = (PlayerManager.I.Player_EXP * EXP_ChangeValue);
        Level_Slider_Text.GetComponent<TextMeshProUGUI>().text = PlayerManager.I.Player_EXP + " / " + PlayerManager.I.MAX_EXP;
    }
}
