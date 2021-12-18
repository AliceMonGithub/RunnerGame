using UnityEngine;
using UnityEngine.UI;

public class DamagingEnemy : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Animator _animator;
    [SerializeField] private ShotingEnemy _shotingEnemy;
    [SerializeField] private float _dieCooldown;

    public Transform ShotPoint;
    public int Health;

    [Space]

    [SerializeField] private bool _coin;
    [SerializeField] private int _coinsAmount;

    [Space]

    [SerializeField] private bool _block;
    [SerializeField] private int _blocksAmount;

    [Space]

    [SerializeField] private bool _skull;

    private DeadMenu _deadMenu;
    private Player _player;

    private delegate void HealthChangedHandler();
    private event HealthChangedHandler HealthChanged;

    private int MaxHealth;

    private void Awake()
    {
        _deadMenu = FindObjectOfType<DeadMenu>();
        _player = FindObjectOfType<Player>();

        MaxHealth = Health;

        _slider.value = _slider.maxValue;
    }

    private void OnEnable()
    {
        HealthChanged += CheckHealth;
        HealthChanged += RenderHealth;
    }

    private void OnDisable()
    {
        HealthChanged -= CheckHealth;
        HealthChanged -= RenderHealth;
    }

    public void TakeDamage(int value)
    {
        Health -= value;

        HealthChanged.Invoke();
    }

    private void CheckHealth()
    {
        if(Health <= 0)
        {
            Die();
        }
    }

    private void RenderHealth()
    {
        if(Health < 0)
        {
            Health = 0;
        }

        _slider.value = Health / (MaxHealth / 100);
    }

    private void Die()
    {
        if(_animator != null)
        {
            transform.parent = _shotingEnemy.Road;
            _shotingEnemy.enabled = false;

            _animator.SetTrigger("Die");
        }

        if(_coin)
        {
            _player.PlayerStatsBase.PlayerStatisticInteractor.AddSavedCoins(_coinsAmount);
        }
        if(_block)
        {
            _player.PlayerStatsBase.PlayerStatisticInteractor.AddSavedBlocks(_blocksAmount);
        }
        if(_skull)
        {
            _player.PlayerStatsBase.PlayerStatisticInteractor.ChangeSavedSkull(1);
        }

        Invoke("SetDie", _dieCooldown);

        Destroy(gameObject, _dieCooldown + 0.1f);
    }

    private void SetDie()
    {
        _deadMenu.DeadMenuObject.SetActive(true);
        _deadMenu.ShowInformation(_player);
    }
}
