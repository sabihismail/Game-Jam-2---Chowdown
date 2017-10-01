using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthProtector : MonoBehaviour
{
    private const KeyCode RIGHT = KeyCode.RightArrow;
    private const KeyCode LEFT = KeyCode.LeftArrow;
    private const KeyCode FIRE = KeyCode.Space;

    private BulletManager bulletManager;

    public float SPEED = 5f;
    public float RADIUS = 0.9f;

    private Vector3 center;
    private float angle;

    private GameObject energy;
    private Sprite[] sprites;
    
    void Start ()
    {
        center = GameObject.Find("Earth").GetComponent<Transform>().position;

        energy = GameObject.Find("Energy");
        sprites = Resources.LoadAll<Sprite>("sprites/energy");

        bulletManager = new BulletManager();
    }

	void Update ()
    {
        if (Data.gameOver)
        {
            return;
        }

        if (Input.GetKey(RIGHT))
        {
            angle += SPEED * Time.deltaTime;
        }
        else if (Input.GetKey(LEFT))
        {
            angle -= SPEED * Time.deltaTime;
        }
        else if (Input.GetKey(FIRE))
        {
            bulletManager.Shoot();

            try
            {
                energy.GetComponent<SpriteRenderer>().sprite = sprites[bulletManager.MAX_SCORE - bulletManager.score];
            }
            catch
            {
                energy.GetComponent<SpriteRenderer>().sprite = null;
            }
        }

        CalculateAngle();
    }
    
    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.name.Contains("Meatball"))
        {
            Destroy(col.gameObject);

            bulletManager.Increase();

            energy.GetComponent<SpriteRenderer>().sprite = sprites[bulletManager.MAX_SCORE - bulletManager.score];
        }
    }

    private void CalculateAngle ()
    {
        if (angle > Mathf.PI * 2)
        {
            angle -= Mathf.PI * 2;
        }
        else if (angle < 0)
        {
            angle += Mathf.PI * 2;
        }

        var offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), center.z) * RADIUS;
        this.transform.position = center + offset;

        float degrees = angle * Mathf.Rad2Deg;        
        this.transform.eulerAngles = new Vector3(0, 0, -degrees);
    }
}
