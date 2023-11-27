using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;

    private WeaponData GetWeponData(WeaponType weaponType)
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

}
