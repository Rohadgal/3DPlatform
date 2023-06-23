using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager s_instance;

    LevelState m_levelState;
    int platformSpawnSection = 0;
    private float time = 2.2f;
    [SerializeField] GameObject playerPrefab, spawnPoint, mainCamera;

    private void Awake() {
        if (FindObjectOfType<LevelManager>() != null &&
            FindObjectOfType<LevelManager>().gameObject != gameObject) {
            Destroy(gameObject);
        } else {
            s_instance = this;
        }
        if (FindObjectOfType<PlayerManager>() == null)
        {
            Instantiate(playerPrefab);
        }
        PlayerManager.instance.spawnPoint = spawnPoint.transform;
        PlayerManager.instance.mainCamera = mainCamera;
        PlayerManager.instance.assignCamera();
        m_levelState = LevelState.None;
    }

    private void Start(){
        setPlatformSpawnArea(0);
        m_levelState = LevelState.Continue;
        GameManager.s_instance.changeGameSate(GameState.Playing);
        PlayerManager.instance.ChangePlayerState(PlayerState.Idle);
    }

    public void changeLevelState(LevelState state) {
        m_levelState = state;
        switch (m_levelState){
            case LevelState.LevelFinished:
                GameManager.s_instance.nextLevel = "LevelTwo";
                GameManager.s_instance.changeGameSate(GameState.ChangeLevel);
                break;
            default:
                break;
        }
    }

    public float getTime() {
        return time;
    }

    public void setPlatformSpawnArea(int t_spawnArea)
    {
        platformSpawnSection = t_spawnArea;
    }

    public void setSpawnerSection(int t_section)
    {
        platformSpawnSection = t_section;
    }

    public int getSpawnSection() {
        return platformSpawnSection;
    }
}

public enum LevelState {
    None,
    Continue,
    LevelFinished,
    GameOver
}
