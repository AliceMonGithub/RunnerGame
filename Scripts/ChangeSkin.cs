using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    public Skin[] Skins;

    private void Awake()
    {
        Instantiate(Skins[PlayerPrefs.GetInt("SkinID", 0)].Player.gameObject, _spawnPoint.position, Quaternion.identity);
    }
}
