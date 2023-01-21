using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : BaseComponent
{
    public float speedX, speedY;
    public float amplitude;
    public float phase;
    public bool randomPhase;
    public bool randomSpeed;

    private float angle;

    private void Awake()
    {
        if (randomPhase)
        {
            phase = Random.Range(-5 , 5);
        }

        if (randomSpeed)
        {
            speedX = Random.Range(-1f, 1f);
        }
    }

    void Update()
    {
        transform.Translate(transform.right * speedX * Time.deltaTime);
        transform.Translate(transform.up * speedY * angle * Time.deltaTime);

        angle = Mathf.Sin(Time.fixedTime * amplitude + phase);
    }
}
