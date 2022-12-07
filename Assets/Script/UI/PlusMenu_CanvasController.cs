using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlusMenu_CanvasController : MonoBehaviour
{
    enum CLOSENAME
    {
        SHOP,SETTING,HEROES,INVENTORY,UPGRADE, NOMOENY,BOSSROOM,BOSSROOM_IN,NOENERGY
    }
    GameObject SettingPanel;
    GameObject HomeScene_Popup_ADRemove_;
    GameObject Panel_Shop_;
    GameObject ShopGem_Text;
    GameObject ShopGold_Text;

    GameObject Heroes_Popup_;
    GameObject Panel_Inventory_;
    GameObject Panel_Upgrade_;
    GameObject HomeScene_Popup_Message_NoMoeny_;
    GameObject Panel_BossRoom;
    GameObject Panel_BossRoomIn;
    GameObject HomeScene_Popup_Message_NoEnergy_;

    bool IsOpenUpgrade = false;
    public AudioClip touch_S;

    private void Awake()
    {
        SettingPanel = GameObject.Find("Panel_Settings_");
        HomeScene_Popup_ADRemove_ = GameObject.Find("HomeScene_Popup_ADRemove_");
        Panel_Shop_ = GameObject.Find("Panel_Shop_");
        Heroes_Popup_ = GameObject.Find("Heroes_Popup_");
        Panel_Inventory_ = GameObject.Find("Panel_Inventory_");
        Panel_Upgrade_ = GameObject.Find("Panel_Upgrade_");
        HomeScene_Popup_Message_NoMoeny_ = GameObject.Find("HomeScene_Popup_Message_NoMoeny_");
        Panel_BossRoom = GameObject.Find("Panel_BossRoom");
        Panel_BossRoomIn = GameObject.Find("Panel_BossRoomIn");
        HomeScene_Popup_Message_NoEnergy_ = GameObject.Find("HomeScene_Popup_Message_NoEnergy_");

        SettingPanel.SetActive(false);
        HomeScene_Popup_ADRemove_.SetActive(false);
        Panel_Shop_.SetActive(false);
        Heroes_Popup_.SetActive(false);
        Panel_Inventory_.SetActive(false);
        Panel_Upgrade_.SetActive(false);
        HomeScene_Popup_Message_NoMoeny_.SetActive(false);
        Panel_BossRoom.SetActive(false);
        Panel_BossRoomIn.SetActive(false);
        HomeScene_Popup_Message_NoEnergy_.SetActive(false);

    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SoundManager.I.PlayEffectSound(touch_S);
        }
        if(IsOpenUpgrade)
        {
            GameObject.Find("Now_UpgradeHealth_Text").GetComponent<TextMeshProUGUI>().text = (int)PlayerManager.I.Health + "";
            GameObject.Find("Now_UpgradeAttack_Text").GetComponent<TextMeshProUGUI>().text = (int)PlayerManager.I.NormalPower + "";
            GameObject.Find("Now_UpgradeHeavyAttack_Text").GetComponent<TextMeshProUGUI>().text = (int)PlayerManager.I.HeavyAttackPower + "";

            GameObject.Find("Buy_UpgradeHealth_Text").GetComponent<TextMeshProUGUI>().text = ((int)PlayerManager.I.Health + 10).ToString();
            GameObject.Find("Buy_UpgradeAttack_Text").GetComponent<TextMeshProUGUI>().text = ((int)PlayerManager.I.NormalPower + 5).ToString();
            GameObject.Find("Buy_UpgradeHeavyAttack_Text").GetComponent<TextMeshProUGUI>().text = ((int)PlayerManager.I.HeavyAttackPower + 1).ToString();
            GameObject.Find("NowMoney_Text").GetComponent<TextMeshProUGUI>().text = PlayerManager.I.Gold.ToString();

        }
    }
    public void OpenBossRoomIn()
    {
        Panel_BossRoomIn.SetActive(true);
        Panel_BossRoomIn.GetComponent<Animator>().SetTrigger("BossRoomIn_Start");

    }
    public void ClickStageStart()
    {
        if(PlayerManager.I.Energy<0)
        {
            HomeScene_Popup_Message_NoEnergy_.SetActive(true);
            StartCoroutine(CloseTime(CLOSENAME.NOENERGY));
        }
        else
        {
            PlayerManager.I.Energy -= 1;
            JsonManager.I.SavePlayerDataToJson();
            LoadingCanvas_Controller.LoadSene("BossStage");

        }

    }
    public void CloseBossRoomIn()
    {
        Panel_BossRoomIn.GetComponent<Animator>().SetTrigger("BossRoomIn_End");

        StartCoroutine(CloseTime(CLOSENAME.BOSSROOM_IN));
    }


    public void OpenBossRoom()
    {
        Panel_BossRoom.SetActive(true);
        Panel_BossRoom.GetComponent<Animator>().SetTrigger("BossRoom_Start");
        GameObject.Find("BossRoom_Energy_Text").GetComponent<TextMeshProUGUI>().text = PlayerManager.I.Energy + " / " + "5";

    }
    public void CloseBossRoom()
    {
        Panel_BossRoom.GetComponent<Animator>().SetTrigger("BossRoom_End");

        StartCoroutine(CloseTime(CLOSENAME.BOSSROOM));

    }

    public void OpenUpgrade()
    {
        Panel_Upgrade_.SetActive(true);
        GameObject.Find("Upgrade_UI").GetComponent<Animator>().SetTrigger("Upgrade_Start");
        IsOpenUpgrade = true;
        //GameObject.Find("Now_UpgradeHealth_Text").GetComponent<TextMeshProUGUI>().text = (int)JsonManager.I.playerData.P_MaxHealth + "";
        //GameObject.Find("Now_UpgradeAttack_Text").GetComponent<TextMeshProUGUI>().text = (int)JsonManager.I.playerData.P_NormalAttackPower + "";
        //GameObject.Find("Now_UpgradeHeavyAttack_Text").GetComponent<TextMeshProUGUI>().text = (int)JsonManager.I.playerData.P_HeavyAttackPower + "";
       
        //GameObject.Find("Buy_UpgradeHealth_Text").GetComponent<TextMeshProUGUI>().text = ((int)JsonManager.I.playerData.P_MaxHealth + 10).ToString();
        //GameObject.Find("Buy_UpgradeAttack_Text").GetComponent<TextMeshProUGUI>().text = ((int)JsonManager.I.playerData.P_NormalAttackPower +5).ToString();
        //GameObject.Find("Buy_UpgradeHeavyAttack_Text").GetComponent<TextMeshProUGUI>().text = ((int)JsonManager.I.playerData.P_HeavyAttackPower + 1).ToString();
        //GameObject.Find("NowMoney_Text").GetComponent<TextMeshProUGUI>().text = PlayerManager.I.Gold.ToString();
        
    }
    public void BuyHealthButtonClick()
    {
        if (PlayerManager.I.Gold < 1000)
        {

            HomeScene_Popup_Message_NoMoeny_.SetActive(true);
            StartCoroutine(CloseTime(CLOSENAME.NOMOENY));
        }
        else
        {
            PlayerManager.I.Gold -= 1000;
            PlayerManager.I.Health += 10f;
            //GameObject.Find("Buy_UpgradeHealth_Text").GetComponent<TextMeshProUGUI>().text = PlayerManager.I.Health.ToString();
            //GameObject.Find("NowMoney_Text").GetComponent<TextMeshProUGUI>().text = PlayerManager.I.Gold.ToString();

        }
    }
    public void BuyAttackButtonClick()
    {
        if (PlayerManager.I.Gold < 1000)
        {

            HomeScene_Popup_Message_NoMoeny_.SetActive(true);
            StartCoroutine(CloseTime(CLOSENAME.NOMOENY));
        }
        else
        {
            PlayerManager.I.Gold -= 1000;
            PlayerManager.I.NormalPower += 5f;
            //GameObject.Find("Buy_UpgradeAttack_Text").GetComponent<TextMeshProUGUI>().text = PlayerManager.I.NormalPower.ToString();
            //GameObject.Find("NowMoney_Text").GetComponent<TextMeshProUGUI>().text = PlayerManager.I.Gold.ToString();

        }
    }
    public void BuyHeavyAttackButtonClick()
    {
        if (PlayerManager.I.Gold < 1000)
        {
            HomeScene_Popup_Message_NoMoeny_.SetActive(true);
            StartCoroutine(CloseTime(CLOSENAME.NOMOENY));
        }
        else
        {
            PlayerManager.I.Gold -= 1000;
            PlayerManager.I.HeavyAttackPower += 1f;
            //GameObject.Find("Buy_UpgradeHeavyAttack_Text").GetComponent<TextMeshProUGUI>().text = PlayerManager.I.HeavyAttackPower.ToString();
            //GameObject.Find("NowMoney_Text").GetComponent<TextMeshProUGUI>().text = PlayerManager.I.Gold.ToString();

        }
    }
    public void CloseUpgrade()
    {
        IsOpenUpgrade = false;
        JsonManager.I.SavePlayerDataToJson();
        GameObject.Find("Upgrade_UI").GetComponent<Animator>().SetTrigger("Upgrade_End");

        StartCoroutine(CloseTime(CLOSENAME.UPGRADE));

    }

    public void OpenInventory()
    {
        Panel_Inventory_.SetActive(true);
        Panel_Inventory_.GetComponent<Animator>().SetTrigger("Inventory_Start");

    }
    public void CloseInventory()
    {
        Panel_Inventory_.GetComponent<Animator>().SetTrigger("Inventory_End");

        StartCoroutine(CloseTime(CLOSENAME.INVENTORY));

    }
    public void OpenHEROES()
    {
        Heroes_Popup_.SetActive(true);
       // GameObject.Find("HerosePanel").SetActive(true);
        GameObject.Find("Herose_UI").GetComponent<Animator>().SetTrigger("Hero_Start");
        GameObject.Find("Hero_Player_Level_Text").GetComponent<TextMeshProUGUI>().text = PlayerManager.I.Level.ToString();
        GameObject.Find("Hero_Player_Level_Slider").GetComponent<Slider>().value = PlayerManager.I.Player_EXP * 0.01f;
        GameObject.Find("Hero_Player_EXP_Text").GetComponent<TextMeshProUGUI>().text = PlayerManager.I.Player_EXP + "% ";

    }
    public void CloseHEROES()
    {
        GameObject.Find("Herose_UI").GetComponent<Animator>().SetTrigger("Hero_End");

        StartCoroutine(CloseTime(CLOSENAME.HEROES));
    }

    public void OpenShop()
    {
        Panel_Shop_.SetActive(true);
        ShopGem_Text = GameObject.Find("ShopGem_Text");
        ShopGold_Text = GameObject.Find("ShopGold_Text");

        ShopGem_Text.GetComponent<TextMeshProUGUI>().text = PlayerManager.I.GEM.ToString();
        ShopGold_Text.GetComponent<TextMeshProUGUI>().text = PlayerManager.I.Gold.ToString();
        Panel_Shop_.GetComponent<Animator>().SetTrigger("Shop_Start");
        

    }
    public void CloseShop()
    {
        Panel_Shop_.GetComponent<Animator>().SetTrigger("Shop_End");
        StartCoroutine(CloseTime(CLOSENAME.SHOP));

    }

    public void OpenAd()
    {
        HomeScene_Popup_ADRemove_.SetActive(true);

    }
    public void CloseAd()
    {
        HomeScene_Popup_ADRemove_.SetActive(false);
    }

    public void OpenSetting()
    {
        SettingPanel.SetActive(true);

        SettingPanel.GetComponent<Animator>().SetTrigger("Setting_Start");

    }
    public void ClosedSetting()
    {
        SettingPanel.GetComponent<Animator>().SetTrigger("Setting_End");

        StartCoroutine(CloseTime(CLOSENAME.SETTING));
    }

    IEnumerator CloseTime(CLOSENAME name)
    {
        yield return new WaitForSeconds(0.3f);

        switch(name)
        {
            case CLOSENAME.SETTING:
                SettingPanel.SetActive(false);

                break;
            case CLOSENAME.SHOP:
                Panel_Shop_.SetActive(false);

                break;
            case CLOSENAME.HEROES:
                Heroes_Popup_.SetActive(false);

                break;
            case CLOSENAME.INVENTORY:
                Panel_Inventory_.SetActive(false);

                break;
            case CLOSENAME.UPGRADE:
                Panel_Upgrade_.SetActive(false);

                break;
            case CLOSENAME.BOSSROOM:
                Panel_BossRoom.SetActive(false);

                break; 
            case CLOSENAME.BOSSROOM_IN:
                Panel_BossRoomIn.SetActive(false);

                break; 
            case CLOSENAME.NOENERGY:
                yield return new WaitForSeconds(0.2f);
                HomeScene_Popup_Message_NoEnergy_.SetActive(false);

                break;  
            case CLOSENAME.NOMOENY:
                yield return new WaitForSeconds(0.2f);
                HomeScene_Popup_Message_NoMoeny_.SetActive(false);

                break;
        }

    }
}
