using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWind : PlayableBehaviour<SpawnWind.Context>
{
    public class Context { };

    public GameObject wind;
    public Vector2 intervalRange;

    public Transform spawnPoint;

    public override bool Playing => Active;

    private bool active;

    private bool Active
    {
        get => active;
        set => active = value;
    }

    public override void Stop()
    {
        Active = false;
    }

    protected override void Execute(Context context)
    {
        Active = true;
        StartCoroutine(SpawnWinds());
    }

    private IEnumerator SpawnWinds()
    {
        while (Active)
        {
            float delay = Random.Range(intervalRange.x, intervalRange.y);
            yield return new WaitForSeconds(delay);

            GameObject instance = Instantiate(wind, spawnPoint.position, transform.rotation);
            Destroy(instance, 15);
        }
    }
}
