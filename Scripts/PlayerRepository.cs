using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerRepository
{
    public List<Effect> PlayerEffects;

    public float MoveSpeed;

    public int InjuredHealth;
    public int injuredEffectTime;

    public int MaxHealth;
    public int Health { get => _health; set 
        { 
            if(value > MaxHealth)
            {
                _health = MaxHealth;
            }
            else if(value < 0)
            {
                _health = 0;
            }
            else
            {
                _health = value;
            }
        } 
    }

    [SerializeField] private int _health;

    public bool Invulnerability;
}
