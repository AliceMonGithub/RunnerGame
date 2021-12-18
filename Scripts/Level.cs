using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Currency
{
    Coins,
    Blocks,
    Skull
}

public class Level : MonoBehaviour
{
    [SerializeField] private uint _group;
    [SerializeField] private Tile _startTile;
    [SerializeField] private Transform _parent;

    [SerializeField] private int[] _eventCount;

    [Space]

    [SerializeField] private Tile[] _tiles;

    private List<Tile> _randomTiles = new List<Tile>();
    private List<Tile> _spawnedTiles = new List<Tile>();

    private List<GameObject> _objectInLevel = new List<GameObject>();

    private Transform _target;

    private Tile _lastTile;
    private Tile _eventTile;

    private int _currentEvent;
    public int GeneratedTilesCountOnUI;
    public int GeneratedTileCount;
    [HideInInspector] public TMP_Text CountText;

    public bool Free;
    public Currency Currency;
    public int Cost;

    private void Awake()
    {
        _target = FindObjectOfType<Player>().transform;

        _lastTile = Instantiate(_startTile, _parent);

        _spawnedTiles.Add(_lastTile);

        StartSpawn();

        CountText.text = GeneratedTilesCountOnUI.ToString();

        _eventTile = _spawnedTiles[1];
    }

    private void Update()
    {
        if (_target != null)
        {
            if (_target.position.z >= _eventTile.EndPoint.position.z)
            {
                SpawnTile(false);
            }
        }
        else
        {
            _target = FindObjectOfType<Player>().transform;
        }
    }

    private void SpawnTile(bool startTile)
    {
        var newtile = GetRandomTile();

        _lastTile = Instantiate(newtile, _lastTile.EndPoint.position - newtile.StartPoint.localPosition, Quaternion.identity, _parent);

        _spawnedTiles.Add(_lastTile);

        if (!startTile)
        {
            GeneratedTilesCountOnUI++;
            GeneratedTileCount++;

            CheckEvent();
        }
        else
        {
            GeneratedTileCount++;

            CheckEvent();
        }

        CountText.text = GeneratedTilesCountOnUI.ToString();

        if (_spawnedTiles.Count == 6)
        {
            Destroy(_spawnedTiles[0].gameObject);
            _spawnedTiles.RemoveAt(0);

            _eventTile = _spawnedTiles[1];
        }
    }

    private void FindTilesByGroup()
    {
        _randomTiles.Clear();

        foreach (Tile tile in _tiles)
        {
            if (tile.Group == _group)
            {
                _randomTiles.Add(tile);
            }
        }
    }

    private Tile GetRandomTile()
    {
        FindTilesByGroup();

        return _randomTiles[Random.Range(0, _randomTiles.Count)];
    }

    private void CheckEvent()
    {
        if (_currentEvent != _eventCount.Length)
        {
            if (_eventCount[_currentEvent] == GeneratedTileCount)
            {
                _group++;
                _currentEvent++;
            }
        }
    }

    private void StartSpawn()
    {
        for (int count = 0; count < 4; count++)
        {
            SpawnTile(true);
        }
    }
}
