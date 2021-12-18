using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Sprite _notMuteSprite;
    [SerializeField] private Sprite _muteSprite;
    [SerializeField] private Image _soundImage;

    private void Start()
    {
        CheckMute();
    }

    public void MuteMusic()
    {
        if (PlayerPrefs.GetInt("MusicMute") == 0)
        {
            _audioMixer.SetFloat("Sounds", -80f);

            PlayerPrefs.SetInt("MusicMute", 1);

            _soundImage.sprite = _muteSprite;
        }
        else
        {
            _audioMixer.SetFloat("Sounds", 0f);

            PlayerPrefs.SetInt("MusicMute", 0);

            _soundImage.sprite = _notMuteSprite;
        }
    }

    private void CheckMute()
    {
        if(PlayerPrefs.GetInt("MusicMute") == 0)
        {
            _soundImage.sprite = _notMuteSprite;

            _audioMixer.SetFloat("Sounds", 0f);
        }
        else
        {
            _audioMixer.SetFloat("Sounds", -80f);

            _soundImage.sprite = _muteSprite;
        }
    }
}
