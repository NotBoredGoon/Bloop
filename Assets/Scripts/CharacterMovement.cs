using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameData;



public class CharacterMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    private int doubleJumps = 0;
    private AudioSource audioSource;

    [SerializeField] private Rigidbody2D rb;            
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private Transform wallCheckTopLeft;
    [SerializeField] private Transform wallCheckBottomRight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask doubleJumpLayer;
    [SerializeField] private LayerMask voidLayer;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip easterEggCollectedSound;



    void Start()
    {
        Debug.Log(GameData.GetGamePaused());
        audioSource = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");


        if (Input.GetButtonDown("Jump") && (IsGrounded() || IsWalled()))
        {
            Jump();
        }
        else if (Input.GetButtonDown("Jump") && doubleJumps > 0)
        {
            doubleJumps--;
            Jump();
        }

        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0f)         
        {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }





        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Jump()
    {
        if (!IsGrounded() && IsWalled())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower * 1.2f);
            audioSource.PlayOneShot(jumpSound, UserSettings.GetJumpSoundVolume());
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            audioSource.PlayOneShot(jumpSound, UserSettings.GetJumpSoundVolume());
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapPoint(groundCheckLeft.position, groundLayer) || Physics2D.OverlapPoint(groundCheckRight.position, groundLayer);

    }

    private bool IsWalled()
    {

        return Physics2D.OverlapArea(wallCheckTopLeft.position, wallCheckBottomRight.position, groundLayer);

    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("DoubleJump"))
        {
            doubleJumps++;
            Destroy(col.collider.gameObject);
            Debug.Log("Double Jumps: " + doubleJumps);
        }
        else if (col.collider.gameObject.layer == LayerMask.NameToLayer("LevelPortal"))
        {
            GameObject.Find("Game").GetComponent<GameUtil>().NextLevel();

        }
        else if (col.collider.gameObject.layer == LayerMask.NameToLayer("void"))
        {
            GameObject.Find("Game").GetComponent<GameUtil>().RestartLevel();
        }

    }

}
