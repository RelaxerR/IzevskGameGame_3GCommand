using UnityEngine;

public class GameVariables : MonoBehaviour
{
    public static GameVariables Instance { get; private set; }

    public int _levelNumber { get; private set; }
    public int _moneyCount { get; private set; }

    private string _moneyKey = "Money";
    private string _levelNumberKey = "Level";

    private void Awake () {
        Instance = this;
    }
    private void Start()
    {
        LoadVariables ();
        GameController.Instance.LevelEndedEvent += LevelEnded;
    }
    private void OnDestroy () {
        GameController.Instance.LevelEndedEvent -= LevelEnded;
    }

    private void LoadVariables () {
        if (PlayerPrefs.HasKey (_moneyKey))
        {
            _levelNumber = PlayerPrefs.GetInt (_levelNumberKey);
            _moneyCount = PlayerPrefs.GetInt (_moneyKey);
        }
        else
        {
            PlayerPrefs.SetInt (_levelNumberKey, 0);
            PlayerPrefs.SetInt (_moneyKey, 0);
        }
    }
    private void SaveVariables () {
        PlayerPrefs.SetInt (_levelNumberKey, _levelNumber);
        PlayerPrefs.SetInt (_moneyKey, _moneyCount);
    }

    private void LevelEnded(bool win) {
        if (win)
        {
            NextLevel ();
        }
    }

    public void NextLevel () {
        _levelNumber++;
        Debug.Log (_levelNumber);
        SaveVariables ();
    }

    public bool TrySpendMoney (int price) {
        Debug.Log (_moneyCount);
        if (_moneyCount >= price)
        {
            _moneyCount -= price;
            SaveVariables ();
            return true;
        }
        else
        {
            return false;
        }
    }
    public void AddMoney(int money) {
        _moneyCount += money;
        SaveVariables ();
    }
}