using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu, ChooseWeapon, GamePlay, Finish
}

public class GameManager : Singleton<GameManager>
{
    public WeaponSO weaponSO;
    private GameState gameState;
    public FloatingJoystick floatingJoystick;

    private void Awake()
    {
        ChangeState(GameState.MainMenu);
    }

    public WeaponData GetWeponData(WeaponType weaponType)
    {
        List<WeaponData> weaponData = weaponSO.weapons;
        for (int i = 0; i < weaponData.Count; i++)
        {
            if (weaponType == weaponData[i].weaponType)
            {
                return weaponData[i];
            }
        }
        return null;
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
