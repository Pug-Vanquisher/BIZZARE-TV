using Rogalik.Scripts.Buff.Base;
using Rogalik.Scripts.Player;
using UnityEngine;

namespace Rogalik.Scripts.Buff
{
    public class SpeedBuff : IBuff
    {
        public string BuffName => "SpeedBuff";
        public string BuffInfo => $"Скорость +{_speedBonus}";
        public string TempBuffInfo => BuffInfo;
        private readonly float _speedBonus;
        
        public SpeedBuff(int speedBonus)
        {
            _speedBonus = speedBonus;
        }
        
        public PlayerStats ApplyBuff(PlayerStats baseStats)
        {
            PlayerStats newStats = baseStats;
            
            newStats.speed = Mathf.Clamp(newStats.speed + _speedBonus, newStats.minSpeed, newStats.maxSpeed);
            
            return newStats;
        }
    }
}