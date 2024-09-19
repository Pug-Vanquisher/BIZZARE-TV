using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rogalik.Scripts.Player
{
    [Serializable]
    public class PlayerStats
    {
        [Header("Health")]
        public int maxHealth;
        public int limitHealth;
        public int health;
        public bool isImmortal;
        public bool isRolling;

        [Header("Movement")] public float speed;
        public float minSpeed;
        public float maxSpeed;
        public float rollSpeed;

        [Header("Attack")]
        public int damage;
        public float attackSpeed;
        public float attackRange;
        public float bulletSpeed;
        
        public PlayerStats(PlayerStats stats)
        {
            maxHealth = stats.maxHealth;
            limitHealth = stats.limitHealth;
            health = stats.health;
            isImmortal = stats.isImmortal;
            isRolling = stats.isRolling;

            speed = stats.speed;
            minSpeed = stats.minSpeed;
            maxSpeed = stats.maxSpeed;
            rollSpeed = stats.rollSpeed;
            
            damage = stats.damage;
            attackSpeed = stats.attackSpeed;
            attackRange = stats.attackRange;
            bulletSpeed = stats.bulletSpeed;
        }
        
        public PlayerStats(int[] healthArray, PlayerStats stats)
        {
            health = healthArray[0];
            maxHealth = healthArray[1];
            limitHealth = healthArray[2];
            isImmortal = stats.isImmortal;
            isRolling = stats.isRolling;

            speed = stats.speed;
            minSpeed = stats.minSpeed;
            maxSpeed = stats.maxSpeed;
            rollSpeed = stats.rollSpeed;

            damage = stats.damage;
            attackSpeed = stats.attackSpeed;
            attackRange = stats.attackRange;
            bulletSpeed = stats.bulletSpeed;
        }

        public int[] GetHealthArray()
        {
            return new[] { health, maxHealth, limitHealth };
        }
    }
}