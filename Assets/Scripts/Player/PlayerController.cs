using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviourPun
{
    public static event Action<GameObject, Player> OnSpawn;
    public static event Action<GameObject, Player> OnDeath;
    [SerializeField] GameObject cameraHolder;  // Reference to the camera holder GameObject.
    [SerializeField] PlayerFriction friction;
    [SerializeField] PlayerGroundCheck groundCheck;
    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;  // Player movement and camera control parameters.
    
    // microman: Just a tip for future, don't leave comments like this. If something is confusing, explain it-
    // -but generally, variable names are to be self-explanatory. That's why they have names.
    
    public Animator playerAnimator;  // Animator for player animations.
    float verticalLookRotation;  // Vertical camera rotation value.
    [SerializeField] bool grounded;  // Indicates if the player is grounded.
    Vector3 smoothMoveVelocity;  // Velocity used for smoothing player movement.
    Vector3 moveAmount;  // Total movement amount.
    Rigidbody rb;  // Player's Rigidbody component.
    PhotonView PV;  // PhotonView component for network synchronization.

    void Awake()
    {
        rb = GetComponent<Rigidbody>();   // Get the player's Rigidbody component.
        PV = GetComponent<PhotonView>();  // Get the PhotonView component for network synchronization.

        OnSpawn?.Invoke(gameObject, PV.Controller);
    }

    void Start ()
    {
        if (PV.IsMine)
            return;

        // If this GameObject doesn't belong to the local player, destroy the camera and Rigidbody.
        Destroy(GetComponentInChildren<Camera>().gameObject);
        Destroy(rb);
    }

    void Update()
    {
        if (!PV.IsMine)
            return;

        Look();  // Handle camera look/rotation.
        Move();  // Handle player movement.
        Jump();  // Handle player jumping.
    }

    void Look()
    {
        // Calculate mouse delta
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // Rotate the player based on mouse input.
        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);

        // Adjust the vertical camera rotation and clamp it.
        verticalLookRotation += mouseDelta.y * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -10f, 10f);

        // Apply the vertical camera rotation to the camera holder.
        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    [PunRPC]
    void Move()
    {
        // Calculate the player's movement direction based on input.
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        // Apply faster speed if the player has Timer
        if (TimerManager.IsLocalPlayerTarget()) // Check if the local player is the current target
        {
            // Apply sprint speed if the player is the target
            moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * sprintSpeed, ref smoothMoveVelocity, smoothTime);
        }
        else
        {
            // Apply walk speed if the player is not the target
            moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * walkSpeed, ref smoothMoveVelocity, smoothTime);
        }

        // Determine if the player is walking.
        bool isWalking = moveDir.magnitude > 0;
        playerAnimator.SetBool("walk", isWalking);
    }

    void Jump()
    {
        // Check for jumping input and if the player is grounded.
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumpForce);  // Apply an upward force for jumping.
        }
    }

    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;  // Set the grounded state based on collision detection.
    }

    void FixedUpdate()
    {
        // Check if this is the locally controlled player's object (IsMine).
        if (!PV.IsMine)
            return;

        grounded = groundCheck.Grounded;

        // Move the Rigidbody's position based on the current player's input (moveAmount) and time step.
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

        if (grounded)
        {
            friction.Tick();
        }
    }

    public PhotonView GetPhotonView ()
    {
        return photonView;
    }

    public void Die ()
    {
        PV.RPC(nameof(RPC_Die), RpcTarget.All);

        PhotonNetwork.Destroy(gameObject);
        //SceneManager.LoadScene("Menu"); // OBVIOUSLY NOT RIGHT
    }

    [PunRPC]
    void RPC_Die ()
    {
        OnDeath?.Invoke(gameObject, PV.Controller);
    }

    public void SetAnimator (Animator animator)
    {
        playerAnimator = animator;
    }
}
