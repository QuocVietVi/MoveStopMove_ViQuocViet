using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData 
{
    public int golds;
    public int weaponEquppied;
    public List<int> listWeaponUnlock;

    public PlayerData()
    {
        golds = 0;
        weaponEquppied = 0;
        listWeaponUnlock = new List<int>(0);
    }

    public PlayerData(int golds, int weaponEquppied, List<int> listWeaponUnlock)
    {
        this.golds = golds;
        this.weaponEquppied = weaponEquppied;
        this.listWeaponUnlock = listWeaponUnlock;
    }
}
