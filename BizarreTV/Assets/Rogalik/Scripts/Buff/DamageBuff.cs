using Rogalik.Scripts.Buff.Base;
using Rogalik.Scripts.Player;
using UnityEngine;

namespace Rogalik.Scripts.Buff
{
    public class DamageBuff : IBuff
    {
        private readonly int _damageBonus;
        public string BuffName => "DamageBuff";
        public string BuffInfo => $"Урон+{_damageBonus}";
        public string TempBuffInfo => BuffInfo;

        public DamageBuff(int damageBonus)
        {
            _damageBonus = damageBonus;
        }
        
        public PlayerStats ApplyBuff(PlayerStats baseStats)
        {
            PlayerStats newStats = baseStats;
            newStats.damage = Mathf.Clamp(newStats.damage + _damageBonus, 0, int.MaxValue);
            
            return newStats;
        }
    }
}