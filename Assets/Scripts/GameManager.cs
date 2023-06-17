using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager s_instance;
    public GameObject canvas;
    private GameState m_gameState;
    int mainMenuScene = 0;
    public bool isGameOver, isStarted;
    public GameObject gameOverUI;

    private void Awake() {
        if (FindObjectOfType<GameManager>() != null &&
            FindObjectOfType<GameManager>().gameObject != gameObject) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        s_instance = this;
        m_gameState = GameState.None;
    }

    private void Start() {
        isGameOver = false;
        isStarted = false;
    }

    public void changeGameSate(GameState t_newState) {
        if (m_gameState == t_newState) {
            return;
        }
        m_gameState = t_newState;
        switch (m_gameState) {
            case GameState.None:
                break;
            case GameState.LoadMenu:
                break;
            case GameState.StartGame:
                startGame();
                break;
            case GameState.ChangeLevel:
                break;
            case GameState.Playing:
                break;
            case GameState.GameOver:
                gameOver();
                break;
            case GameState.GameFinished:
                break;
            default:
                throw new UnityException("Invalid Game State");
        }
    }

    public GameState getGameState() { 
        return m_gameState; 
    }

    public void startGame() {
        changeGameSate(GameState.Playing);
    }

    void gameOver() {
        if (canvas != null) {
            canvas.SetActive(true);
            gameOverUI.SetActive(true);
        }
    }
}


public enum GameState {
    None,
    LoadMenu,
    StartGame,
    ChangeLevel,
    Playing,
    GameOver,
    GameFinished
}
