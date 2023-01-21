using UnityEngine;
using UnityEngine.UI;

public class WarmthBar : MonoBehaviour
{
    public Warmth warmth;
    public Scrollbar bar;

    private void Update()
    {
        bar.size = warmth.warmth / Warmth.MaxWarmth;
    }
}