using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Warmth : CharacterBehaviour
{
    public const float MaxWarmth = 100;

    public Color damageColor;

    public float damageRate;
    [HideInInspector] public float healRate;

    public UnityEvent deathEvent;
    public DeathScene death;

    [HideInInspector] public float warmth;


    protected override void Awake()
    {
        base.Awake();
        warmth = MaxWarmth;
    }

    private void Update()
    {
        warmth = Mathf.Max(warmth - damageRate * Time.deltaTime, 0);
        warmth = Mathf.Min(warmth + healRate * Time.deltaTime, MaxWarmth);

        if (warmth <= 0)
            Kill();
    }

    private void Kill()
    {
        warmth = 0;
        death.Play(new DeathScene.Context());
        deathEvent.Invoke();
        Debug.Log("no chill!!\n [character is dead]");
    }

    public void TakeDamage(float damage)
    {
        warmth -= damage;
        StartCoroutine(AnimateDamage());
    }

    private IEnumerator AnimateDamage()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        sprite.color = damageColor;
        yield return new WaitForSeconds(0.5f);
        sprite.color = Color.white;
    }

}