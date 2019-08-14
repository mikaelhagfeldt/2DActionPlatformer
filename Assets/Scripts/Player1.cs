using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player1 : MonoBehaviour, Interface_CanBeDamaged
{
    // Gain Access To Player1 Rigidbody 2D, Declaration
    private Rigidbody2D field_rigidbody2d;

    // float value to indicate direction, -1 for left, 1 for right
    private float field_float_horizontalMovement;

    // Variable for jump force
    private float field_float_jumpForce = 6.1f;

    // Variable for checking if player1 is grounded or not
    [SerializeField] private bool field_bool_isGrounded = false;

    // Variable for storing our specific ground layer
    [SerializeField] private LayerMask field_layerMask_groundLayer;

    [SerializeField] private bool field_bool_resetForJumpIsNeeded = false;

    [SerializeField] private float field_float_secondsforJumpResetCoRoutine = 1.0f;

    [SerializeField] private float field_float_playerSpeed = 1.5f;

    private Player1Animations field_player1Animations;

    private SpriteRenderer field_spriteRenderer;

    public int property_int_hitpoints { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        // Assign Player1 Rigidbody 2D
        field_rigidbody2d = GetComponent<Rigidbody2D>();

        // Assign Player1Animations
        field_player1Animations = GetComponent<Player1Animations>();

        // Assign Sprite Renderer to access Flip X/Y

        field_spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        property_int_hitpoints = 7;

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (property_int_hitpoints < 1)
        {
            field_float_playerSpeed = 0;
            field_rigidbody2d.gravityScale = 0;
            return;
        }
        */


        /*
         * Player1 Movement
         */

        // Horizontal Input : Left And Right
        // CrossPlatformInputManager.GetAxis("Horizontal") or Input.GetAxisRaw("Horizontal");
        field_float_horizontalMovement = Input.GetAxisRaw("Horizontal");  //Input.GetAxisRaw("Horizontal");

        // Code for turning the player to left or right depending on Input.GetAxisRaw("Horizontal") value
        if (field_float_horizontalMovement > 0)
        {
            field_spriteRenderer.flipX = false;
        }
        else if (field_float_horizontalMovement < 0)
        {
            field_spriteRenderer.flipX = true;
        }

        // If we press Space and we are grounded, then Player1 can jump
        // previously was Input.GetKeyDown(KeyCode.Space)
        if (Input.GetKeyDown(KeyCode.Space) && field_bool_isGrounded == true)
        {
            field_rigidbody2d.velocity = new Vector2(field_rigidbody2d.velocity.x, field_float_jumpForce);
            field_bool_isGrounded = false;

            // Triggering the Jump Animation
            field_player1Animations.PlayerJump(true);

            // Adding a Cooldown before Jump Ability gets activated again
            field_bool_resetForJumpIsNeeded = true;
            StartCoroutine(ResetJumpCoRoutine());
        }


        // Updating the vector, making our Player1 move on the screen
        field_rigidbody2d.velocity = new Vector2(field_float_horizontalMovement * field_float_playerSpeed, field_rigidbody2d.velocity.y);

        // Triggering the Run Animation
        field_player1Animations.MovePlayer(field_float_horizontalMovement);

        /*
         * Using RayCast in order to detect collision.
         */

        // Using Raycast2D, we can detect when Player1 "hits" a surface (another Rigidbody)
        // We cast the ray straight down
        // DrawRay is used to gain a visual of the Raycast beam
        // field_layerMask_groundLayer.value holds our ground layer value (which is 8)

        // IF PROBLEM!!! INCREASE THE RAY LENGTH!!!!!
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, field_layerMask_groundLayer.value);
        Debug.DrawRay(transform.position, Vector2.down * 0.7f, Color.cyan);

        if (raycastHit2D.collider != null)
        {
            //Debug.Log("raycastHit2D.collider.name: " + raycastHit2D.collider.name);
            if (field_bool_resetForJumpIsNeeded == false)
            {
                field_bool_isGrounded = true;
                field_player1Animations.PlayerJump(false);
            }
        }

        // previously was Input.GetMouseButtonDown(0)
        // CrossPlatformInputManager.GetButtonDown("Attack")
        if (Input.GetMouseButtonDown(0) && field_bool_isGrounded == true)
        {
            field_player1Animations.Attack();
        }
    }

    // A CoRoutine to implement a CoolDown before Player1 can be able to jump again
    IEnumerator ResetJumpCoRoutine()
    {
        yield return new WaitForSeconds(field_float_secondsforJumpResetCoRoutine);
        field_bool_resetForJumpIsNeeded = false;
    }

    public void Damage()
    {
        Debug.Log("Player1.Damage() called");
        
        /*
        if (property_int_hitpoints < 1)
        {
            return;
        }
        */

        property_int_hitpoints += -1;
        Debug.Log("Player Health :: " + property_int_hitpoints);

        if (property_int_hitpoints < 1)
        {
            field_float_playerSpeed = 0;
            field_rigidbody2d.gravityScale = 20;
            field_player1Animations.Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "deathtrap")
        {
            //property_int_hitpoints = 0;
            //field_player1Animations.Death();
            //StartCoroutine(CoolDownBeforeGameOverScreen());

            field_float_playerSpeed = 0;
            field_rigidbody2d.gravityScale = 20;
            field_player1Animations.Death();
        }
    }

    IEnumerator CoolDownBeforeGameOverScreen()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("CoolDownBeforeGameOverScreen called");
    }
}
