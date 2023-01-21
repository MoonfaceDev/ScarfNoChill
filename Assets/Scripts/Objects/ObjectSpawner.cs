using System.Collections;
using UnityEngine;

public class ObjectSpawner : BaseComponent
{
    public float size;
    public float interval;
    public Collectable objectPrefab;

    private void Awake()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Instantiate(objectPrefab, GetSpawnPosition(), Quaternion.identity);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        var center = transform.position.x;
        return new Vector3(Random.Range(center - size / 2, center + size / 2), 0, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        var center = transform.position;
        Gizmos.DrawLine(center + size / 2 * Vector3.left, center + size / 2 * Vector3.right);
    }
}