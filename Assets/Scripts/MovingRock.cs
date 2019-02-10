using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRock : MonoBehaviour {
    int direction;
	// Use this for initialization
	void Start () {
        direction = 1;

    }
	
	// Update is called once per frame
	void Update () {
        if(direction == 1)
            gameObject.transform.Translate(Vector3.right * Time.deltaTime * 3);
        else
            gameObject.transform.Translate(Vector3.left * Time.deltaTime * 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction = (++direction) % 2;
    }
}
