using UnityEngine;

public class ExitLevelTwo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManagerLevelTwo.s_instance.changeLevelState(LevelState.LevelFinished);
            Debug.Log("LevelFinished");
        }
    }
}
