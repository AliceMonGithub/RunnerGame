using UnityEngine;
using TMPro;

public class HealthRender : MonoBehaviour
{
    [SerializeField] private TMP_Text _renderHealth;

    [SerializeField] private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<PlayerShoting>().GetComponent<Player>();

        RenderHealth(true);

        _player.PlayerInteractor.HealthChanged += RenderHealth;
    }

    private void OnDisable()
    {
        _player.PlayerInteractor.HealthChanged -= RenderHealth;
    }

    private void RenderHealth(bool PlusHealth)
    {
         _renderHealth.text = " X " + _player.PlayerInteractor.Health.ToString();
    }
}
