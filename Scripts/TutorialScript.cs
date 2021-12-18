using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] private bool _tutorialEnable;
    [SerializeField] private Levels _levels;

    public Animator ButtonAnimator;

    private void Awake()
    {

        if (!_tutorialEnable) return;

        var result = CheckTutorial();

        if (ButtonAnimator != null && result != null)
        {
            _levels.TutorialLevel = result;
            ButtonAnimator.SetTrigger("Tutorial");
        }
    }

    public Level CheckTutorial()
    {
        for (int i = _levels.GameLevels.Length - 1; i > -1; i--)
        {
            if(i != 0 && _levels.GameLevels[i].Free)
            {
                return _levels.GameLevels[i];
            }

            if (i != 0 && _levels.BuyMenu.CheckBuy(_levels.GameLevels[i]))
            {
                return _levels.GameLevels[i];
            }
        }

        return null;
    }
}
