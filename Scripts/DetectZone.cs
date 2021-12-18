using UnityEngine;

public class DetectZone : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    public void SetDetectZone(bool value)
    {
        _player.InDetectZone = value;
    }
}
