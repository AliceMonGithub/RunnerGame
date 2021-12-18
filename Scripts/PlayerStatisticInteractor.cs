using UnityEngine;

public class PlayerStatisticInteractor
{
    private PlayerStatisticRepository _characterStats;

    public int Coins => _characterStats.Coins;
    public int Blocks => _characterStats.Blocks;
    public int Skull => _characterStats.Skull;
    public bool Key => _characterStats.Key;

    public delegate void CoinsValueHandler();
    public event CoinsValueHandler CoinsValueChanged;

    public delegate void BlocksValueHandler();
    public event BlocksValueHandler BlocksValueChanged;

    public delegate void SkullValueHandler();
    public event SkullValueHandler SkullValueChanged;

    public PlayerStatisticInteractor(PlayerStatisticRepository repository)
    {
        _characterStats = repository;
    }

    public void AddCoins(int value)
    {
        _characterStats.Coins += value;

        CoinsValueChanged?.Invoke();
    }

    public void AddBlocks(int value)
    {
        _characterStats.Blocks += value;

        BlocksValueChanged?.Invoke();
    }

    public void AddSkull(int value)
    {
        _characterStats.Skull += value;

        SkullValueChanged?.Invoke();
    }

    public void SpentBlocks(int value)
    {
        _characterStats.Blocks -= value;

        BlocksValueChanged?.Invoke();
    }

    public void SpendSavedBlocks(int value) => PlayerPrefs.SetInt("Blocks", PlayerPrefs.GetInt("Blocks") - value);
    public void SpendSavedSkull(int value) => PlayerPrefs.SetInt("Skull", PlayerPrefs.GetInt("Skull") - value);
    public void SpendSavedCoins(int value) => PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - value);
    public void ChangeSavedSkull(int value) => PlayerPrefs.SetInt("Skull", value);
    public void ChangeSavedKey(int value) => PlayerPrefs.SetInt("Key", value);
    public void AddSavedBlocks(int value) => PlayerPrefs.SetInt("Blocks", PlayerPrefs.GetInt("Blocks") + value);
    public void AddSavedSkull(int value) => PlayerPrefs.SetInt("Skull", PlayerPrefs.GetInt("Skull") + value);
    public void AddSavedCoins(int value) => PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + value);

    public bool ChangeKeyValue(int value)
    {
        if (Key)
        {
            return false;
        }

        if (value == 0)
        {
            _characterStats.Key = false;

            return false;
        }
        else
        {
            _characterStats.Key = true;

            return true;
        }
    }
    public void SpentCoins(int value)
    {
        _characterStats.Coins -= value;

        CoinsValueChanged?.Invoke();
    }

    public void SaveCoins() => PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + _characterStats.Coins);
    public void SaveSkull() => PlayerPrefs.SetInt("Skull", PlayerPrefs.GetInt("Skull") + _characterStats.Skull);
    public int GetCoinsValue() => PlayerPrefs.GetInt("Coins", 0);
    public void SaveBlocks() => PlayerPrefs.SetInt("Blocks", PlayerPrefs.GetInt("Blocks") + _characterStats.Blocks);
    public void SaveBlocks(int value) => PlayerPrefs.SetInt("Blocks", PlayerPrefs.GetInt("Blocks") + value);

    public void SaveValues()
    {
        SaveBlocks();
        SaveCoins();
        SaveSkull();
        SaveKey();
    }

    public int GetBlocksValue() => PlayerPrefs.GetInt("Blocks", 0);

    public void SaveKey()
    {
        if (Key)
        {
            PlayerPrefs.SetInt("Key", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Key", 0);
        }
    }
}
