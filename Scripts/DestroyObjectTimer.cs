using UnityEngine;

public class DestroyObjectTimer : MonoBehaviour
{
    [SerializeField] private float _time;

    private void Start()
    {
        Destroy(gameObject, _time);
    }
}
