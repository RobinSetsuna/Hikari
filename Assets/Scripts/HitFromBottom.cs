using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFromBottom : MonoBehaviour {
    public float offset;
    public float duration;
    private bool isBumped;

    private void Start()
    {
        isBumped = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Vector2 v = collision.GetComponent<PlayerController>().GetVelocity();
            if (v.y > 0)
            {
                if (isBumped == false)
                {
                    StartCoroutine(MoveToPosition(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + offset, 0), duration));
                    isBumped = true;
                }

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
