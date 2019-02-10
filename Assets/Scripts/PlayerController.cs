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
    public float Hold;
   

    private bool isRushing;
    private bool isMoving;
    private bool isAtacking;
    private bool IsControlling;
    private bool isVelocityWritten;
    private bool HasKey;

    private float velocity_marker;
    private float jumpTimeCounter;
    private float elapsedTime;

    private Vector2 velocity;
    private Vector3 t;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        IsControlling = true;
        t = gameObject.transform.position;
    }
    private void Update()
    {
        velocity = rb.velocity;
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Rush", isRushing);
        anim.SetBool("Move", isMoving);
        anim.SetBool("Attack", isAtacking);

        if(IsControlling == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                gameObject.transform.position = t;
            }
            //jump       
            if (grounded && Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                elapsedTime = 0;
                isVelocityWritten = false;
                rb.AddForce(Vector2.up * jumpforce);
            }
            if (Input.GetButton("Jump") && isJumping == true)
            {
                if (jumpTimeCounter > 0)
                {
                    if (elapsedTime > Hold)
                    {
                        if (isVelocityWritten == false)
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
            if (Input.GetButtonUp("Jump"))
            {
                isJumping = false;
                elapsedTime = 0;
            }

            //attack
            //if (Input.GetButtonDown("Attack"))
            //{
            //    isAtacking = true;
            //}
            //else
            //{
            //    isAtacking = false;
            //}

            if (Input.GetButtonDown("Interact"))
            {
                int m = LayerMask.GetMask("Moveable");
                // Cast a ray straight down.
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, Mathf.Infinity, layerMask: m);

                // If it hits something...
                if (hit.collider != null)
                {
                    Debug.Log("hit");
                    hit.collider.gameObject.GetComponent<SimpleController>().SetControlling();
                    IsControlling = false;
                    //// Calculate the distance from the surface and the "error" relative
                    //// to the floating height.
                    //float distance = Mathf.Abs(hit.point.y - transform.position.y);
                    //float heightError = floatHeight - distance;

                    //// The force is proportional to the height error, but we remove a part of it
                    //// according to the object's speed.
                    //float force = liftForce * heightError - rb2D.velocity.y * damping;

                    //// Apply the force to the rigidbody.
                    //rb2D.AddForce(Vector3.up * force);
                }
            }
        }
        
    }
    void FixedUpdate()
    {
        characterLocation = rb.transform;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if(IsControlling == true)
        {
            //control rotation
            if (moveHorizontal < 0)
                gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
            else if (moveHorizontal > 0)
                gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);

            Vector2 movement = new Vector2(moveHorizontal, 0);

            //move 
            if (Mathf.Abs(moveHorizontal) == 1)
            {
                isRushing = true;
                isMoving = false;
                rb.AddForce(movement * rushForce);
            }
            else if (Mathf.Abs(moveHorizontal) == 0)
            {
                isMoving = false;
                isRushing = false;
            }
            else
            {
                isRushing = false;
                isMoving = true;
                rb.AddForce(movement * force);
            }
            if (rb.velocity.x > maxSpeed)
            {
                rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            }
            if (rb.velocity.x < -maxSpeed)
            {
                rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
            }
        }
    
    }

    public Vector2 GetVelocity()
    {
        return velocity;
    }
    public void SetControlling()
    {
        IsControlling = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Key")
        {
            Debug.Log("Get Key");
            HasKey = true;
            collision.gameObject.SetActive(false);
        }
        if(collision.name == "Door")
        {
            if(HasKey == true)
            {
                Vector3 pos = GameObject.Find("Point").transform.position;
                collision.gameObject.transform.RotateAround(pos, new Vector3(0, 0, 1), 90);
            }
        }
    }
}