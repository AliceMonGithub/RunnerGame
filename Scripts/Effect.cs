using System.Collections;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public float Time;

    [HideInInspector] public Player Player;

    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player != null)
        {
            var result = TakeEffect(player);

            if (result)
            {
                player.PlayerInteractor.PlayerEffects.Add(this);

                Player = player;

                _meshRenderer.enabled = false;
            }
        }
    }

    public abstract bool TakeEffect(Player player);
    public abstract IEnumerator ClearEffect();
}
