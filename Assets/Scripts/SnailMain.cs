using Mono.Cecil.Cil;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;


public class SnailMainScript : MonoBehaviour
{

    public float jumpforce;
    public float gravityScale;

    public int speed;
    public float acceleration;
    public float deceleration;
    public PlayerInput playerInput;

    private InputActionMap actionMap;
    private InputAction Move;
    private InputAction Jump;
    private InputAction DashI;
    public Rigidbody2D snailRigid;

    private Vector2 currentVelocity;

    public SpriteRenderer spriteRenderer;

    private GameManager gameManager;

    public bool isGrounded;
    private int howManyJumpsLeft;


    //dashing
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;
    [SerializeField] private TrailRenderer trailRenderer;
    private int directionFacing;


   

    //public float groundCheckDistance;
    //LayerMask layerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //called as soon as object/script appears
    private void Awake()
    {
        actionMap = playerInput.currentActionMap;
        Move = actionMap.FindAction("Move");
        Jump = actionMap.FindAction("Jump");
        DashI = actionMap.FindAction("Sprint");
          
        gameManager = GameManager.instance;
        

        //layerMask = LayerMask.GetMask("Player");



    }

    //right after awake or whenever the script is enabled
    private void OnEnable()
    {
        actionMap.Enable();
    }
    //when the script is disabled or destroyed by paddyt wallce
    private void OnDisable()
    {
        actionMap.Disable();
    }

    // Update is called once per frame
    void Update()
    {

        if (isDashing)
        {
            return; //prevents player from doing stuff while dashing i think
        }
        Vector2 input = Move.ReadValue<Vector2>();
        Vector2 targetVelocity = new Vector2(input.x * speed, snailRigid.linearVelocity.y);


        if (input.magnitude > 0)
        {

            snailRigid.linearVelocity = Vector2.MoveTowards(snailRigid.linearVelocity, targetVelocity, acceleration * Time.deltaTime);
        }
        else
        {

            snailRigid.linearVelocity = Vector2.MoveTowards(snailRigid.linearVelocity, new Vector2(0, snailRigid.linearVelocity.y), deceleration * Time.deltaTime);
        }


        //handle jump
        if (isGrounded && Jump.triggered)
        {
            howManyJumpsLeft -= 1;
            snailRigid.linearVelocity = new Vector2(snailRigid.linearVelocity.x, 0); //need to reset vel before jump again
            snailRigid.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
        }
        if (!isGrounded && Jump.triggered && howManyJumpsLeft > 0)
        {
            howManyJumpsLeft -= 1;
            snailRigid.linearVelocity = new Vector2(snailRigid.linearVelocity.x, 0);
            snailRigid.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
        }

        if (isGrounded)
        {
            howManyJumpsLeft = 1;
        }

        if (input.x > 0)
        {
            directionFacing = 1;
            spriteRenderer.flipX = false;
        }
        else if (input.x < 0)
        {
            directionFacing = 0;
            spriteRenderer.flipX = true;
        }

        if (DashI.triggered && canDash)
        {
            StartCoroutine(Dash());
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            snailRigid.gravityScale -= 2;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            snailRigid.gravityScale += 2;
        }
    }


    /*private bool fuckmatt()
    {
        // Check for line of sight with the player
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 2f);
        if (ray.collider != null)
        {
            bool lineOfSight = ray.collider.CompareTag("Ground");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * 1f, lineOfSight ? Color.green : Color.red);
            Debug.Log("true");
            return true;
        }
        else
        {
            Debug.Log("falso");
            return false;
        }

    }
    */

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = snailRigid.gravityScale; // Store the original gravity
        snailRigid.gravityScale = 0f; // Disable gravity during dash

        if (directionFacing == 0)
        {
            snailRigid.linearVelocity = new Vector2((transform.localScale.x * dashingPower * -1), 0f);
        }
        else
        {
            snailRigid.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        }

        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;

        snailRigid.gravityScale = originalGravity; // Reset gravity to original value
        snailRigid.linearVelocity = new Vector2(snailRigid.linearVelocity.x, 0f);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

}




