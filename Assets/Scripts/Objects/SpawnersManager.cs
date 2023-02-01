using System.Collections.Generic;

public class SpawnersManager : BaseComponent
{
    public static Dictionary<string, int> tiers;
    public CraftingMenu craftingMenu;

    public void Initialize()
    {
        tiers = new Dictionary<string, int>();
        foreach (var collectable in craftingMenu.collectables)
        {
            tiers[collectable.objectType] = 0;
        }
    }

    public static void Upgrade(string objectType)
    {
        tiers[objectType]++;
    }
}