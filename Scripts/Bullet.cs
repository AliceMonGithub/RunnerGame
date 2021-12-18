using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;

    [HideInInspector] public Transform Target;

    private void Update()
    {
        transform.LookAt(Target);
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<DamagingEnemy>();

        if(enemy != null)
        {
            enemy.TakeDamage(_damage);

            Destroy(gameObject);
        }
    }
}
