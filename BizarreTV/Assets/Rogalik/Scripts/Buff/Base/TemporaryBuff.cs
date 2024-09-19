using System.Timers;
using Rogalik.Scripts.Player;

namespace Rogalik.Scripts.Buff.Base
{
    public class TemporaryBuff : IBuff
    {
        public string BuffInfo => _buff.BuffInfo + " на " + _time + " секунды";
        public string TempBuffInfo => _buff.TempBuffInfo;

        private readonly IBuffable _owner;
        private readonly IBuff _buff;
        private readonly float _time;
        public string BuffName { get; }



        public TemporaryBuff(IBuffable owner, IBuff buff, float time)
        {
            _owner = owner;
            _buff = buff;
            _time = time;
            BuffName = buff.BuffName;
        }

        public PlayerStats ApplyBuff(PlayerStats baseStats)
        {
            
            PlayerStats newStats = _buff.ApplyBuff(baseStats);

            Timer timer = new Timer(_time * 1000);
            timer.Elapsed += (_, _) => _owner.RemoveBuff(this);
            timer.Start();

            return newStats;
        }
    }
}