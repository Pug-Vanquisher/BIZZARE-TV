using Rogalik.Scripts.Player;

namespace Rogalik.Scripts.Buff.Base
{
    public interface IBuff
    {
        public string BuffName { get; }
        public string BuffInfo { get; }
        public string TempBuffInfo { get; }
        PlayerStats ApplyBuff(PlayerStats baseStats);
    }
}