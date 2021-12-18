using UnityEngine;

public class ShotingEnemy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _distance;

    [SerializeField] private Transform _rotatingObject;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private StateEnemy _prefab;
    [SerializeField] private Animator _animator;

    [SerializeField] private bool _rotateAllDirection;
    [SerializeField] private bool _secondOffset;
    [SerializeField] private bool _onlyFront;

    private Player _player;
    private Transform _target;
    [HideInInspector] public Transform Road;

    private bool CanShot = true;

    private void Start()
    {
        _player = FindObjectOfType<PlayerShoting>().GetComponent<Player>();
        _target = _player.FirePoint;

        Road = FindObjectOfType<RoadMovement>().transform;
    }

    public void Shot()
    {
        if (_player.enabled && (_target.position - transform.position).magnitude < _distance && _onlyFront == false)
        {
            var bullet = Instantiate(_prefab, _firePoint.position, transform.rotation, Road);

            bullet.transform.LookAt(_target.transform.position);

            if (_secondOffset)
            {
                bullet.transform.eulerAngles = new Vector3(bullet.transform.eulerAngles.x, bullet.transform.eulerAngles.y + 90, bullet.transform.eulerAngles.z);
            }

            CanShot = true;
        }
        else if (_player.enabled && (_target.position - transform.position).magnitude < _distance && _onlyFront && transform.position.z > _target.position.z)
        {
            var bullet = Instantiate(_prefab, _firePoint.position, transform.rotation, Road);

            bullet.transform.LookAt(_target.transform.position);

            if (_secondOffset)
            {
                bullet.transform.eulerAngles = new Vector3(bullet.transform.eulerAngles.x, bullet.transform.eulerAngles.y + 90, bullet.transform.eulerAngles.z);
            }

            CanShot = true;
        }
    }

    private void Update()
    {
        if (_rotateAllDirection)
        {
            _rotatingObject.LookAt(_target.transform.position);
        }
        else
        {
            _rotatingObject.LookAt(_target.transform.position, Vector3.up);
        }

        if (_animator != null)
        {
            if (_player.enabled && (_target.position - transform.position).magnitude < _distance && CanShot && _onlyFront == false)
            {
                _animator.SetTrigger("Shot");

                CanShot = false;
            }
            else if (_player.enabled && (_target.position - transform.position).magnitude < _distance && CanShot && _onlyFront && transform.position.z > _target.position.z)
            {
                _animator.SetTrigger("Shot");

                CanShot = false;
            }
        }
    }
}
