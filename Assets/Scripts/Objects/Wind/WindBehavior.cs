using UnityEngine;
using UnityEngine.Serialization;

public class WindBehavior : BaseComponent
{
    [HideInInspector] [FormerlySerializedAs("alreayHit")] public bool alreadyHit;
    public float damage;
    public float scarfDamageMultiplier;

    private void Awake()
    {
        alreadyHit = false;
    }
}