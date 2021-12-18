using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject[] _backgrounds;

    public void ChangeBackground(int index)
    {
        ClearBackground();

        Instantiate(_backgrounds[index], _parent);
    }

    private void ClearBackground()
    {
        foreach(Transform child in _parent)
        {
            Destroy(child.gameObject);
        }
    }
}
