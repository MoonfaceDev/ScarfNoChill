using System.Collections.Generic;

public class Inventory : CharacterBehaviour
{
    public string[] collectableObjects;
    public Dictionary<string, int> storage;

    protected override void Awake()
    {
        base.Awake();
        storage = new Dictionary<string, int>();
        foreach (var type in collectableObjects)
        {
            storage[type] = 0;
        }
    }
}