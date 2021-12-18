using System;
using TMPro;
using UnityEngine;

[Serializable]
class SpawnObject
{
    public Level Level;
    public GameObject[] Objects;
}

public class Levels : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectsForDeactive;
    [SerializeField] private SpawnObject[] _spawnObjects;
    [SerializeField] private Background _background;

    [SerializeField] private TutorialScript _tutorialScript;
    [SerializeField] private HighScore _highScore;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _objectPool;

    [SerializeField] private TMP_Text _distanceText;
    [SerializeField] private TMP_Text _levelText;

    public Level[] GameLevels;
    public BuyLevelMenu BuyMenu;
    public int ÑurrentLevelIndex;

    [HideInInspector] public Level CurrentLevel;
    [HideInInspector] public Player Player;
    [HideInInspector] public Level TutorialLevel;

    private RoadMovement _road;

    private void Awake()
    {
        Player = FindObjectOfType<Player>();
        _road = FindObjectOfType<RoadMovement>();

        Time.timeScale = 1f;

        GameLevels[ÑurrentLevelIndex].CountText = _distanceText;

        CurrentLevel = Instantiate(GameLevels[ÑurrentLevelIndex], _spawnPoint);

        _levelText.text = (ÑurrentLevelIndex + 1).ToString();

        CheckBuyMenu();

        _background.ChangeBackground(ÑurrentLevelIndex);
    }

    public void OpenNextLevel()
    {
        if (ÑurrentLevelIndex + 1 < GameLevels.Length)
        {
            ÑurrentLevelIndex++;

            foreach (Transform child in _spawnPoint)
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in _objectPool)
            {
                Destroy(child.gameObject);
            }

            GameLevels[ÑurrentLevelIndex].CountText = _distanceText;

            CurrentLevel = Instantiate(GameLevels[ÑurrentLevelIndex], _spawnPoint);

            _levelText.text = (ÑurrentLevelIndex + 1).ToString();

            foreach (SpawnObject spawnObject in _spawnObjects)
            {
                if (spawnObject.Level == GameLevels[ÑurrentLevelIndex])
                {
                    foreach (GameObject gameObject in spawnObject.Objects)
                    {
                        var spawningObject = Instantiate(gameObject, _objectPool.position, Quaternion.identity);

                        spawningObject.transform.SetParent(_objectPool, true);
                    }
                }
            }

            CheckBuyMenu();

            if (TutorialLevel == GameLevels[ÑurrentLevelIndex])
            {
                _tutorialScript.ButtonAnimator.SetTrigger("Tutorial");
            }

            _highScore.RefreshHighScore();

            _background.ChangeBackground(ÑurrentLevelIndex);
        }
    }

    public void OpenPreviousLevel()
    {
        if (ÑurrentLevelIndex - 1 >= 0)
        {
            ÑurrentLevelIndex--;

            foreach (Transform child in _spawnPoint)
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in _objectPool)
            {
                Destroy(child.gameObject);
            }

            GameLevels[ÑurrentLevelIndex].CountText = _distanceText;

            CurrentLevel = Instantiate(GameLevels[ÑurrentLevelIndex], _spawnPoint);

            _levelText.text = (ÑurrentLevelIndex + 1).ToString();

            foreach (SpawnObject spawnObject in _spawnObjects)
            {
                if (spawnObject.Level == GameLevels[ÑurrentLevelIndex])
                {
                    foreach (GameObject gameObject in spawnObject.Objects)
                    {
                        var spawningObject = Instantiate(gameObject, _objectPool.position, Quaternion.identity);

                        spawningObject.transform.SetParent(_objectPool, true);
                    }
                }
            }

            CheckBuyMenu();

            _highScore.RefreshHighScore();

            _background.ChangeBackground(ÑurrentLevelIndex);
        }
    }

    public void StartLevel()
    {
        Player.enabled = true;
        Player.GetComponent<Animator>().SetTrigger("Idle");

        _road.enabled = true;
        gameObject.SetActive(false);
    }

    public void CheckBuyMenu()
    {
        if (!CurrentLevel.Free)
        {
            BuyMenu.gameObject.SetActive(true);

            foreach (GameObject gameObject in _objectsForDeactive)
            {
                gameObject.SetActive(false);
            }

            foreach(Transform child in BuyMenu.PriceParent)
            {
                Destroy(child.gameObject);
            }

            Instantiate(BuyMenu.CostTexts[ÑurrentLevelIndex], BuyMenu.PriceParent);
        }
        else
        {
            foreach (GameObject gameobject in _objectsForDeactive)
            {
                gameobject.SetActive(true);
            }

            BuyMenu.gameObject.SetActive(false);
        }
    }
}
