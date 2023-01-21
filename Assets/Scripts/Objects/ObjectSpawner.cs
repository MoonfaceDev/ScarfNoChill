using System.Collections;
using UnityEngine;

public class ObjectSpawner : BaseComponent
{
    private const float IntervalMultiplier = 0.66f;

    public float size;
    public float interval;
    public Collectable objectPrefab;
    public int spawnLimit;

    private int currentLivingObjects;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval * Mathf.Pow(IntervalMultiplier, SpawnersManager.tiers[objectPrefab.objectType]));
            if (currentLivingObjects >= spawnLimit) continue;
            var clone = Instantiate(objectPrefab, GetSpawnPosition(), Quaternion.identity);
            clone.onConsume.AddListener(() => currentLivingObjects--);
            currentLivingObjects++;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        return transform.position + Random.Range(-size / 2, size / 2) * Vector3.right;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        var center = transform.position;
        Gizmos.DrawLine(center + size / 2 * Vector3.left, center + size / 2 * Vector3.right);
    }
}