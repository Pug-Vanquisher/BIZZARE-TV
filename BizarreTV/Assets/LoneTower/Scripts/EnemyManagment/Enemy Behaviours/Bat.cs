using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Bat : EnemyMove
{
    float t = 0;
    float radius = 30f;
    Vector2 spawnpoint;
    
    void Awake()
    {
        spawnpoint = new Vector2(transform.position.x, transform.position. y);
    }
    public override void EnemyMoveTick (){
        t += Time.fixedDeltaTime / 20;
        //radius -= getSpeed() * t;
        radius -= 0.005f;
        if (radius < 0){
            radius = 0;
        }
        transform.position = Vector2.Lerp(transform.position, new Vector2(Mathf.Cos(1f * t + spawnpoint.x) * radius + target.position.x, Mathf.Sin(1f * t + spawnpoint.y) * radius + target.position.y), Time.deltaTime);
        //transform.position = Vector2.MoveTowards(transform.position, (transform.position - target.position) * 0.01f, getSpeed() * Time.deltaTime);
    }

    public override void TakeDamage(float damage)
    {
        health -= damage;
        
        if (health <= 0)
        {   
            Destroy(gameObject);
            AudioManagerTower.Instance.PlaySFX("Died");
            Tower tower = GameObject.FindGameObjectWithTag("Tower").GetComponent<Tower>();
            tower.money = tower.money + tower.moneyForKill;
            tower.moneyUpdate();

            Random random = new Random();
            int chance = random.Next(1, 100);
            if (chance <= rubyDropChance)
            {
                tower.ruby += 1;
                tower.rubyUpdate();
            }
        }
        else
        {
            AudioManagerTower.Instance.PlaySFX("Damage");
        }
    }
}