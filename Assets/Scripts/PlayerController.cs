using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float force = 4.1f;
    [SerializeField] private float rushForce = 6f;
    [SerializeField] private float jumpforce = 1f;
    [SerializeField] private float maxSpeed = 0.7f;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform characterLocation;
    public bool grounded;

    public bool isJumping;
    public float jumpTime;
    private float jumpTimeCounter;
    private float elapsedTime;
    public float Hold;
    private float velocity_marker;
    private bool isVelocityWritten;

    private bool isRushing;
    private bool isMoving;
    private bool isAtacking;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Rush", isRushing);
        anim.SetBool("Move", isMoving);
        anim.SetBool("Attack", isAtacking);
        
    }
    void FixedUpdate()
    {
        characterLocation = rb.transform;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //control rotation
        if (moveHorizontal < 0)
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        else if(moveHorizontal > 0)
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);

        Vector2 movement = new Vector2(moveHorizontal, 0);

        //move
        if(Input.GetKey(KeyCode.LeftShift))
        {
            isRushing = true;
            rb.AddForce(movement * rushForce);
        }
        else
        {
            isRushing = false;
            rb.AddForce(movement * force);
        }

        if (moveHorizontal != 0)
        {
            isMoving = true;
        }
        else
            isMoving = false;
        
        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }
        if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }

        //jump
        if (grounded && Input.GetKeyDown(KeyCode.W))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            elapsedTime = 0;
            isVelocityWritten = false;
            rb.AddForce(Vector2.up * jumpforce);
        }
        if (Input.GetKey(KeyCode.W) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                if (elapsedTime > Hold)
                {
                    if(isVelocityWritten == false)
                    {
                        velocity_marker = rb.velocity.y;
                        isVelocityWritten = true;
                    }
                    rb.velocity = new Vector2(rb.velocity.x, velocity_marker);
                }
                    
                jumpTimeCounter -= Time.deltaTime;
                elapsedTime += Time.deltaTime;
            }
            else
            {
                isJumping = false;
                elapsedTime = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
            elapsedTime = 0;
        }

        //attack
        if(Input.GetKey(KeyCode.G))
        {
            isAtacking = true;
        }
        else
        {
            isAtacking = false;
        }
    }

}