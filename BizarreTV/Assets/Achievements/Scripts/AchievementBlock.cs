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

        public void Init(AchievementConfig config)
        {
            _iconView.sprite = config.Icon;
            _titleView.text = config.Title;
            _descriptionView.text = config.Description;
        }
    }
}