using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRock : MonoBehaviour {
    public int direction;
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        direction = 1;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (direction == 1)
            rb.velocity = Vector2.right * 3f;
        else
            rb.velocity = Vector2.left * 3f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            
        }
        else if(collision.transform.name == "Mirror")
        {

        }
        else if(collision.transform.name == "HoldRock")
        {
            rb.velocity = Vector2.zero;
            direction = -1;
        }
        else
        {
            direction = (++direction) % 2;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

            if (collision.name == "Mirror")
            {
                Vector3 vel = gameObject.GetComponent<Rigidbody2D>().velocity;
                collision.transform.GetComponent<Rigidbody2D>().velocity = vel;
            }
 
    }
}
