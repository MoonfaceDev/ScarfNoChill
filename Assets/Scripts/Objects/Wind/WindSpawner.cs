using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpawner : PlayableBehaviour<WindSpawner.Context>
{
    public class Context { };

    public GameObject wind;
    public Vector2 intervalRange;

    public Transform spawnPoint;

    public override bool Playing => Active;

    private bool Active { get; set; }

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
            var delay = Random.Range(intervalRange.x, intervalRange.y);
            yield return new WaitForSeconds(delay);

            var instance = Instantiate(wind, spawnPoint.position, transform.rotation);
            Destroy(instance, 15);
        }
    }
}
