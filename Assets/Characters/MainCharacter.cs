using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacter : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private InputActionReference horizontalAxis;


    [SerializeField]
    private InputActionReference jumpAxis;

    [SerializeField]
    private float moveForce;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float jumpGravityScale = 1f;

    [SerializeField]
    private float fallGravityScale = 1f;

    [SerializeField]
    private float maxSpeed = 1f;

    [SerializeField]
    private float movementRampDownRate = 0.1f;

    //Used to ramp down the speed when no input is occurring
    private float dynamicMaxSpeed = 0f;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Transform animationSprite;

    private bool lookingRight = true;


    public static MainCharacter instance;

    // Start is called before the first frame update
    void Start()
    {
        horizontalAxis.action.Enable();
        jumpAxis.action.Enable();
        instance = this;
    }

    private void OnEnable()
    {
        jumpAxis.action.performed += OnJump;

    }

    private void OnDisable()
    {
        jumpAxis.action.performed -= OnJump;

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Grounded", IsGrounded());
    } 

    private void FixedUpdate()
    {
        float horizontalAxisValue = horizontalAxis.action.ReadValue<float>();
        Debug.Log(horizontalAxisValue);
        rb.AddForce(Vector2.right * moveForce * horizontalAxisValue);

        if (Mathf.Abs(horizontalAxisValue) > 0.01f)//then set max speed to max
            dynamicMaxSpeed = maxSpeed;
        else
            dynamicMaxSpeed = Mathf.MoveTowards(dynamicMaxSpeed, 0f, movementRampDownRate);//then ramp down the max speed

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -dynamicMaxSpeed, dynamicMaxSpeed), rb.velocity.y);
        animator.SetFloat("RunSpeed", Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(rb.velocity.x)));

        //Flipping look direction if needed
        if ((lookingRight && rb.velocity.x < 0) || (!lookingRight && rb.velocity.x > 0)) 
            lookingRight = !lookingRight;

        animationSprite.localScale = new Vector3(lookingRight ? 1f : -1f, 1f);

        //Setting gravity scale
        rb.gravityScale = rb.velocity.y < 0 ? fallGravityScale : jumpGravityScale;
        
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        if(IsGrounded())
            rb.AddForce(Vector2.up * jumpForce);
    }
    private bool IsGrounded()
    {
        
        RaycastHit2D hit = Physics2D.CircleCast(this.transform.position, 0.15f, Vector2.down, 1.32f, ~LayerMask.GetMask("Character"));
        return hit.collider != null;
    }
}
