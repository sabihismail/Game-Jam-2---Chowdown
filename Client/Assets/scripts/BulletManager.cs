using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager
{
    public int MAX_SCORE = 5;
    private float TIME_DIFFERENCE = 0.1f;

    public int score = 0;

    private float timer = 0;

    public void Shoot()
    { 
        if (score == 0)
        {
            return;
        }
        
        if (!Waited())
        {
            return;
        }
        else
        {
            SetTimer();
        }

        Transform input = GameObject.Find("Protector").transform;

        Vector3 pos = new Vector3(input.position.x, input.position.y, input.position.z);
        Vector3 angle = new Vector3(input.eulerAngles.x, input.eulerAngles.y, input.eulerAngles.z);

        GameObject candy = (GameObject)GameObject.Instantiate(Resources.Load("prefabs/Candy" + Data.player));

        candy.SendMessage("Position", pos);
        candy.SendMessage("Direction", pos.normalized);
        candy.SendMessage("Angle", angle);

        Physics2D.IgnoreCollision(candy.GetComponent<Collider2D>(), input.gameObject.GetComponent<Collider2D>());

        score--;
    }

    public void SetTimer ()
    {
        timer = Time.fixedTime + TIME_DIFFERENCE;
    }

    private bool Waited()
    {
        if (Time.fixedTime >= timer)
        {
            return true;
        }

        return false;
    }

    public void Increase()
    {
        if (score == MAX_SCORE)
        {
            return;
        }

        score++;
    }
}