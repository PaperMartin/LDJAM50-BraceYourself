using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaneGameManager : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnGameReset;
    [SerializeField]
    private UnityEvent OnGameStart;
    [SerializeField]
    private UnityEvent OnGameFixedUpdate;
    [SerializeField]
    private UnityEvent OnGameOver;

    public enum GameState
    {
        Start,
        Running,
        GameOver
    }

    public GameState CurrentState { get; private set; } = GameState.Start;

    private void Start() {
        StartGame();
    }

    public void FixedUpdate()
    {
        if (CurrentState == GameState.Running)
        {
            OnGameFixedUpdate.Invoke();
        }
    }

    public void StartGame()
    {
        if (CurrentState == GameState.Start)
        {
            CurrentState = GameState.Running;
            OnGameStart.Invoke();
        }
    }

    public void EndGame()
    {
        if (CurrentState == GameState.Running)
        {
            CurrentState = GameState.GameOver;
            OnGameOver.Invoke();
        }
    }

    public void ResetGame()
    {
        if (CurrentState == GameState.GameOver)
        {
            CurrentState = GameState.Start;
            OnGameReset.Invoke();
        }
    }
}
