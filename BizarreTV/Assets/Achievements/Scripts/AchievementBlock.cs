using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Achievements
{
	public class AchievementBlock : MonoBehaviour
    {
        [SerializeField] private Image _iconView;
        [SerializeField] private TMP_Text _titleView;
        [SerializeField] private TMP_Text _descriptionView;
        [SerializeField] private TMP_Text _progressView;

        public void Init(AchievementConfig config, float progress)
        {
            var formattedProgress = progress % 1 == 0 ? progress.ToString("0") : progress.ToString("0.0");

            _iconView.sprite = config.Icon;
            _titleView.text = config.Title;
            _descriptionView.text = config.Description;
            _progressView.text = $"{formattedProgress}/{config.Progress}";
        }
    }
}