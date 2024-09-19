using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MolotUron : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] public MolotScript molot;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyMove>().TakeDamage(molot.uron);
        }
            
    }
}
