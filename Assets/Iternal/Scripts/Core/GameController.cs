using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public event Action LevelStartedEvent;
    public event Action<bool> LevelEndedEvent;

    private bool _isGameStart;
    public bool IsGameStart => _isGameStart;

    private void Awake () {
        Instance = this;
    }
    void Start()
    {
        
    }

    public void GameStart () {
        if (_isGameStart)
            return;

        LevelStartedEvent?.Invoke ();
        _isGameStart = true;
    }
    public void GameOver (bool isWin) {
        if (!_isGameStart)
            return;

        LevelEndedEvent?.Invoke (isWin);
        _isGameStart = false;
    }

    public void RestartGame () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
    }
    public void NextLevel () {
        GameVariables.Instance.NextLevel ();
        RestartGame ();
    }
}