using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class JsonManager : MonoBehaviour
{
    private static JsonManager _Instance;

    public static JsonManager I
    {
        get
        {
            if(_Instance == null)
            {
                _Instance = FindObjectOfType<JsonManager>();
            }
            return _Instance;
        }
    }
    public PlayerData playerData;

    int IsSave;
    void Awake()
    {
        //SavePlayerDataToJson();
        //LoadPlayerDataToJson();
        //PlayerPrefs.SetInt("Save_3", 1);
        //PlayerPrefs.Save();
    }

    [ContextMenu("Save Json Data")]
    public void SavePlayerDataToJson()
    {
       
        playerData.P_Level = PlayerManager.I.Level;
        playerData.P_Money = PlayerManager.I.Gold;
        playerData.P_Gem = PlayerManager.I.GEM;
        playerData.P_Stamina = PlayerManager.I.Energy;
        playerData.P_EXP = PlayerManager.I.Player_EXP;
        playerData.P_MaxHealth = PlayerManager.I.Health;
        playerData.P_NormalAttackPower = PlayerManager.I.NormalPower;
        playerData.P_HeavyAttackPower = PlayerManager.I.HeavyAttackPower;


        string jsonData = JsonUtility.ToJson(playerData,true);
        string path = Application.persistentDataPath;
        Debug.Log(path);
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", jsonData);
    }
       


    public void SavePlayerDataToJson_BossClear()
    {
        playerData.P_Level = PlayerManager.I.Level;
        playerData.P_Money = PlayerManager.I.Gold;
        playerData.P_Gem = PlayerManager.I.GEM;
        playerData.P_Stamina = PlayerManager.I.Energy;
        playerData.P_EXP = PlayerManager.I.Player_EXP;
        playerData.P_NormalAttackPower = PlayerManager.I.NormalPower;
        playerData.P_HeavyAttackPower = PlayerManager.I.HeavyAttackPower;

        string jsonData = JsonUtility.ToJson(playerData, true);
        string path = Application.persistentDataPath;
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", jsonData);

        LoadPlayerDataToJson();
    }


    [ContextMenu("ReSet Json Data")]
    public void ReSetPlayerDataToJson()
    {
        playerData.P_Level = 1;
        playerData.P_Money = 1000;

        playerData.P_Gem = 0;
        playerData.P_Stamina = 5;
        playerData.P_EXP = 1;

        playerData.P_MaxHealth = 200f;

        playerData.P_NormalAttackPower = 10f;
        playerData.P_HeavyAttackPower = 3f;


        string jsonData = JsonUtility.ToJson(playerData, true);
        string path = Application.persistentDataPath;
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", jsonData);

    }

    [ContextMenu("Load Json Data")]
    public void LoadPlayerDataToJson()
    {
       // GameObject Check = GameObject.Find("Check");
        //Debug.Log("IsSave = " + IsSave);
        // Debug.Log(PlayerPrefs.HasKey("Save_int"));
        
        IsSave = PlayerPrefs.GetInt("Save_3");
        Debug.Log(IsSave);
        if(IsSave == 3)
        {
            TextAsset temp = Resources.Load("playerData") as TextAsset;
            playerData = JsonUtility.FromJson<PlayerData>(temp.text);

            PlayerManager.I.Level = playerData.P_Level;
            PlayerManager.I.Gold = playerData.P_Money;
            PlayerManager.I.GEM = playerData.P_Gem;
            PlayerManager.I.Energy = playerData.P_Stamina;
            PlayerManager.I.Player_EXP = playerData.P_EXP;
            PlayerManager.I.Health = playerData.P_MaxHealth;
            PlayerManager.I.NormalPower = playerData.P_NormalAttackPower;
            PlayerManager.I.HeavyAttackPower = playerData.P_HeavyAttackPower;
            PlayerPrefs.SetInt("Save_3", 4);
            PlayerPrefs.Save();
            Debug.Log("load to Resources");

        }

        else if (IsSave == 4)
        {
            string path_e = Application.persistentDataPath + "/playerData.json";
            string jsonData_e = File.ReadAllText(path_e);
            playerData = JsonUtility.FromJson<PlayerData>(jsonData_e);

            //Debug.Log(path_e);

            PlayerManager.I.Level = playerData.P_Level;
            PlayerManager.I.Gold = playerData.P_Money;
            PlayerManager.I.GEM = playerData.P_Gem;
            PlayerManager.I.Energy = playerData.P_Stamina;
            PlayerManager.I.Player_EXP = playerData.P_EXP;
            PlayerManager.I.Health = playerData.P_MaxHealth;
            PlayerManager.I.NormalPower = playerData.P_NormalAttackPower;
            PlayerManager.I.HeavyAttackPower = playerData.P_HeavyAttackPower;
            Debug.Log("Load to_json");
        }
        else
        {
            PlayerPrefs.SetInt("Save_3",3);
            PlayerPrefs.Save();
        }
        //if (!Check.GetComponent<CheckLoad>().isLoad && !IsSave) //게임을 처음 시작할 시 초기화 된 데이터를 가져온다.
        //else if(IsSave == 2)//게임을 처음 시작할 시 초기화 된 데이터를 가져온다.
        //{
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("Save_3", 1);
        //    PlayerPrefs.Save();

        //}
        ////  else if(Check.GetComponent<CheckLoad>().isLoad)//초기화를 한 데이터를 가져온 후부터는 새로 데이터가 써진 json파일에 계속 저장한다.
        //  else if(IsSave) //초기화를 한 데이터를 가져온 후부터는 새로 데이터가 써진 json파일에 계속 저장한다.
        //  {
        //  }
    }
}


[System.Serializable]
public class PlayerData
{
    public int P_Level;
    public int P_Money;
    public int P_Gem;
    public int P_Stamina;
    public int P_EXP; 
    public float P_MaxHealth;
    public float P_NormalAttackPower;
    public float P_HeavyAttackPower;

}

[System.Serializable]
public class MonsterData
{
    public string monsterName;

}
[System.Serializable]
public class SoundData
{
    public int Volume;

}