using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFromBottom : MonoBehaviour {
    public float offset;
    public float duration;
    private bool hitonce;
	// Use this for initialization
	void Start () {
        hitonce = false;
    }
	
	// Update is called once per frame
	void Update () {
     
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hitonce == false)
        {
            if (collision.transform.tag == "MovingObject")
            {
                StartCoroutine(MoveToPosition(new Vector3(gameObject.transform.position.x + 1.8f, gameObject.transform.position.y, 0), duration));
                hitonce = true;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Vector2 v = collision.GetComponent<PlayerController>().GetVelocity();
            if (v.y > 0)
            {
                StartCoroutine(MoveToPosition(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + offset, 0), duration));
            }
        }
           
    }

    IEnumerator MoveToPosition(Vector3 newPosition, float time)
    {
        float elapsedTime= 0;
        Vector3 startingPos = transform.position;
        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, newPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame(); 
        }
        yield return null;
    }
}
