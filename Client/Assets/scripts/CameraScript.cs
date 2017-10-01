using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    private int MAX_RNG = 1000;
    private int CENTER = 990;

    public static float minX;
    public static float maxX;
    public static float minY;
    public static float maxY;

    void Start ()
    {
        Data.changeSprites();

        GameObject.Find("Protector").GetComponent<SpriteRenderer>().sprite = Data.spriteProtector;
        GameObject.Find("Earth").GetComponent<SpriteRenderer>().sprite = Data.spriteEarth;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("CollisionCandy"), LayerMask.NameToLayer("CollisionCandy"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("CollisionCandy"), LayerMask.NameToLayer("CollisionMeatball"));

        float vert = Camera.main.orthographicSize;
        float hor = vert * Screen.width / Screen.height;

        minX = -hor;
        maxX = hor;
        minY = -vert;
        maxY = vert;
    }
	
	void Update ()
    {
        if (Data.gameOver)
        {
            return;
        }

        int r = Random.Range(0, MAX_RNG);

        if (r > CENTER)
        {
            SpawnNewMeatball();
        }
	}

    public void SpawnNewMeatball()
    {
        GameObject meatball = (GameObject) Instantiate(Resources.Load("prefabs/Meatball"));
        Vector2 bounds = meatball.GetComponent<Collider2D>().bounds.size;

        float width = bounds.x;
        float height = bounds.y;
       
        float x;
        float y;
        if (Random.Range(0, 2) == 0)
        {
            x = Random.Range(minX, maxX);
            if (x > 0)
            {
                x += width;
            }
            else
            {
                x -= width;
            }
            
            if (Random.Range(0, 2) == 0)
            {
                y = minY - height;
            }
            else
            {
                y = maxY + height;
            }
        }
        else
        {
            if (Random.Range(0, 2) == 0)
            {
                x = minX - width;
            }
            else
            {
                x = maxX + width;
            }

            y = Random.Range(minY, maxY);
            if (y > 0)
            {
                y += height;
            }
            else
            {
                y -= height;
            }
        }

        meatball.transform.position = new Vector3(x, y, 0);
    }
}
