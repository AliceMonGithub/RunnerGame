using UnityEngine;

public class PlayerShoting : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private Transform _parent;

    public Transform Target;
    [SerializeField] private Animator _animator;

    public void UpdateTarget()
    {
        if (FindObjectOfType<DamagingEnemy>() != null)
        {
            Target = FindObjectOfType<DamagingEnemy>().ShotPoint;
            _parent = FindObjectOfType<RoadMovement>().transform;
        }
    }

    public void Shot()
    {
        var bullet = Instantiate(_bullet, _shotPoint.position, transform.rotation, _parent);
        bullet.transform.LookAt(Target);
        bullet.Target = Target;
    }

    public void StartShotAnimation()
    {
        _animator.SetTrigger("Shot");
    }
}
