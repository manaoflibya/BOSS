using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private static MonsterManager instance;
    public static MonsterManager I
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MonsterManager>();
            }

            return instance;
        }

    }

    // SLIME ½ºÅÈ Á¤º¸
    public string Slime_name { get; private set; }
    public float Slime_MaxHealth { get; private set; }
    public float Slime_Health { get; private set; }
    public float Slime_Power { get; private set; }


    // Dragon ½ºÅÈ
    public string Dragon_name { get; private set; }
    public float Dragon_MaxHealth { get; private set; }
    public float Dragon_Health { get; private set;}
    public float Dragon_Power { get; private set; }

    // Golem ½ºÅÈ
    public string Golem_name { get; private set;}
    public float Golem_MaxHealth { get; private set;}
    public float Golem_Health { get; private set; }
    public float Golem_Power { get; private set; }
    public bool BossDead { get; set; }
    public bool Golem_IsJumping { get; set; } //Á¡ÇÁ Áß ½ºÅ³ ²ô±â
    public bool Golem_CameraFinish { get; set; } //Ä«¸Þ¶ó »ç¿ë ²ô±â
    public bool Golem_Stage_Start { get; set; }


    //gold box
    public string GoldBox_name { get; private set; }
    public float GoldBox_Health { get; private set; }
    
    // how many slime 
    public int SlimeCount { get; set; }
    public int DragonCount { get; set; }

    private void Awake()
    {
        Golem_name = "golem";
        Golem_Health = Golem_MaxHealth = 1000f;
        Golem_Power = 10f;
        BossDead = false;
        Golem_IsJumping = false;
        Golem_CameraFinish = false;
        Golem_Stage_Start = false;

        Slime_name = "slime";
        Slime_Health = Slime_MaxHealth = 120f;
        Slime_Power = 5f;

        Dragon_name = "dragon";
        Dragon_Health = Dragon_MaxHealth = 200f;
        Dragon_Power = 5f;
        
        GoldBox_name = "goldbox";
        GoldBox_Health = 150;

        SlimeCount = 0;
        DragonCount = 0;
        ////
    }
    public void KillingSlime()
    {
        SlimeCount++;
        //Debug.Log(SlimeCount);
    }
    public void KillingDragon()
    {
        DragonCount++;
        //Debug.Log(DragonCount);
    }
}
