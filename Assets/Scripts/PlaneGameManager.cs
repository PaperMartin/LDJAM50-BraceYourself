using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlaneGameManager : MonoBehaviour
{
    //[SerializeField]
    //private UnityEvent OnGameReset;
    [SerializeField]
    private UnityEvent OnGameStart;
    [SerializeField]
    private UnityEvent OnGameFixedUpdate;
    [SerializeField]
    private UnityEvent OnGameOver;

    public static PlaneGameManager Instance;

    public float Score { get; private set; } = 0;

    private PlaneController controller;

    private void Awake() {
        Instance = this;
    }

    private void OnEnable() {
        controller = FindObjectOfType<PlaneController>();
    }

    public enum GameState
    {
        Start,
        Running,
        GameOver
    }

    public GameState CurrentState { get; private set; } = GameState.Start;

    private void Start()
    {
        StartGame();
    }

    public void FixedUpdate()
    {
        if (CurrentState == GameState.Running)
        {
            OnGameFixedUpdate.Invoke();
            Score = controller.transform.position.z;
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
        Score = 0;
        if (CurrentState == GameState.GameOver)
        {   
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //CurrentState = GameState.Start;
            //OnGameReset.Invoke();
        }
    }
}
