using UnityEngine;

namespace Balance
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "BalanceConfigs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
    }
}
