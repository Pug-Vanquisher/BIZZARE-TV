using UnityEngine;

namespace Balance
{
    public interface IInput
    {
        public Vector3 GetAxis();
        public bool IsNotZero();
    }
}
