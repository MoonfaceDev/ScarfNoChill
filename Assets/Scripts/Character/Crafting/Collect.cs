using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class Collect : CharacterBehaviour
{
    public float maximumDistance;

    private Inventory inventory;
    private Score score;

    protected override void Awake()
    {
        base.Awake();
        inventory = GetComponent<Inventory>();
        score = GetComponent<Score>();
    }

    private void Update()
    {
        var collectables = FindObjectsOfType<Collectable>();

        foreach (var collectable in collectables)
        {
            if (ShouldCollect(collectable))
            {
                CollectCollectable(collectable);
            }
        }
    }

    private void CollectCollectable(Collectable collectable)
    {
        inventory.storage[collectable.objectType]++;
        collectable.Consume();
        score.score += collectable.score;
    }

    private bool ShouldCollect(Collectable collectable)
    {
        return Vector2.Distance(transform.position, collectable.transform.position) < maximumDistance;
    }
}