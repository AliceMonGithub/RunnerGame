using UnityEngine;
using TMPro;
public class HighScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private Levels _levels;

    private void Start()
    {
        RefreshHighScore();
    }

    public void RefreshHighScore()
    {
        _highScoreText.text = PlayerPrefs.GetInt("HighScore" + _levels.GameLevels[_levels.ÑurrentLevelIndex].name).ToString();
    }
}
