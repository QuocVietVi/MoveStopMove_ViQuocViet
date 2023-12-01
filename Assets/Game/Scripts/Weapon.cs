using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public void Despawn()
    {
        LeanPool.Despawn(this);
    }
}
