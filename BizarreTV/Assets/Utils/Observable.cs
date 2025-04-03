using System;

namespace Utils
{
    public class Observable<T>
    {
        private event Action<T> _onCompleted;

        private T _value;
        private bool _hasValue;

        public static Observable<T> Return(T value)
        {
            return new Observable<T>()
            {
                _value = value,
                _hasValue = true
            };
        }

        public void Subscribe(Action<T> action)
        {
            if (_hasValue)
                action?.Invoke(_value);
            else
                _onCompleted += (T value) => action.Invoke(value);
        }

        public void OnNext(T value)
        {
            _onCompleted?.Invoke(value);
        }
    }
}
