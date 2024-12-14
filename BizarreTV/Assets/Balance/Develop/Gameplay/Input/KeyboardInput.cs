using UnityEngine;

namespace Balance
{
    public class KeyboardInput : IInput
    {
        private const string HORIZONTAL = "Horizontal";
        private const string VERTICAL = "Vertical";

        public Vector3 GetAxis()
        {
            float horizontalAxis = Input.GetAxis(HORIZONTAL);
            float verticalAxis = Input.GetAxis(VERTICAL);

            return new Vector3(horizontalAxis, 0f, verticalAxis);
        }

        public bool IsNotZero()
        {
            return Input.GetAxis(HORIZONTAL) != 0f || Input.GetAxis(VERTICAL) != 0f;
        }
    }
}
