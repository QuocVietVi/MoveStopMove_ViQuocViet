using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PantType
{
    Default = 0,
    Batman = 1,
    Chambi = 2,
    Comy = 3,
    Dabao = 4,
    Onion = 5,
    Pokemon = 6,
    Rainbow = 7,
    Skull = 8,
    Vantim = 9
}

public enum HatType
{
    Default = 0,
    Arrow = 1,
    Cowboy = 2,
    Crown = 3,
    Ear = 4,
    Hat = 5,
    Hat_Cap = 6,
    Hat_Yellow = 7,
    Horn = 8
}

public enum ShieldType
{
    Default = 0,
    Black_Panther = 1,
    Skull = 2,
    Pink_Star = 3,
    Viet_Nam = 4

}

[Serializable]
public class SkinData
{
    public PantType pant;
    public float moveSpeed;
    public Sprite image;
    public Material material;
    public float price;
    //public bool unlock;
    //public bool isEqipped;
}


[Serializable]
public class HatData
{
    public HatType hatType;
    public float range;
    public Sprite image;
    public GameObject hatPrefab;
    public float price;
    //public bool unlock;
    //public bool isEqipped;
}

[Serializable]
public class ShieldData
{
    public ShieldType shieldType;
    public int gold;
    public Sprite image;
    public GameObject shieldPrefab;
    public float price;

}

