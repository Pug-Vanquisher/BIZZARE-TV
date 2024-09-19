using Rogalik.Scripts.Buff.Base;
using Rogalik.Scripts.Player;

namespace Rogalik.Scripts.Buff
{
    public class ImmortalBuff : IBuff
    {
        public string BuffName => "ImmortalBuff";
        public string BuffInfo => "Неуязвимость";
        public string TempBuffInfo => "Неуязвимый";

        public PlayerStats ApplyBuff(PlayerStats baseStats)
        {
            PlayerStats newStats = baseStats;
            baseStats.isRolling = false;
            newStats.isRolling = false;
            newStats.isImmortal = true;
            
            return newStats;
        }
    }
}