using UnityEngine;

public class ExitLevelThree : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManagerLevelThree.s_instance.changeLevelState(LevelState.LevelFinished);
            Debug.Log("LevelFinished");
        }
    }
}
