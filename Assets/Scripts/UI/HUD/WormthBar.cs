using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WormthBar : MonoBehaviour
{
    public Wormth wormth;
    public Scrollbar bar;

    private void Update()
    {
        bar.size = wormth.heat / 100;
    }
}
