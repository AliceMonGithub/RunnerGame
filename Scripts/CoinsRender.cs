using UnityEngine;
using TMPro;

public class CoinsRender : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsText;

    [SerializeField] private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<PlayerShoting>().GetComponent<Player>();

        RenderText();

        _player.PlayerStatsBase.PlayerStatisticInteractor.CoinsValueChanged += RenderText;
    }

    private void OnDisable()
    {
        _player.PlayerStatsBase.PlayerStatisticInteractor.CoinsValueChanged -= RenderText;
    }

    public void RenderText()
    {
        _coinsText.text = _player.PlayerStatsBase.PlayerStatisticInteractor.Coins.ToString();
    }
}
