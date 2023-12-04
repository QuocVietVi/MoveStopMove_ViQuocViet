using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pant
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

[Serializable]
public class SkinData
{
    public Pant pant;
    public float moveSpeed;
    public Sprite image;
    public Material material;
    public bool unlock;
    public bool isEqipped;
}
