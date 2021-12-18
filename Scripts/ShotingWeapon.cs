using UnityEngine;

public class ShotingWeapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var playershoting = other.GetComponent<PlayerShoting>();

        if (playershoting != null)
        {
            playershoting.StartShotAnimation();

            Destroy(gameObject);
        }
    }
}
