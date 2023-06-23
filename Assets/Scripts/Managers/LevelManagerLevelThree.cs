using UnityEngine;

public class LevelManagerLevelThree : MonoBehaviour
{
    public static LevelManagerLevelThree s_instance;

    LevelState m_levelState;
    int platformSpawnSection = 0;
    private float time = 2.2f;
    [SerializeField] GameObject playerPrefab, spawnPointThree, mainCamera;

    private void Awake()
    {
        if (FindObjectOfType<LevelManagerLevelThree>() != null &&
            FindObjectOfType<LevelManagerLevelThree>().gameObject != gameObject)
        {
            Destroy(gameObject);
        }
        else
        {
            s_instance = this;
        }
        if (FindObjectOfType<PlayerManager>() == null)
        {
            Instantiate(playerPrefab);
        }

        m_levelState = LevelState.None;
        PlayerManager.instance.spawnPoint = spawnPointThree.transform;
        PlayerManager.instance.gameObject.transform.position = spawnPointThree.transform.position;
        PlayerManager.instance.mainCamera = mainCamera;
        PlayerManager.instance.assignCamera();
    }

    private void Start()
    {
        setPlatformSpawnArea(0);
        m_levelState = LevelState.Continue;
        GameManager.s_instance.changeGameSate(GameState.Playing);
        PlayerManager.instance.ChangePlayerState(PlayerState.Idle);
    }

    public void changeLevelState(LevelState state)
    {
        m_levelState = state;
        switch (m_levelState)
        {
            case LevelState.LevelFinished:
                GameManager.s_instance.changeGameSate(GameState.GameFinished);
                break;
            default:
                break;
        }
    }

    public float getTime()
    {
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

    public int getSpawnSection()
    {
        return platformSpawnSection;
    }
}
