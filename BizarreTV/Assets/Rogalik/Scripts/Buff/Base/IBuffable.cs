namespace Rogalik.Scripts.Buff.Base
{
    public interface IBuffable
    {
        void AddBuff(IBuff buff);
        void RemoveBuff(IBuff buff);
    }
}