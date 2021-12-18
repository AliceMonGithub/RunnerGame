using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class DeadMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _distanceText;
    [SerializeField] private TMP_Text _blocksText;
    [SerializeField] private TMP_Text _diamountText;
    [SerializeField] private TMP_Text _skullText;
    [SerializeField] private Levels _levels;

    public GameObject DeadMenuObject;

    public void ShowInformation(Player player)
    {
        ShowDistance(player.TileGenerator.GeneratedTilesCountOnUI);
        ShowBlocksCount(player.PlayerStatsBase.PlayerStatisticInteractor.Blocks);
        ShowDiamountCount(player.PlayerStatsBase.PlayerStatisticInteractor.Coins);
        ShowSkullCount(player.PlayerStatsBase.PlayerStatisticInteractor.Skull);

        SaveHighScore(player);
    }

    private void SaveHighScore(Player player)
    {
        if(PlayerPrefs.GetInt("HighScore" + _levels.GameLevels[_levels.ÑurrentLevelIndex].name) < player.TileGenerator.GeneratedTilesCountOnUI)
        {
            PlayerPrefs.SetInt("HighScore" + _levels.GameLevels[_levels.ÑurrentLevelIndex].name, player.TileGenerator.GeneratedTilesCountOnUI);
        }
    }

    public void RestartGame() =>
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void GoToMenu() =>
        SceneManager.LoadScene("Menu");

    private void ShowDistance(int distance)
    {
        _distanceText.text = distance.ToString();
    }

    private void ShowBlocksCount(int count) =>
        _blocksText.text = count.ToString();

    private void ShowDiamountCount(int count) =>
        _diamountText.text = count.ToString();

    private void ShowSkullCount(int count) =>
        _skullText.text = count.ToString();
}
