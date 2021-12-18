using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player != null)
        {
            var result = player.PlayerStatsBase.PlayerStatisticInteractor.ChangeKeyValue(1);

            if (_effect != null)
            {
                Instantiate(_effect, transform.position, Quaternion.identity);
            }

            if (result)
            {
                Destroy(gameObject);
            }
        }
    }
}
