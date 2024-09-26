using UnityEngine;

[CreateAssetMenu(fileName = "TestConfig", menuName = "BalanceConfigs/TestConfig")]
public class TestConfig : ScriptableObject
{
    [field: SerializeField] public string Message { get; private set; }
}
