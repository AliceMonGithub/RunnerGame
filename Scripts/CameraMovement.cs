using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _smoothTime;

    [Space]

    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _rotateOffset;

    private void Start()
    {
        _target = FindObjectOfType<PlayerShoting>().transform;
    }

    private void LateUpdate()
    {
        if (_target != null)
        {
            var smoothposition = Vector3.Lerp(transform.position, _target.position + _offset, _smoothTime);

            transform.position = new Vector3(smoothposition.x, _target.position.y, _target.position.z) + _offset;
            transform.eulerAngles = _rotateOffset;
        }
        else
        {
            _target = FindObjectOfType<Player>().transform;
        }

    }
}
