using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBehavior : BaseComponent
{
    public bool alreayHit;
    public float damage;
    public float scarfDamageMultiplier;

    private void Awake()
    {
        alreayHit = false;
    }
}
