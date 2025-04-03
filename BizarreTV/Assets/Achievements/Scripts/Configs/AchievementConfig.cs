using UnityEngine;

namespace Achievements
{
    [CreateAssetMenu(fileName = "AchievementConfig", menuName = "Achievements/New Achievement Config")]
    public class AchievementConfig : ScriptableObject
    {
        [field: SerializeField] public int Id { get; private set; } = -1;
        [field: SerializeField] public float Progress { get; private set; } = 1f;
        [field: SerializeField] public int Reward { get; private set; } = 1;
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField, TextArea] public string Description { get; private set; }

        private void OnValidate()
        {
            if (Id == -1)
                throw new System.ArgumentException($"" +
                    $"Id {Id} is not definited.");
        }
    }
}
