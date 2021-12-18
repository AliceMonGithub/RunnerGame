using UnityEngine;

public class PlayerStatsBase : MonoBehaviour
{
    public PlayerStatisticRepository PlayerStatisticRepository;
    public PlayerStatisticInteractor PlayerStatisticInteractor;

    private void Awake()
    {
        PlayerStatisticInteractor = new PlayerStatisticInteractor(PlayerStatisticRepository);
    }
}
