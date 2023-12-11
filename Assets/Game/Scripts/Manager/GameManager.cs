using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu, GamePlay,Revive, GameOver, Finish
}

public class GameManager : Singleton<GameManager>
{
    private GameState gameState;
    private  PlayerData playerData;
    public WeaponSO weaponSO;
    public SkinSO skinSO;
    public HatSO hatSO;
    public ShieldSO shieldSO;
    public FloatingJoystick floatingJoystick;
    public PlayerData PlayerData { get => playerData; set => playerData = value; }


    private void Awake()
    {
        GetPlayerData();
        ChangeState(GameState.MainMenu);
    }

    public WeaponData GetWeponData(WeaponType weaponType)
    {
        //List<WeaponData> weaponData = weaponSO.weapons;
        //for (int i = 0; i < weaponData.Count; i++)
        //{
        //    if (weaponType == weaponData[i].weaponType)
        //    {
        //        return weaponData[i];
        //    }
        //}
        return weaponSO.weapons[(int)weaponType];
    }

    public SkinData GetPantData(PantType pant)
    {
        List<SkinData> pantData = skinSO.pants;
        for (int i = 0; i < pantData.Count; i++)
        {
            if (pant == pantData[i].pant)
            {
                return pantData[i];
            }
        }
        return null;
    }

    public HatData GetHatData(HatType hat)
    {
        List<HatData> hatData = hatSO.hats;
        for (int i = 0; i < hatData.Count; i++)
        {
            if (hat == hatData[i].hatType)
            {
                return hatData[i];
            }
        }
        return null;
    }

    public ShieldData GetShieldData(ShieldType shield)
    {
        return shieldSO.shields[(int)shield];
    }

    public void GetPlayerData()
    {
        if (DataManager.Instance.HasData<PlayerData>())
        {
            PlayerData = DataManager.Instance.LoadData<PlayerData>();
            Debug.Log("Loaded User Data.");
        }
        else
        {
            PlayerData = new PlayerData();
            DataManager.Instance.SaveData(PlayerData);
            Debug.Log("Creating New User Data");
        }
    }


    public void ChangeState(GameState state)
    {
        gameState = state;
    }

    public bool IsState(GameState state)
    {
        return gameState == state;
    }

}
