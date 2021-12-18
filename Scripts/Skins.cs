using TMPro;
using UnityEngine;

public class Skins : MonoBehaviour
{
    [SerializeField] private Skin[] _skins;
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private GameObject _buyMenu;
    [SerializeField] private GameObject _selectButton;
    [SerializeField] private TMP_Text _buyText;
    [SerializeField] private TMP_Text _description;

    [SerializeField] private MainMenu _mainMenu;

    [Space]

    [SerializeField] private int _currentSkin;
    [SerializeField] private int _currentBuySkin;

    [HideInInspector] public Skin CurrentSkin;

    private void Awake()
    {
        var skinID = 0;

        foreach (Skin skin in _skins)
        {
            skin.Purchased = IntToBool(PlayerPrefs.GetInt("Skin" + skinID, 0));

            skinID++;
        }

        _currentSkin = PlayerPrefs.GetInt("SkinID", 0);

        UpdateSkin();
    }

    private bool IntToBool(int value)
    {
        if (value == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SelectNextSkin()
    {
        if (_currentSkin != _skins.Length - 1)
        {
            _currentSkin++;

            UpdateSkin();
        }
    }

    public void SelectLastSkin()
    {
        if (_currentSkin > 0)
        {
            _currentSkin--;

            UpdateSkin();
        }
    }

    private void UpdateSkin()
    {
        foreach (Transform child in _spawnPoint)
        {
            Destroy(child.gameObject);
        }

        Instantiate(_skins[_currentSkin], _spawnPoint);

        if (!_skins[_currentSkin].Purchased && !_skins[_currentSkin].Free && PlayerPrefs.GetInt("Skin" + _currentSkin) == 0)
        {
            _buyMenu.SetActive(true);
            _selectButton.SetActive(false);

            switch (_skins[_currentSkin].Currency)
            {
                case Currency.Coins:
                    _buyText.text = PlayerPrefs.GetInt("Coins", 0) + "/" + _skins[_currentSkin].Cost + " монеток";
                    break;
                case Currency.Blocks:
                    _buyText.text = PlayerPrefs.GetInt("Blocks", 0) + "/" + _skins[_currentSkin].Cost + " блоков";
                    break;
                case Currency.Skull:
                    _buyText.text = "Скин покупается за череп";
                    break;
            }

            _currentBuySkin = _currentSkin;
        }
        else
        {
            _buyMenu.SetActive(false);
            _selectButton.SetActive(true);

            CurrentSkin = _skins[_currentSkin];
        }

        _description.text = $"{CurrentSkin.Player.Description}\n" +
            $"Health: {CurrentSkin.Player.PlayerRepository.Health}\n" +
            $"Sensitivity: {CurrentSkin.Player.Sensitivity}\n" +
            $"Speed: {CurrentSkin.Player.PlayerRepository.MoveSpeed}\n";
    }

    public void SelectSkin()
    {
        PlayerPrefs.SetInt("SkinID", _currentSkin);
    }

    public void TryBuy()
    {
        switch (_skins[_currentBuySkin].Currency)
        {
            case Currency.Coins:

                if (PlayerPrefs.GetInt("Coins") >= _skins[_currentBuySkin].Cost)
                {
                    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - _skins[_currentBuySkin].Cost);

                    _buyMenu.SetActive(false);

                    _skins[_currentBuySkin].Purchased = true;

                    _mainMenu.RenderValues();

                    _selectButton.SetActive(true);

                    PlayerPrefs.SetInt("Skin" + _currentSkin, 1);
                }

                break;

            case Currency.Blocks:

                if (PlayerPrefs.GetInt("Blocks") >= _skins[_currentBuySkin].Cost)
                {
                    PlayerPrefs.SetInt("Blocks", PlayerPrefs.GetInt("Blocks") - _skins[_currentBuySkin].Cost);

                    _buyMenu.SetActive(false);

                    _skins[_currentBuySkin].Purchased = true;

                    _mainMenu.RenderValues();

                    _selectButton.SetActive(true);

                    PlayerPrefs.SetInt("Skin" + _currentSkin, 1);
                }

                break;

            case Currency.Skull:

                if (PlayerPrefs.GetInt("Skull") == 1)
                {
                    PlayerPrefs.SetInt("Skull", 0);

                    _buyMenu.SetActive(false);

                    _skins[_currentBuySkin].Purchased = true;

                    _mainMenu.RenderValues();

                    _selectButton.SetActive(true);

                    PlayerPrefs.SetInt("Skin" + _currentSkin, 1);
                }

                break;
        }
    }
}
