using System;

namespace Utils
{
    public class Observable<T>
    {
        private event Action<T> _onCompleted;

        public void Subscribe(Action<T> action)
        {
            _onCompleted += (T value) => action.Invoke(value);
        }

        public void OnNext(T value)
        {
            _onCompleted?.Invoke(value);
        }
    }
}
