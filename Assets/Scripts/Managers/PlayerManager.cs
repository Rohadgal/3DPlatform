using System.Collections;
using Cinemachine;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    Animator animator;
    PlayerState playerState;
    public GameObject mainCamera;
    public Transform spawnPoint;
    public Transform playerShouders;

    private void Awake() {
        animator = GetComponent<Animator>();
       if (FindObjectOfType<PlayerManager>() != null &&
            FindObjectOfType<PlayerManager>().gameObject != gameObject)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
    void Start()
    {
        playerState = PlayerState.None;
    }

    private void Update() {
        if (playerState == PlayerState.Dead) {
            LevelManager.s_instance.changeLevelState(LevelState.GameOver);
        }
        Debug.Log("State: " + playerState);
    }
    public void ChangePlayerState(PlayerState newState) {
        if (playerState == newState) {
            return;
        }
        resetAnimatorParameters();

        playerState = newState;
        switch (playerState) {
            case PlayerState.None:
                break;
            case PlayerState.Idle:
                animator.SetBool("isIdle", true);
                break;
            case PlayerState.Running:
                animator.SetBool("isRunning", true);
                break;
            case PlayerState.Jumping:
                animator.SetBool("isJumping", true);
                break;
            case PlayerState.JumpFall:
                animator.SetBool("isFalling", true);
                break;
            case PlayerState.FreeFall:
                animator.SetBool("isFalling", true);
                break;
            case PlayerState.Dead:
                animator.SetBool("isDead", true);
                break;
            default: break;
        }
    }
    private void resetAnimatorParameters() {
        foreach (AnimatorControllerParameter parameter in animator.parameters) {
            if (parameter.type == AnimatorControllerParameterType.Bool) {
                animator.SetBool(parameter.name, false);
            }
        }
    }
    public PlayerState GetState() { return playerState; } 

    public Animator getAnimator() { return animator; }

    private bool IsDead() {
        if (PlayerManager.instance.GetState() != PlayerState.Dead) {
            return false;
        }
        //Debug.LogWarning("You died");
        StartCoroutine(DestroyPlayer());
        return true;
    }

    IEnumerator DestroyPlayer() {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
    public void respawn()
    {
        gameObject.transform.position = spawnPoint.position;
    }

    public void assignCamera()
    {
        mainCamera.GetComponent<CinemachineVirtualCamera>().Follow = transform;
    }
}

public enum PlayerState {
    None,
    Idle,
    Running,
    Jumping,
    JumpFall,
    FreeFall,
    Dead
}
