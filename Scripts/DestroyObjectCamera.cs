using UnityEngine;

public class DestroyObjectCamera : MonoBehaviour
{
    private Transform _camera;

    private void Awake()
    {
        _camera = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if(_camera.position.z > transform.position.z)
        {
            Destroy(gameObject, 1f);
        }
    }
}
