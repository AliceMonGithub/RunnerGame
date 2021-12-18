using UnityEngine;

public class Skull : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private int _value;
    [SerializeField] private AudioSource _pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player != null)
        {
            player.PlayerStatsBase.PlayerStatisticInteractor.AddSkull(_value);

            if (_effect != null)
            {
                Instantiate(_effect, transform.position, Quaternion.identity);
            }

            Instantiate(_pickupSound);

            Destroy(gameObject);
        }
    }
}
