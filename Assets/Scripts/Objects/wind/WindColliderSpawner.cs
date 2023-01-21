using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindColliderSpawner : BaseComponent
{
    public GameObject windCollider;

    private void Awake()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(0.17f);
            GameObject instance = Instantiate(windCollider, transform.position, transform.rotation);
            Destroy(instance, 1f);
        }
    }
}
