using TMPro;
using UnityEngine;

public class BuyLevelMenu : MonoBehaviour
{
    [SerializeField] private Levels _levels;
    public Transform PriceParent;
    public TMP_Text[] CostTexts;

    public void TryBuy()
    {
        switch(_levels.CurrentLevel.Currency)
        {
            case Currency.Coins:

                if (PlayerPrefs.GetInt("Coins") >= _levels.CurrentLevel.Cost)
                {
                    _levels.Player.PlayerStatsBase.PlayerStatisticInteractor.SpendSavedCoins(_levels.CurrentLevel.Cost);

                    _levels.StartLevel();

                    gameObject.SetActive(false);
                }

                break;

            case Currency.Blocks:

                if (PlayerPrefs.GetInt("Blocks") >= _levels.CurrentLevel.Cost)
                {
                    _levels.Player.PlayerStatsBase.PlayerStatisticInteractor.SpendSavedBlocks(_levels.CurrentLevel.Cost);

                    _levels.StartLevel();

                    gameObject.SetActive(false);
                }

                break;

            case Currency.Skull:

                if (PlayerPrefs.GetInt("Skull") >= _levels.CurrentLevel.Cost)
                {
                    _levels.Player.PlayerStatsBase.PlayerStatisticInteractor.SpendSavedSkull(_levels.CurrentLevel.Cost);

                    _levels.StartLevel();

                    gameObject.SetActive(false);
                }

                break;
        }
    }

    public bool CheckBuy(Level level)
    {
        switch (level.Currency)
        {
            case Currency.Coins:

                if (PlayerPrefs.GetInt("Coins") >= level.Cost)
                {
                    return true;
                }

                break;

            case Currency.Blocks:

                if (PlayerPrefs.GetInt("Blocks") >= level.Cost)
                {
                    return true;
                }

                break;

            case Currency.Skull:

                if (PlayerPrefs.GetInt("Skull") == 1)
                {
                    return true;
                }

                break;
        }

        return false;
    }
}
