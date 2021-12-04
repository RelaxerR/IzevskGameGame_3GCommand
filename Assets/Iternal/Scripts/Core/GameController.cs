using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public event Action LevelStartedEvent;
    public event Action<bool> LevelEndedEvent;

    private void Awake () {
        
    }
    void Start()
    {
        Instance = this;
        LevelStartedEvent?.Invoke ();
    }

    void Update()
    {
        
    }
}