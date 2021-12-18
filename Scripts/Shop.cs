using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Price")]

    [SerializeField] private int _blocksPrice;
    [SerializeField] private int _blocksCount;

    [SerializeField] private int _skullPrice;
    [SerializeField] private int _skullCount;

    private MainMenu _mainMenu;
    private PlayerStatisticInteractor _statsInteractor = new PlayerStatisticInteractor(new PlayerStatisticRepository());

    private delegate void BuyResourceHandler();
    private event BuyResourceHandler BuyResources;

    private void OnEnable()
    {
        BuyResources += _mainMenu.RenderValues;
    }

    private void OnDisable()
    {
        BuyResources -= _mainMenu.RenderValues;
    }

    private void Awake()
    {
        _mainMenu = FindObjectOfType<MainMenu>();
    }

    public void BuyBlocks()
    {
        if(PlayerPrefs.GetInt("Coins") >= _blocksPrice)
        {
            _statsInteractor.SaveBlocks(_blocksCount);

            _statsInteractor.SpendSavedCoins(_blocksPrice);

            BuyResources.Invoke();
        }
    }

    public void BuySkull()
    {
        if(PlayerPrefs.GetInt("Coins") >= _skullPrice)
        {
            _statsInteractor.AddSavedSkull(_skullCount);

            _statsInteractor.SpendSavedCoins(_skullPrice);

            BuyResources.Invoke();
        }
    }
}
