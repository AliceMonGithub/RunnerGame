using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _speed;

    private Transform _target;
    
    private void Awake()
    {
        if ((_startPoint.position - transform.position).magnitude <= 0.1f)
        {
            _target = _endPoint;

            transform.LookAt(_endPoint, Vector3.up);
        }
        else if ((_endPoint.position - transform.position).magnitude <= 0.1f)
        {
            _target = _startPoint;

            transform.LookAt(_startPoint, Vector3.up);
        }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

        if ((_startPoint.position - transform.position).magnitude <= 0.1f)
        {
            _target = _endPoint;

            transform.LookAt(_endPoint, Vector3.up);
        }
        else if ((_endPoint.position - transform.position).magnitude <= 0.1f)
        {
            _target = _startPoint;

            transform.LookAt(_startPoint, Vector3.up);
        }
    }
}
