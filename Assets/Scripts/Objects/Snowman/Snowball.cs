using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public Warmth player;

    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Scarf scarf = FindObjectOfType<Scarf>();

        if (collision.gameObject.tag == "snowball")
            return;

        if (collision.gameObject.tag == "Player")
        {
            if (scarf.Active)
                player.TakeDamage(damage * 0.25f);
            else
                player.TakeDamage(damage);

            Debug.Log("snowball hit \n[from snowball prefab]");
        }
        
        Destroy(gameObject);
    }
}
