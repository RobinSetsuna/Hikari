using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour {
    public int id;
    private int direction;
	// Use this for initialization
	void Start () {
        direction = -1;
    }
	
	// Update is called once per frame
	void Update () {
        if (direction == 1)
            gameObject.transform.Translate(Vector3.right * Time.deltaTime * 4.25f);
        else if(direction == 0)
            gameObject.transform.Translate(Vector3.left * Time.deltaTime * 4.25f);
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

   
}
