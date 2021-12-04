using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    private int _playersCount;
    public float _beerCost { get; private set; }
    public float _beerEnergy { get; private set; }
    public float _vodkaCost { get; private set; }
    public float _vodkaEnergy { get; private set; }
    public float _vodkaEnergyInterval { get; private set; }

    private void LevelStarted () {
        var levelNum = GameVariables.Instance.LevelNumber;

        _playersCount = (int) levelNum / 10;
        _beerCost = 0.1f;
        _beerEnergy = 0.4f;
        _vodkaCost = 0.01f;
        _vodkaEnergy = 0.4f;
    }

    private void Start () {
        Instance = this;
    }
}