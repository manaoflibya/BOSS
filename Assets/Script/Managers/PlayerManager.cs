using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager I
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerManager>();
            }

            return instance;
        }

    }
    //플레이어 상태
    public bool IsAttack { get; set; }
    public bool IsStop { get; set; }
    public bool IsHit { get; set; }
    public bool IsDead { get; set; }
    public bool IsRolling { get; set; }
    public bool StopRolling { get; set; }
    public bool IsCameraChange { get; set; }
    public bool IsSpinAttack { get; set; }
    //플레이어 스텟
    public float Health { get; set; }
    //public float CurHealth { get; set; }
    public int Level { get; set; }
    public float NormalPower { get; set; }
    public float HeavyAttackPower { get; set; }
    

    //플레이어 아이템
    public int Gold { get; set; }
    public int Potion { get; private set; }
    public int Player_EXP { get; set; }
    public int GEM { get; set; }
    public int Energy { get; set; }
    public int MAX_EXP { get; set; }

    float StartHealth;
    private void Awake()
    {
        JsonManager.I.LoadPlayerDataToJson();
        StartHealth = Health = JsonManager.I.playerData.P_MaxHealth;
        Level = JsonManager.I.playerData.P_Level;
        NormalPower = JsonManager.I.playerData.P_NormalAttackPower;
        HeavyAttackPower = JsonManager.I.playerData.P_HeavyAttackPower;
        IsRolling = false;
        StopRolling = false;
        IsDead = false;
        IsCameraChange = false;
        IsSpinAttack = false;

        Gold = JsonManager.I.playerData.P_Money;
        Potion = 0;
        Player_EXP = JsonManager.I.playerData.P_EXP;
        GEM = JsonManager.I.playerData.P_Gem;
        Energy = JsonManager.I.playerData.P_Stamina;
        MAX_EXP = 300;

       // Debug.Log(StartHealth);
       // Debug.Log(Level);
       // Debug.Log(NormalPower);
       // Debug.Log(HeavyAttackPower);
    }
    
    public void PlayerOnDamage(float damage)
    {
        if(!IsRolling &&!IsDead && !IsCameraChange)
        {
            GameObject camera = GameObject.Find("PlayerCamera");
            StartCoroutine(camera.GetComponent<FollowPlayer_Camera>().Shake(0.12f, 0.12f));

            int changeInt = (int)damage;
            GameObject go = Instantiate(EffectManager.I.AddDamageText_Player, this.transform.position, Quaternion.identity, transform);
            go.GetComponent<TextMesh>().text = changeInt.ToString();


            Health -= damage;
            //Debug.Log(CurHealth);
            IsHit = true;
            if (Health <= Mathf.Epsilon)
                IsDead = true;

        }
        else if(IsRolling)
        {
            GameObject go = Instantiate(EffectManager.I.AddDamageText_Player, this.transform.position, Quaternion.identity, transform);
            go.GetComponent<TextMesh>().text = "회피!";


        }
    }
    public void PlusCoin()
    {
        Gold++;
    }
    public void PlusPotion()
    {
        Potion++;
    }
    public void PlusEXP(int exp)
    {
        Player_EXP += exp;
        if(Player_EXP >= MAX_EXP)
        {
            PlusLevel();

        }
    }
    public void PlusHeal(float healAmount)
    {
        if(Health <= StartHealth - 30)
        {
            Health += healAmount;
            Debug.Log(Health);
            Debug.Log(StartHealth);
        }
        else
        {

            Debug.Log(Health);
            Debug.Log(StartHealth);
            Debug.Log("Health is full!");
        }
    }
    public void PlusLevel()
    {
        Level += 1;
        Player_EXP = 0;
    }
}
