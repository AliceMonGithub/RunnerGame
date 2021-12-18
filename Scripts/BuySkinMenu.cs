using UnityEngine;

public class BuySkinMenu : MonoBehaviour
{
    [SerializeField] private Skins _skins;

    public void TryBuy()
    {
        switch (_skins.CurrentSkin.Currency)
        {
            case Currency.Coins:

                if (PlayerPrefs.GetInt("Coins") >= _skins.CurrentSkin.Cost)
                {
                    _skins.CurrentSkin.Player.PlayerStatsBase.PlayerStatisticInteractor.SpendSavedCoins(_skins.CurrentSkin.Cost);

                    gameObject.SetActive(false);
                }

                break;

            case Currency.Blocks:

                if (PlayerPrefs.GetInt("Blocks") >= _skins.CurrentSkin.Cost)
                {
                    _skins.CurrentSkin.Player.PlayerStatsBase.PlayerStatisticInteractor.SpendSavedBlocks(_skins.CurrentSkin.Cost);

                    gameObject.SetActive(false);
                }

                break;

            case Currency.Skull:

                if (PlayerPrefs.GetInt("Skull") >= _skins.CurrentSkin.Cost)
                {
                    _skins.CurrentSkin.Player.PlayerStatsBase.PlayerStatisticInteractor.SpendSavedSkull(_skins.CurrentSkin.Cost);

                    gameObject.SetActive(false);
                }

                break;
        }
    }
}
