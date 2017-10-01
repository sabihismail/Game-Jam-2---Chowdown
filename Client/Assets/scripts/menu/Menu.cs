using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    private GameObject host;
    private GameObject join;

	void Start ()
    {
        host = GameObject.Find("Host");
        join = GameObject.Find("Join");
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

            if (active == host)
            {
                SceneManager.LoadScene("MenuHost");
            }
            else if (active == join)
            {
                SceneManager.LoadScene("MenuJoin");
            }
        }
    }
}
