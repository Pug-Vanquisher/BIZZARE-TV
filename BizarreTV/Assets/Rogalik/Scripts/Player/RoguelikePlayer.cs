using System;
using System.Collections.Generic;
using Rogalik.Scripts.Buff.Base;
using UnityEngine;

namespace Rogalik.Scripts.Player
{
    public class RoguelikePlayer : MonoBehaviour, IBuffable
    {
        public PlayerStats baseStats;
        public PlayerStats currentStats;
        
        [NonSerialized] public List<IBuff> buffs = new();
        [NonSerialized] public bool isRolling;
        [NonSerialized] public bool isStaying;
        
        public static RoguelikePlayer Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            currentStats = baseStats;
            EventBus.Instance.OnHealthChanged.Invoke();
        }
        
        public void GetDamage(int damage)
        {
            if (currentStats.isImmortal || currentStats.isRolling)
                return;
            
            SoundManager.Instance.PlaySound(5);
            currentStats.health = Mathf.Clamp(currentStats.health - damage, 0, int.MaxValue);
            EventBus.Instance.OnHealthChanged.Invoke();
            
            if (currentStats.health <= 0)
                GameManager.Instance.Lose();
        }
        
        public void Heal(int heal)
        {
            currentStats.health = Mathf.Clamp(currentStats.health + heal, 0, currentStats.maxHealth);
            EventBus.Instance.OnHealthChanged.Invoke();
        }
        
        public void AddBuff(IBuff buff)
        {
            buffs.Add(buff);
            
            ApplyBuffs();
            Debug.Log($"Buff added: {buff.GetType()}");
        }

        public void RemoveBuff(IBuff buff)
        {
            if (!buffs.Contains(buff))
                return;
            
            buffs.Remove(buff);
            ApplyBuffs();
            Debug.Log($"Buff removed: {buff.GetType()}");
        }
        
        private void ApplyBuffs()
        {
            currentStats = new PlayerStats(new[] { currentStats.health, currentStats.maxHealth, currentStats.limitHealth }, baseStats);
            
            foreach (var buff in buffs)
            {
                if (buff.BuffName == "ImmortalBuff")
                {
                    baseStats.isRolling = false;
                    currentStats.isRolling = false;
                }
                
                currentStats = buff.ApplyBuff(currentStats);
            }
        }
    }
}
