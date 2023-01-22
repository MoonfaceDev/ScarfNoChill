using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeath : MonoBehaviour
{
    public Transform bar;
    public float timeToLiveSeconds;

    private float currentTime;
    private float maxSize;

    private void Awake()
    {
        currentTime = timeToLiveSeconds;
        maxSize = bar.localScale.x;
    }

    private void FixedUpdate()
    {
        currentTime -= Time.deltaTime;
        bar.localScale = new Vector3((currentTime / timeToLiveSeconds) * maxSize, bar.localScale.y, bar.localScale.z);

        if (currentTime <= 0)
            Destroy(gameObject);
    }
}
