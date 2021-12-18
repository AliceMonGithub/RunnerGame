using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _value;
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private AudioSource _pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if(player != null)
        {
            player.PlayerStatsBase.PlayerStatisticInteractor.AddCoins(_value);

            if(_effect != null) {
                Instantiate(_effect, transform.position, Quaternion.identity);
            }

            Instantiate(_pickupSound);

            Destroy(gameObject);
        }
    }
}
