using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private static EffectManager instance;
    public static EffectManager I
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EffectManager>();
            }

            return instance;
        }

    }
    //------------player-------------
    public GameObject AddDamageText_Player { get; private set; }

    public GameObject NormalAttackEffect { get; private set; }
    public GameObject NormalAttackEffect1 { get; private set; }
    public GameObject NormalAttackEffect2 { get; private set; }

    public GameObject HeavyAttackEffect1 { get; private set; }
    public GameObject HeavyAttackEffect2 { get; private set; }
    public GameObject HeavyAttackEffect3 { get; private set; }
    public GameObject HeavyAttackEffect4 { get; private set; }

    public GameObject SkillStar { get; private set; }
    public GameObject SkillCharge { get; private set; }
    public GameObject SkillSpin { get; private set; }

    public GameObject AttackEffect { get; private set; }
    public GameObject MoveEffectPlayer { get; private set; }
    public GameObject MoveEffect_StartMenu { get; private set; }
    
    public GameObject Player_VictoryEmoji { get; private set; }
    //------------Slime--------------
    public GameObject AddDamageText_Slime { get; private set; }
    public GameObject MoveEffect { get; private set; }

    public GameObject HitEffect { get; private set; }
    //------------Dragon-------------
    public GameObject AddDamageText_Dragon { get; private set; }

    public GameObject FireBall { get; private set; }
    public GameObject FireBallExplosion { get; private set; }
    public GameObject FireBreath { get; private set; }

    //-----------golem---------------
    public GameObject AddDamageText_Golem { get; private set; }
    public GameObject Attack_Punch_Hole { get; private set; }
    public GameObject Attack_Punch_Explosion { get; private set; }
    public GameObject GolemAttack1_PunchExplosion_Second { get; private set; }

    public GameObject Attack_Fire_Breath { get; private set; }
    public GameObject Attack_Fire_Potal { get; private set; }
    public GameObject Attack_Fire_FireBall { get; private set; }
    public GameObject Attack_Fire_FireballExplosionm { get; private set; }

    public GameObject Attack_Jump_fog { get; private set; }
    public GameObject Attack_Jump_Explosion { get; private set; }
    public GameObject Attack_Jump_WarningZone { get; private set; }

    public GameObject AddMonster_Portal { get; private set; }
    //----------------object-----------------
    public GameObject Drop_Item_Coin { get; private set; }
    public GameObject Drop_Item_Heal { get; private set; }
    public GameObject Drop_Item_EXP { get; private set; }


    public GameObject GetMoneyExplosion { get; private set; }
    public GameObject GetHealExplosion { get; private set; }
    public GameObject GetEXP_Explosion { get; private set; }

    // ---------------object-----------------
    public GameObject Goldbox { get; private set; }

    public GameObject MySlime { get; private set; }
    public GameObject myDragon { get; private set; }
    private void Start()
    {
        Drop_Item_Coin = Resources.Load("DropCoin") as GameObject;
        Drop_Item_Heal = Resources.Load("DropHeal") as GameObject;
        Drop_Item_EXP = Resources.Load("DropEXP") as GameObject;


    }
    private void Awake()
    {
        AddDamageText_Player = Resources.Load("FloatingText_Player") as GameObject;
        AddDamageText_Slime = Resources.Load("FloatingText_slime") as GameObject;
        AddDamageText_Dragon = Resources.Load("FloatingText_Dragon") as GameObject;
        AddDamageText_Golem = Resources.Load("FloatingText_Golem") as GameObject;

        NormalAttackEffect = Resources.Load("NormalAttack_1_") as GameObject;
        NormalAttackEffect1 = Resources.Load("SwordSlashThinBlue Variant Variant_") as GameObject;
        NormalAttackEffect2 = Resources.Load("SwordWaveBlue Variant") as GameObject;

        HeavyAttackEffect1 = Resources.Load("SwordSlashThickBlue") as GameObject;
        HeavyAttackEffect2 = Resources.Load("NovaBlue") as GameObject;
        HeavyAttackEffect3 = Resources.Load("MysticDeath") as GameObject;
        HeavyAttackEffect4 = Resources.Load("NovaBlueSecond") as GameObject;

        SkillStar = Resources.Load("StarExplosionOrange") as GameObject;
        SkillCharge = Resources.Load("MagicChargeBlue") as GameObject;
        SkillSpin = Resources.Load("SwordWhirlwindBlue Variant Variant") as GameObject;

        Player_VictoryEmoji = Resources.Load("PlayerVictoryEmoji") as GameObject;

        AttackEffect = Resources.Load("SwordHitYellowCriticalLegacy Variant Variant") as GameObject;
        HitEffect = Resources.Load("SwordHitMiniRed") as GameObject;

        MoveEffect = Resources.Load("DustDirtyPoof") as GameObject;
        MoveEffectPlayer = Resources.Load("DustDirtyPoof_P") as GameObject;
        MoveEffect_StartMenu = Resources.Load("StartMenuPlayerMove") as GameObject;

        FireBall = Resources.Load("NovaMissileSmallFire") as GameObject;
        FireBallExplosion = Resources.Load("NovaFireRed") as GameObject;
        FireBreath = Resources.Load("FlamethrowerCartoonyFire") as GameObject;

        Attack_Punch_Hole = Resources.Load("GolemAttack1Hole") as GameObject;
        Attack_Punch_Explosion = Resources.Load("GolemAttack1Explosion") as GameObject;
        GolemAttack1_PunchExplosion_Second = Resources.Load("GolemAttack1_PunchExplosion_Second") as GameObject;
        Attack_Fire_Breath = Resources.Load("GolemAttack2Breath") as GameObject;
        Attack_Fire_Potal = Resources.Load("SimplePortalBlue") as GameObject;
        Attack_Fire_FireBall = Resources.Load("MagicSphereBlue") as GameObject;
        Attack_Fire_FireballExplosionm = Resources.Load("NovaBlueSecond") as GameObject;
        Attack_Jump_fog = Resources.Load("GolemJumpAttackFog") as GameObject;
        Attack_Jump_Explosion = Resources.Load("GolemJumpAttackExplosion") as GameObject;
        Attack_Jump_WarningZone = Resources.Load("GolemJumpAttackWarningZone") as GameObject;
        AddMonster_Portal = Resources.Load("Golem_AddMonsterPortal") as GameObject;

        GetMoneyExplosion = Resources.Load("GetMoneyExplosion") as GameObject;
        GetHealExplosion = Resources.Load("GetHealExplosion") as GameObject;
        GetEXP_Explosion = Resources.Load("GetEXP_Explosion") as GameObject;

        Goldbox = Resources.Load("GoldBox Variant") as GameObject;

        MySlime = Resources.Load("MyDragon") as GameObject;
        myDragon = Resources.Load("MySlime") as GameObject;

    }

}
