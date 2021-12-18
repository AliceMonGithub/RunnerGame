using System.Collections.Generic;

public class PlayerInteractor
{
    public PlayerInteractor(PlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    private PlayerRepository _playerRepository;

    public delegate void HealthChangedHandler(bool PlusHealth);
    public event HealthChangedHandler HealthChanged;

    public List<Effect> PlayerEffects => _playerRepository.PlayerEffects;
    public float MoveSpeed => _playerRepository.MoveSpeed;
    public int Health => _playerRepository.Health;
    public int MaxHealth => _playerRepository.MaxHealth;
    public int InjuredHealth => _playerRepository.InjuredHealth;
    public int InjuredEffectTime => _playerRepository.injuredEffectTime;
    public bool Invulnerability => _playerRepository.Invulnerability;

    public void AddHealth(int value)
    {
        _playerRepository.Health += value;

        HealthChanged.Invoke(true);
    }

    public void SpentHealth(int value)
    {
        _playerRepository.Health -= value;

        HealthChanged.Invoke(false);
    }

    public void SetHealth(int value)
    {
        bool result = false;

        if(_playerRepository.Health < value)
        {
            result = true;
        }

        _playerRepository.Health = value;

        HealthChanged.Invoke(result);
    }

    public void ChangeInvulnerability(bool value)
    {
        _playerRepository.Invulnerability = value;
    }

    public void ChangeSpeed(float value)
    {
        _playerRepository.MoveSpeed = value;
    }
}
