using System;
using System.Collections.Generic;

namespace Balance
{
    // Ётот контейнер супер сырой, может регистрировать только одиночные сервисы.
    // Ќе стоит его копировать, лучше сразу используйте Zenject или VContainer.
    // Ёто решение создано исключительно в рамках моих интересов.
    public static class DIContainer
    {
        private static Dictionary<Type, Object> _services = new();

        public static void Register(Type type, Object service)
        {
            _services[type] = service;
        }

        public static T Resolve<T>()
        {
            if (!_services.ContainsKey(typeof(T)))
                throw new Exception($"Service {typeof(T)} not registered");

            return (T)_services[typeof(T)];
        }
    }
}
