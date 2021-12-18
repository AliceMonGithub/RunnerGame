using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    private float _speed => _player.PlayerInteractor.MoveSpeed;

    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.back * _speed * Time.deltaTime);
    }
}
