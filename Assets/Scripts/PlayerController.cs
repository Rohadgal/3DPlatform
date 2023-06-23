using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private
    Animator animator;
    Rigidbody rgbd;
    bool isGrounded;
    float verticalInput;
    float horizontalInput;
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
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        PlayerManager.instance.getAnimator().SetFloat("xMove", horizontalInput);
        PlayerManager.instance.getAnimator().SetFloat("yMove", verticalInput);

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        
        movement.Normalize();

        Vector3 moveInWorld = transform.TransformDirection(movement);
        Vector3 velocity = moveInWorld * speed;
        velocity.y = rgbd.velocity.y;
        rgbd.velocity = velocity;

        //rgbd.velocity = new Vector3(horizontalInput * speed, rgbd.velocity.y, verticalInput * speed);

        if (isGrounded) {
            if (horizontalInput != 0 || verticalInput != 0) {
                PlayerManager.instance.ChangePlayerState(PlayerState.Running);
            } else if (horizontalInput == 0 && verticalInput == 0) {
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
