using UnityEngine;
using System;
using System.Collections;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    
    public bool BeerChosed;
    public float _beerCost { get; private set; }
    public float _beerEnergy { get; private set; }
    public float _vodkaCost { get; private set; }
    public float _vodkaEnergy { get; private set; }
    public float _vodkaEnergyInterval { get; private set; }

    [SerializeField] private float _levelProgress;
    [SerializeField] private float _levelProgressTick;

    [SerializeField] GameObject[] _playerPrefab;
    [SerializeField] private int _playersCount;

    public event Action<GameObject> BringStartEvent;
    public event Action<float> LevelProgressAddedEvent;

    private void Awake () {
        Instance = this;
    }
    private void Start () {
        GameController.Instance.LevelStartedEvent += LevelStarted;
    }
    private void OnDestroy () {
        GameController.Instance.LevelStartedEvent -= LevelStarted;
    }

    private void LevelStarted () {
        var levelNum = GameVariables.Instance._levelNumber;
        Debug.Log (levelNum);

        _playersCount = (int) levelNum / 10;
        if (_playersCount <= 0) _playersCount = 1;
        if (_playersCount > 6) _playersCount = 6;
        _beerCost = levelNum / 100;
        if (_beerCost > 0.2f) _beerCost = 0.2f;
        if (_beerCost < 0.05f) _beerCost = 0.05f;
        _beerEnergy = 0.4f;
        _vodkaCost = levelNum / 100 + 0.1f;
        if (_vodkaCost < 0.05f) _vodkaCost = 0.05f;
        if (_vodkaCost > 0.1f) _vodkaCost = 0.1f;
        _vodkaEnergy = 0.4f;
        _levelProgressTick = 1 - levelNum / 50;
        if (_levelProgressTick < 0.01f) _levelProgressTick = 0.01f;
        if (_levelProgressTick > 0.1f) _levelProgressTick = 0.1f;

        _levelProgress = 0;

        BeerChosed = true;

        SpawnPlayers ();
        StartCoroutine (LevelProgressTick());
    }
    public void GetDrink (out float cost, out float energy) {
        if (BeerChosed)
        {
            cost = _beerCost;
            energy = _beerEnergy;
        }
        else
        {
            cost = _vodkaCost;
            energy = _vodkaEnergy + UnityEngine.Random.Range(-_vodkaEnergyInterval, _vodkaEnergyInterval);
        }
    }

    private void SpawnPlayers () {
        for (int i = 0; i < _playersCount; i++)
        {
            Instantiate (_playerPrefab[UnityEngine.Random.Range(0, _playerPrefab.Length - 1)], new Vector3 (0, 0, 0), Quaternion.identity, this.transform);
        }
    }

    public void BringOutPerson(GameObject target) {
        BringStartEvent?.Invoke (target);
    }

    public void PlayerDead () {
        _playersCount--;
        if (_playersCount <= 0)
        {
            GameController.Instance.GameOver (false);
        }
    }

    private IEnumerator LevelProgressTick () {
        while (_levelProgress <= 1)
        {
            yield return new WaitForSeconds (1f);
            _levelProgress += _levelProgressTick;
            LevelProgressAddedEvent?.Invoke (_levelProgress);
        }
        GameController.Instance.GameOver (true);
    }

    public void AddSummaryMoney (int money) {
        GameVariables.Instance.AddMoney (money);
    }
}