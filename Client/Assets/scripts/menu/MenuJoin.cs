using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuJoin : MonoBehaviour
{
    private string create = "https://us-central1-game-jam-fall-2017.cloudfunctions.net/join?q=";
    private GameObject back;
    private GameObject go;

    void Start()
    {
        back = GameObject.Find("Back");
        go = GameObject.Find("Go");

        Data.player = 1;
    }

	void Update ()
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
            else if (active == go)
            {
                string text = GameObject.Find("InputField").GetComponent<InputField>().text;

                StartCoroutine(Join(text));
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            string text = GameObject.Find("InputField").GetComponent<InputField>().text;

            StartCoroutine(Join(text));
        }
	}

    IEnumerator Join(string text)
    {
        WWW www = new WWW(create + text);
        yield return www;

        try
        {
            ID query = JsonUtility.FromJson<ID>(www.text);
            Data.id = query.id;

            SceneManager.LoadScene("Main");
        }
        catch
        {

        }
    }
}
