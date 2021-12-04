using UnityEngine;

public class GameVariables : MonoBehaviour
{
    public static GameVariables Instance { get; private set; }

    public static bool LevelRunning;

    public int LevelNumber { get; private set; }
    public int MoneyCount { get; private set; }

    private void Awake () {
        
    }
    private void Start()
    {
        Instance = this;

        //GameController.Instance.LevelStartedEvent += LevelStarted;
        //GameController.Instance.LevelEndedEvent += LevelEnded;
        LoadVariables ();
    }
    private void OnDestroy () {
        GameController.Instance.LevelStartedEvent -= LevelStarted;
        GameController.Instance.LevelEndedEvent -= LevelEnded;
    }

    private void LoadVariables () {
        if (PlayerPrefs.HasKey ("Money"))
        {
            LevelNumber = PlayerPrefs.GetInt ("LevelNumber");
            MoneyCount = PlayerPrefs.GetInt ("Money");
        }
        else
        {
            PlayerPrefs.SetInt ("LevelNumber", 0);
            PlayerPrefs.SetInt ("Money", 0);
        }
    }
    private void SaveVariables () {
        PlayerPrefs.SetInt ("LevelNumber", LevelNumber);
        PlayerPrefs.SetInt ("Money", MoneyCount);
    }

    private void LevelStarted () {
        LevelRunning = true;
    }
    private void LevelEnded(bool win) {
        if (win)
        {

            SaveVariables();
        }
        else
        {
            
        }
    }
}