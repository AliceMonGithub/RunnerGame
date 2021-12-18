using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private int _value;
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private AudioSource _pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player != null)
        {
            if(player.PlayerInteractor.Health != player.PlayerInteractor.MaxHealth)
            {
                player.PlayerInteractor.AddHealth(_value);

                if (_effect != null)
                {
                    Instantiate(_effect, transform.position, Quaternion.identity);
                }
            }

            Instantiate(_pickupSound);

            Destroy(gameObject);
        }
    }
}
