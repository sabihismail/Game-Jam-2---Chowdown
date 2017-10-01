using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meatball : MonoBehaviour {
    private Vector3 point;
    
    private float speed;

	void Start ()
    {
        point = GameObject.Find("Earth").transform.position;

        speed = Random.Range(3f, 5f);
	}
	
	void Update ()
    {
        if (Data.gameOver)
        {
            return;
        }

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(this.transform.position, point, step);
    }
}
