using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public float speed;

    void Update()
    {
        rigidbody.velocity = new Vector2(speed, 0);
    }
}
