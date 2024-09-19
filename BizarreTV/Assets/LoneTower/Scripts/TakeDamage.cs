using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public HealthBar bar;
    public int damage;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            bar.MinusHealth(damage);
            Destroy(col.gameObject);
        }
    }
}
