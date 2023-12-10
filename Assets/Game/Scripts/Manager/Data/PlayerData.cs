using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData 
{
    public float golds;
    public int weaponEquipped;
    public int pantEqipped;
    public int hatEqipped;
    public int shieldEqipped;
    public List<int> listWeaponUnlock;
    public List<int> listPantUnlock;
    public List<int> listHatUnlock;
    public List<int> listShieldUnlock;

    public PlayerData()
    {
        golds = 0;
        weaponEquipped = pantEqipped = hatEqipped = shieldEqipped = 0;
        listWeaponUnlock = new List<int>(0);
        listPantUnlock = new List<int>(0);
        listHatUnlock = new List<int>(0);
        listShieldUnlock = new List<int>(0);
    }

    public PlayerData(float golds, int weaponEquppied,int pantEqipped, int hatEqipped, int shieldEqipped, 
        List<int> listWeaponUnlock, List<int> listPantUnlock, List<int> listHatUnlock, List<int> listShieldUnlock)
    {
        this.golds = golds;
        this.weaponEquipped = weaponEquppied;
        this.pantEqipped = pantEqipped;
        this.hatEqipped = hatEqipped;
        this.shieldEqipped= shieldEqipped;
        this.listWeaponUnlock = listWeaponUnlock;
        this.listPantUnlock = listPantUnlock;
        this.listHatUnlock = listHatUnlock;
        this.listShieldUnlock = listShieldUnlock;
    }
}
