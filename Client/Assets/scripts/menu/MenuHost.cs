using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuHost : MonoBehaviour
{
    private string create = "https://us-central1-game-jam-fall-2017.cloudfunctions.net/create";
    private string queryURL;

    private float TIME_DIFFERENCE = 1.5f;
    private float timer = 0;

    private GameObject back;

    IEnumerator Start()
    {
        back = GameObject.Find("Back");

        Data.player = 0;

        WWW www = new WWW(create);
        yield return www;

        ID id = JsonUtility.FromJson<ID>(www.text);
        GameObject.Find("Code").GetComponent<Text>().text = id.id;

        queryURL = "https://us-central1-game-jam-fall-2017.cloudfunctions.net/g?q=" + id.id;
        Data.id = id.id;
    }
    
    void Update()
    {
        if (Data.gameOver)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject active = EventSystem.current.currentSelectedGameObject;

            if (active == back)
            {
                SceneManager.LoadScene("Menu");
            }
        }

        if (!Waited())
        {
            return;
        }
        else
        {
            SetTimer();
        }

        if (queryURL != null)
        {
            StartCoroutine(Check());
        }
    }

    IEnumerator Check()
    {
        WWW www = new WWW(queryURL);
        yield return www;

        Query query = JsonUtility.FromJson<Query>(www.text);

        if(query.p1.xy != null && query.p2.xy != null) {
            SceneManager.LoadScene("Main");
        }
    }

    public void SetTimer()
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
}
