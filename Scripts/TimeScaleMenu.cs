using UnityEngine;

public class TimeScaleMenu : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 0;
    }
}
