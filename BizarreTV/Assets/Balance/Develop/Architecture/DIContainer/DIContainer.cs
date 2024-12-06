using System;
using System.Collections.Generic;
using UnityEngine;

namespace Balance
{
    // ���� ��������� ����� �����, ����� �������������� ������ ��������� �������.
    // �� ����� ��� ����������, ����� ����� ����������� Zenject ��� VContainer.
    // ��� ������� ������� ������������� � ������ ���� ���������.
    public static class DIContainer
    {
        private static Dictionary<Type, System.Object> _services = new();

        public static void Register<T>(T service)
        {
            _services[typeof(T)] = service;
        }

        public static void RegisterCofig<T>(T config) where T : ScriptableObject
        {
            var newConfig = ScriptableObject.Instantiate(config);
            _services[typeof(T)] = newConfig;
        }

        public static T Resolve<T>()
        {
            if (!_services.ContainsKey(typeof(T)))
                throw new Exception($"Service {typeof(T)} not registered");

            return (T)_services[typeof(T)];
        }

        public static void RemoveAll()
        {
            _services.Clear();
        }
    }
}
