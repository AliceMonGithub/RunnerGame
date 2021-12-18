using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _coinsTexts;
    [SerializeField] private TMP_Text[] _blocksTexts;
    [SerializeField] private TMP_Text[] _skullTexts;
    [SerializeField] private TMP_Text[] _keyTexts;

    private void Awake()
    {
        Time.timeScale = 1;

        RenderValues();
    }

    public void RenderValues()
    {
        foreach(TMP_Text coinsText in _coinsTexts)
        {
            coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
        }

        foreach (TMP_Text blocksText in _blocksTexts)
        {
            blocksText.text = PlayerPrefs.GetInt("Blocks").ToString();
        }

        foreach (TMP_Text skullText in _skullTexts)
        {
            skullText.text = PlayerPrefs.GetInt("Skull").ToString();
        }

        if (PlayerPrefs.GetInt("Key", 0) == 0)
        {
            foreach(TMP_Text keyText in _keyTexts)
            {
                keyText.text = "Ключ отсутствует";
            }
        }
        else
        {
            foreach (TMP_Text keyText in _keyTexts)
            {
                keyText.text = "Ключ присутствует";
            }
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
