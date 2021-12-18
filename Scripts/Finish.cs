using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private Transform _target;

    private void Awake()
    {
        _target = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        if(_target.position.z > transform.position.z)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
