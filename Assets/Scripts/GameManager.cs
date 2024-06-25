using System;
using System.Collections;
using System.Collections.Generic;
using HungrySurvivor.Singleton;
using HungrySurvivor.StateMachine;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    //public static GameManager Instance { get; private set; }

    public EXPSystem expSystem;
    private EXPSystem baseExpSystem;
    
    public float bulletDamage;
    private float baseBulletDamage;

    //public State GameOverState;

    public UnityEvent OnStartGame;
    public UnityEvent OnPlayerDie;
    public UnityEvent OnEndGame;

    private void OnEnable()
    {
        OnStartGame.AddListener(StartSettings);
        OnStartGame.AddListener(ResumeGame);
        OnPlayerDie.AddListener(PauseGame);
    }

    private void OnDisable()
    {
        OnStartGame.RemoveListener(StartSettings);
        OnStartGame.RemoveListener(ResumeGame);
        OnPlayerDie.RemoveListener(PauseGame);
    }


    void Start()
    {
        OnStartGame?.Invoke();
    }

    private void StartSettings()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
