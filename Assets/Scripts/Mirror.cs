using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour {
    public int id;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (Input.GetButtonDown("Attack"))
            {
                GameObject[] objects = GameObject.FindGameObjectsWithTag("Mirror");

                foreach (GameObject obj in objects)
                {
                    if (obj.GetComponent<Mirror>().id != this.id)
                    {
                        var p = obj.transform.position;
                        if (collision.transform.rotation.eulerAngles.y == 0)
                        {
                            collision.transform.position = new Vector3(p.x + 0.6f, p.y, 0);
                            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 250);
                        }
                        else
                        {
                            collision.transform.position = new Vector3(p.x - 0.6f, p.y, 0);
                            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 250);
                        }
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.name == "Moveable_Rock_3")
        {
            gameObject.transform.parent = collision.gameObject.transform;
        }
    }
}
