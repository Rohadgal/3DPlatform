using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Public
    public static GameManager s_instance;
    public string currentLevel, nextLevel;
    #endregion

    #region Private
    private GameState m_gameState;
    int mainMenuScene = 0, levelIndex;
    bool isCoroutineActivated;
    #endregion


    [SerializeField] GameObject canvas, winUI, gameOverUI, creditsUI;

    private void Awake() {
        if (canvas != null && SceneManager.GetActiveScene().buildIndex != mainMenuScene) {
            canvas.SetActive(false);
        }
        if (FindObjectOfType<GameManager>() != null &&
            FindObjectOfType<GameManager>().gameObject != gameObject) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(gameObject);
        s_instance = this;
        m_gameState = GameState.None;
        isCoroutineActivated = false;
        
    }

    private void Start()
    {
        currentLevel = "";
        nextLevel = "LevelOne";
    }

    private void Update() {
        if (m_gameState == GameState.GameOver) {
            gameOver();
        }
        if (m_gameState == GameState.GameFinished && !isCoroutineActivated) {
            isCoroutineActivated = true;
            gameFinished();
        }
        Debug.Log("game state: " + m_gameState);
    }

    void turnOffUI(){
        canvas.SetActive(false);
        winUI.SetActive(false);
        gameOverUI.SetActive(false);
        creditsUI.SetActive(false);
    }

    public void changeScene() {
        Debug.Log("next level: " + nextLevel);
        SceneManager.LoadScene(nextLevel);
        currentLevel = nextLevel;
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
                loadMainMenu();
                break;
            case GameState.ChangeLevel:
                changeScene();
                break;
            case GameState.Playing:
                turnOffUI();
                break;
            case GameState.GameOver:
                //gameOver();
                break;
            case GameState.GameFinished:
                StartCoroutine(openCredits());
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

    IEnumerator openCredits() {
        yield return new WaitForSeconds(4f);
        winUI.SetActive(false);
        creditsUI.SetActive(true);
    }

    public void loadMainMenu() {
        Destroy(PlayerManager.instance.gameObject);
        Destroy(gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    void gameOver() {
        if (canvas != null) {
            StartCoroutine(slowDownGameOverCanvas());
        }
    }

    void gameFinished() {
        if (canvas != null) {
            canvas.SetActive(true);
            winUI.SetActive(true);
        }

        //if (SceneManager.GetActiveScene().name == "LevelThree1" && !isCoroutineActivated) {
        //    isCoroutineActivated=true;
        //    //canvas.SetActive(true);
        //    //StartCoroutine(openCredits());
        //    Debug.LogError("YAAAA");
        //}
    }

    IEnumerator slowDownGameOverCanvas() {
        yield return new WaitForSeconds(4f);
            canvas.SetActive(true);
            gameOverUI.SetActive(true);
    }

    public void exitGame() {
        Application.Quit();
    }

    public void retryLevel() {
        canvas.SetActive(false);
        gameOverUI.SetActive(false);
        changeGameSate(GameState.Playing);
        PlayerManager.instance.ChangePlayerState(PlayerState.Idle);
        PlayerManager.instance.respawn();
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelIndex);
    }

}


public enum GameState {
    None,
    LoadMenu,
    ChangeLevel,
    Playing,
    GameOver,
    GameFinished
}
