using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveForward : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool MoveRight;

    private Rigidbody _rigitbody;

    private void Awake()
    {
        _rigitbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(MoveRight)
        {
            _rigitbody.velocity = -transform.right * _speed;

            return;
        }
        _rigitbody.velocity = transform.forward * _speed;
    }
}
