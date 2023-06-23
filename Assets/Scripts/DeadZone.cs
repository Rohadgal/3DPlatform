using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            PlayerManager.instance.ChangePlayerState(PlayerState.Dead);
            GameManager.s_instance.changeGameSate(GameState.GameOver);
        }
    }
}
