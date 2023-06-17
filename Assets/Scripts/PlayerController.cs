using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private
    Animator animator;
    Rigidbody rgbd;
    bool isGrounded;
    float yInput;
    float xInput;
    #endregion

    #region Public
    public LayerMask whatIsGround;

    #endregion

    #region Serialized Field
    [SerializeField] Transform footPosition;
    [SerializeField] float footRadius, jumpForce, speed;
    #endregion

    private void Start() {
        animator = GetComponent<Animator>();
        rgbd = GetComponent<Rigidbody>();
        isGrounded = false;
    }

    private void FixedUpdate() {
        
        if (PlayerManager.instance.GetState() != PlayerState.Dead) {
            isGrounded = Physics.CheckSphere(footPosition.position, footRadius, whatIsGround); //&& rgbd.velocity.y < 0.01f;
            moveAround();
            verticalMovement();
            return;
        }
        rgbd.velocity = Vector2.zero;
    }

    private void Update() {
        if (Input.GetButtonDown("Jump")) {
            jump();
            //Debug.Log("IS grounded: " + isGrounded);
        }
        //Debug.Log("IS grounded: " + isGrounded);
    }

    void moveAround() {
        
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        PlayerManager.instance.getAnimator().SetFloat("xMove", xInput);
        PlayerManager.instance.getAnimator().SetFloat("yMove", yInput);

        rgbd.velocity = new Vector3(xInput * speed, rgbd.velocity.y, yInput * speed);

        if (isGrounded) {
            if (xInput != 0 || yInput != 0) {
                PlayerManager.instance.ChangePlayerState(PlayerState.Running);
            } else if (xInput == 0 && yInput == 0) {
                PlayerManager.instance.ChangePlayerState(PlayerState.Idle);
            }
        }
    }

    void verticalMovement() {
        if (isGrounded) {
            return;
        }
        if (rgbd.velocity.y >= 0.1) {
            PlayerManager.instance.ChangePlayerState(PlayerState.Jumping);
        } else if (rgbd.velocity.y < 0.1) {
            PlayerManager.instance.ChangePlayerState(PlayerState.JumpFall);
        }
    }

    void jump() {
        if(!isGrounded) {
            return;
        }
        //Debug.Log("jUMPING");
        rgbd.velocity = new Vector3(rgbd.velocity.x, jumpForce, rgbd.velocity.z);
        //Debug.Log("vector 3: " + rgbd.velocity  );
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(footPosition.position, footRadius);
    }
}
