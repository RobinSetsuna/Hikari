using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleController : MonoBehaviour {
    public float speedFactor;

    private bool IsControlling;
    private GameObject myLine;
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
                gameObject.transform.Translate(Vector3.right * Time.deltaTime * speedFactor);
                MoveLine();
            }
            else if (h < 0)
            {
                gameObject.transform.Translate(Vector3.left * Time.deltaTime * speedFactor);
                MoveLine();
            }
            if(Input.GetButton("Cancel"))
            {
                IsControlling = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SetControlling();
                GameObject.Destroy(myLine);
            }
        }
	}

    public void SetControlling()
    {
        IsControlling = true;
        float y_offset = gameObject.GetComponent<HitFromBottom>().offset;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + y_offset, 0);
        DrawLine(transform.position, pos, new Color(102, 51, 0, 0.7f));

    }
    void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.sortingLayerName = "Foreground";
    }

    void MoveLine()
    {
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        float y_offset = gameObject.GetComponent<HitFromBottom>().offset;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + y_offset, 0);
        lr.SetPosition(0, gameObject.transform.position);
        lr.SetPosition(1, pos);
    }
}
