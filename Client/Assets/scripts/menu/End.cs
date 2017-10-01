using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End : MonoBehaviour
{ 
    private GameObject back;

    void Start()
    {
        back = GameObject.Find("Back");

        Image background = GameObject.Find("Image").GetComponent<Image>();

        if (Data.isLoser)
        {
            if (Data.player == 0)
            {
                background.sprite = Resources.Load<Sprite>("menu/lose-blue");
            }
            else
            {
                background.sprite = Resources.Load<Sprite>("menu/lose-pink");
            }
        }
        else
        {
            if (Data.player == 0)
            {
                background.sprite = Resources.Load<Sprite>("menu/win-blue");
            }
            else
            {
                background.sprite = Resources.Load<Sprite>("menu/win-pink");
            }
        }

        Data.gameOver = false;
    }

    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject active = EventSystem.current.currentSelectedGameObject;

            if (active == back)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
