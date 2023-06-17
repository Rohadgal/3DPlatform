using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    public static PlayerManager instance;
    PlayerState playerState;

    private void Awake() {
        instance = this;
        playerState = PlayerState.None;
    }
    void Start()
    { 
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (playerState == PlayerState.Dead) {
            //evelManager.s_instance.changeLevelState(LevelState.GameOver);
        }
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