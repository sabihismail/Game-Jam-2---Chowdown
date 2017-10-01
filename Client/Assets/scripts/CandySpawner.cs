using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySpawner : MonoBehaviour 
{
    private string url;
    private string deleteURL;

    private List<XY> candies = new List<XY>();

    private float TIME_DIFFERENCE = 0.1f;
    private float timer = 0;

    void Start ()
    {
        url = "https://us-central1-game-jam-fall-2017.cloudfunctions.net/g?q=" + Data.id + "&u=" + Data.player;
        deleteURL = "https://us-central1-game-jam-fall-2017.cloudfunctions.net/d?q=" + Data.id + "&u=" + Data.player + "&d=";
    }

    void Update ()
    {
        if (!Waited())
        {
            return;
        }
        else
        {
            SetTimer();
        }

        StartCoroutine(Check());
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

    IEnumerator Check()
    {
        WWW www = new WWW(url);
        yield return www;

        Debug.Log(url);

        Coords query = JsonUtility.FromJson<Coords>(www.text);

        Debug.Log(www.text);

        if (query.coords.Count > 0)
        {
            for (int i = 0; i < query.coords.Count; i++)
            {
                Vector2 xy = new Vector2(query.coords[i].x, query.coords[i].y);

                Vector2 inverse = xy * -1;
                inverse = Vector2.Scale(new Vector2(CameraScript.minX, CameraScript.minY), inverse);

                GameObject candy = (GameObject)GameObject.Instantiate(Resources.Load("prefabs/Candy" + Data.player));

                candy.SendMessage("Position", inverse);
                candy.SendMessage("Direction", xy);

                Delete(i);
            }
        }
    }

    public void Delete (int i)
    {
        var fullURL = deleteURL + i;

        Debug.Log(fullURL);

        StartCoroutine(SendDelete(fullURL));
    }

    IEnumerator SendDelete(string url)
    {
        WWW www = new WWW(url);
        yield return www;
    }

    [Serializable]
    private class Coords
    {
        public List<XY> coords;
    }

    [Serializable]
    private class XY
    {
        public int x;
        public int y;
    }
}
