using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingEnemy : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _movingSpeed;

    [SerializeField] private AudioSource _showingSound;

    [SerializeField] private SkinnedMeshRenderer[] _playerRenderers;

    private Player _target;
    private Rigidbody _rigidbody;

    private bool Show;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _target = FindObjectOfType<Player>();

        foreach (SkinnedMeshRenderer meshrenderer in _playerRenderers)
        {
            meshrenderer.enabled = false;
        }
    }

    private void Update()
    {
        if((_target.transform.position - transform.position).magnitude <= _distance)
        {
            if(Show == false)
            {
                foreach (SkinnedMeshRenderer meshrenderer in _playerRenderers)
                {
                    meshrenderer.enabled = true;
                }

                Instantiate(_showingSound);
                
            }

            Show = true;

            GoToTarget();
        }

        if(_target.transform.position.z > transform.position.z + 1)
        {
            Destroy(gameObject);
        }
    }

    private void GoToTarget()
    {
        transform.LookAt(_target.transform, Vector3.up);
        _rigidbody.velocity = transform.forward * _movingSpeed;
    }
}
