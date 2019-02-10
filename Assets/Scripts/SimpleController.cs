using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleController : MonoBehaviour {
    private bool IsControlling;
	// Use this for initialization
	void Start () {
        IsControlling = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(IsControlling == true)
        {
            float h = Input.GetAxis("Horizontal");
            if (h > 0)
            {
                gameObject.transform.Translate(Vector3.right * Time.deltaTime);
            }
            else if (h < 0)
            {
                gameObject.transform.Translate(Vector3.left * Time.deltaTime);
            }
            if(Input.GetButton("Cancel"))
            {
                IsControlling = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SetControlling();
            }
        }
	}

    public void SetControlling()
    {
        IsControlling = true;
    }

}
