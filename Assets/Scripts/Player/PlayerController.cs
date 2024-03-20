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

        OnSpawn?.Invoke(gameObject, PV.Owner);
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
        // Rotate the player based on mouse input.
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        // Adjust the vertical camera rotation and clamp it.
        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -10f, 10f);

        // Apply the vertical camera rotation to the camera holder.
        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    [PunRPC]
    void Move()
    {
        // Calculate the player's movement direction based on input.
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        // Apply sprint or walk speed based on left shift key.
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);

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

        // Move the Rigidbody's position based on the current player's input (moveAmount) and time step.
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
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
        OnDeath?.Invoke(gameObject, PV.Owner);
    }
}
