using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    Hammer = 0,
    Candy = 1,
    Arrow = 2,
    Knife = 3,
    Boomerang = 4,
}
[Serializable]
public class WeaponData 
{
    public Bullet bullet;
    public Weapon weapon;
    public WeaponType weaponType;
    public float range;
    public float speed;
    public float resetAttack;
    public float price;
    
}
