using Rogalik.Scripts.Buff.Base;
using Rogalik.Scripts.Player;
using UnityEngine;

namespace Rogalik.Scripts.Buff
{
    public class HealBuff : IBuff
    {
        public string BuffName => "HealBuff";
        public string BuffInfo => $"Çהמנמגüו +{_healValue}";
        public string TempBuffInfo => BuffInfo;
        private readonly IBuffable _owner;
        private int _healValue;
        
        public HealBuff(int healValue, IBuffable owner)
        {
            _healValue = healValue;
            _owner = owner;
        }
        
        public PlayerStats ApplyBuff(PlayerStats baseStats)
        {
            PlayerStats newStats = baseStats;
            RoguelikePlayer.Instance.Heal(_healValue);
            
            return newStats;
        }
    }
}