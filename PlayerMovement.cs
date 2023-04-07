using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpSpeed;
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private LayerMask groundLayer;
    public Transform groundCheck;
    public Vector3 respawnPoint;
    public Vector3 spawnPoint;
    private bool playerGrounded;
    public float playerSpeed = 0;
    private Rigidbody2D body;
    private CapsuleCollider2D capsuleCollider;
    public Animator animator;
    public GameObject FinishMenu;
    private PlayerInput playerInput;
    public AudioClip Jump;
    public SliderValues SliderFloats;
    public CompleteText completeText;


    // Initiates the body and capsule collider
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        playerInput = GetComponent<PlayerInput>();

        // Default spawn point is set to player position when the game starts 
        spawnPoint = transform.position;
    }

    // updates each frame
    private void Update()
    {
        //changes the speed parameter for animations to the mangitude of the x velocity of the player 
        animator.SetFloat("Speed", Mathf.Abs(body.velocity.x));

        // Ground check for seeing if the player can jump
        if (IsGrounded() && playerInput.actions["Jump"].triggered)
        {
            AudioSource.PlayClipAtPoint(Jump, body.position, SliderFloats.soundValue);
            body.velocity = new Vector2(body.velocity.x, Vector2.up.y * jumpSpeed);
            animator.SetBool("IsJumping", true);
        }

        // Plays the jumping animation if the player is falling
        if (body.velocity.y < -0.5f && !IsGrounded())
        {
            animator.SetBool("IsJumping", true);
        }
    }

    void FixedUpdate()
    {
        float move = playerInput.actions["Move"].ReadValue<float>();

        // Makes the player face the direction they are moving in
        if (move > 0f)
        {
            if (playerSpeed <= 330)
            {
                playerSpeed += 30f;
            }
            else
            {
                playerSpeed = 330;
            }
            transform.localScale = new Vector2(-2.5f, transform.localScale.y);
        }
        else if (move < 0f)
        {
            if (playerSpeed >= -330)
            {
                playerSpeed -= 30f;
            }
            else
            {
                playerSpeed = -330;
            }
            transform.localScale = new Vector2(2.5f, transform.localScale.y);
        }
        else
        {
            if (playerSpeed < 0)
            {
                playerSpeed += 30f;
            }
            else if (playerSpeed > 0)
            {
                playerSpeed -= 30f;
            }

        }

        // updates the x velocity of the body to match the players'
        body.velocity = new Vector2(playerSpeed * Time.fixedDeltaTime, body.velocity.y);

        // a separate ground check using a collider that controls the jumping animation
        bool oldGrounded = playerGrounded;
        playerGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, groundLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                playerGrounded = true;
                if (!oldGrounded)
                {
                    animator.SetBool("IsJumping", false);
                }
            }
        }
    }

    // primary ground check using a raycast
    private bool IsGrounded()
    {
        float extraHeightTest = 0.2f;
        RaycastHit2D raycasthit = Physics2D.Raycast(capsuleCollider.bounds.center, Vector2.down, capsuleCollider.bounds.extents.y + extraHeightTest, platformLayerMask);
        Color rayColour;
        if (raycasthit.collider != null)
        {
            rayColour = Color.green;
        }
        else
        {
            rayColour = Color.red;
        }
        Debug.DrawRay(capsuleCollider.bounds.center, Vector2.down * (capsuleCollider.bounds.extents.y + extraHeightTest), rayColour);
        return raycasthit.collider != null;
        
    }

    // checks if a player has collided with a checkpoint or the end point 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            respawnPoint = collision.transform.position;
        }
        if (collision.tag == "Finish")
        {
            // sets the complete bool value for the level to true
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                completeText.levelTexts[0] = true;
            }
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                completeText.levelTexts[1] = true;
            }
            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                completeText.levelTexts[2] = true;
            }
            // pauses time and brings up the finished menu 
            Time.timeScale = 0f;
            FinishMenu.SetActive(true);
        }
    }
}
