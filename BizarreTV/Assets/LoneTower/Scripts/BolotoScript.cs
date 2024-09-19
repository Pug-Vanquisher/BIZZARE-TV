using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BolotoScript : MonoBehaviour
{
    public bool setActive, setDamage;
    //public GameObject bolotoImage;

    /*void Awake()
    {
        bolotoImage.SetActive(true);
    }*/

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (setActive)
            {
                EnemyMove enemy = col.gameObject.GetComponent<EnemyMove>();
                //enemy.speed -= 0.8f;
                //enemy.speed *= 0f;
                //Debug.Log(enemy.speed);
                enemy.SetSpeed(0.5f);
                if (setDamage) enemy.TakeDamage(1);
            }
            
        }
    }
}
