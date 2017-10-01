using UnityEngine;

public class Earth : MonoBehaviour {
    private GameObject health;
    private Sprite[] sprites;

    private int hp = 5;

    void Start()
    {
        health = GameObject.Find("Health");

        sprites = Resources.LoadAll<Sprite>("sprites/health");
    }

    void Update()
    {

    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.name.Contains("Meatball"))
        {
            Destroy(col.gameObject);

            HealthLower();
        }
    }

    private void HealthLower()
    {
        hp--;

        if (hp == 0)
        {
            StartCoroutine(Data.loser());
        }

        try
        {
            health.GetComponent<SpriteRenderer>().sprite = sprites[5 - hp];
        } 
        catch
        {
            Data.gameOver = true;
        }
    }
}
