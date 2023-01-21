using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWind : BaseComponent
{
    public GameObject wind;
    public Vector2 intervalRange;

    public Transform spawnPoint;

    private void Awake()
    {
        StartCoroutine(SpawnWinds());
    }

    private IEnumerator SpawnWinds()
    {
        while (enabled)
        {
            float delay = Random.Range(intervalRange.x, intervalRange.y);
            yield return new WaitForSeconds(delay);

            GameObject instance = Instantiate(wind, spawnPoint.position, transform.rotation);
            Destroy(instance, 15);
        }
    }
}
