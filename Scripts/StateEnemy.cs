using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StateEnemy : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private bool _onlyFrontSide;
    [SerializeField] private bool _isDestroing;

    private Vector3 _frontSide;
    private Collider _collider;

    private void Awake()
    {
        if(_onlyFrontSide)
        { 
            _frontSide = new Vector3(transform.position.x, transform.position.y, transform.position.z - _collider.bounds.size.z / 2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if(player != null && !player.PlayerInteractor.Invulnerability && player.transform.position.z < _frontSide.z)
        {
            player.PlayerInteractor.SpentHealth(_damage);
        }

        if(player != null)
        {
            if (_isDestroing)
            {
                Destroy(gameObject);
            }
        }
    }
}