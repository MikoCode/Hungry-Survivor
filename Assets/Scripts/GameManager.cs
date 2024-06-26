using System.Collections;
using System.Collections.Generic;
using HungrySurvivor.Singleton;
using HungrySurvivor.StateMachine;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public EXPSystem expSystem;
    private EXPSystem baseExpSystem;
    
    public float bulletDamage;
    private float baseBulletDamage;

    public UnityEvent OnStartGame;
    public UnityEvent OnPlayerDie;
    public UnityEvent OnEndGame;
    public UnityEvent OnResetGame;

    private void OnEnable()
    {
        OnStartGame.AddListener(StartSettings);
        OnStartGame.AddListener(ResumeGame);
    }

    private void OnDisable()
    {
        OnStartGame.RemoveListener(StartSettings);
        OnStartGame.RemoveListener(ResumeGame);
    }

    void Start()
    {
        // Zapamiętaj bazowe ustawienia
        baseExpSystem = expSystem;  // assuming expSystem is a reference, not a value copy
        baseBulletDamage = bulletDamage;

        //OnStartGame?.Invoke();
    }

    private void StartSettings()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    [Button]
    public void StartGame()
    {
        OnStartGame?.Invoke();
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    [Button]
    public void ResetGame()
    {
        // Przywróć bazowe ustawienia
        expSystem.ResetToFactorySettings();
        bulletDamage = baseBulletDamage;

        // Resetuj gracza
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.ResetPlayer();
        }

        OnResetGame?.Invoke();
    }
}
