using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindColliderSpawner : BaseComponent
{
    public GameObject windCollider;
    public WindBehavior wind;

    private void Awake()
    {
        StartCoroutine(SpawnColliders());
    }

    private IEnumerator SpawnColliders()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(0.17f);

            GameObject instance = Instantiate(windCollider, transform.position, transform.rotation);

            //set a reference to the wind object
            instance.GetComponent<WindFlowBehavior>().wind = wind;
            Destroy(instance, 2f);
        }
    }
}
