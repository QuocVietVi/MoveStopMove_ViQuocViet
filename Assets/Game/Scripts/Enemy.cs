using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}
