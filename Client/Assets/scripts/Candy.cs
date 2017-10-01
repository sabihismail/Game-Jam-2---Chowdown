using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour {
    private float speed = 10f;

    private float width;
    private float height;
    private Vector3 direction;

    private string url = "https://us-central1-game-jam-fall-2017.cloudfunctions.net/i?q=" + Data.id + "&u=" + Data.player + "&i=";

    void Start () {
        Vector2 bounds = this.GetComponent<Collider2D>().bounds.size;

        width = bounds.x;
        height = bounds.y;
    }
	
	void Update ()
    {
        if (Data.gameOver)
        {
            return;
        }

        transform.position += direction * Time.deltaTime * speed;

        CheckInside();
    }

    void CheckInside ()
    {
        Vector3 pos = this.transform.position;

        bool outside = pos.x + width > CameraScript.maxX || pos.x - width < CameraScript.minX || pos.y + height > CameraScript.maxY || pos.y - height < CameraScript.minY;

        if (outside)
        {
            string xy = direction.x + "," + direction.y;
            StartCoroutine(SendData(url + xy));

            Destroy(this.gameObject);
        }
    }

    IEnumerator SendData(string fullURL)
    {
        WWW www = new WWW(fullURL);
        yield return www;
    }

    public void Position (Vector3 position)
    {
        this.transform.position = position;
    }

    public void Direction (Vector3 direction)
    {
        this.direction = direction;
    }

    public void Angle(Vector3 angle)
    {
        this.transform.eulerAngles = angle;
    }
}
