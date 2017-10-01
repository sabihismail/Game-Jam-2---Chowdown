using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySpawner : MonoBehaviour 
{
    private string url;

    private List<XY> candies = new List<XY>();

    void Setup ()
    {
        url = "https://us-central1-game-jam-fall-2017.cloudfunctions.net/g?q=" + Data.id + "&u=" + Data.player;

        InvokeRepeating("CheckDB", 3f, 1f);
    }

    void Update ()
    {

    }

    private void CheckDB ()
    {
        StartCoroutine(Check());
    }

    IEnumerator Check()
    {
        WWW www = new WWW(url);
        yield return www;

        Coords query = JsonUtility.FromJson<Coords>(www.text);

        if (query.coords.Count > candies.Count)
        {
            int count = query.coords.Count - candies.Count;

            for (int i = query.coords.Count - 1; i > candies.Count - 1; i--)
            {
                Vector2 xy = new Vector2(query.coords[i].x, query.coords[i].y);

                Vector2 inverse = xy * -1;
                inverse = Vector2.Scale(new Vector2(CameraScript.maxX, CameraScript.maxY), inverse);

                GameObject candy = (GameObject)GameObject.Instantiate(Resources.Load("prefabs/Candy" + Data.player));

                candy.SendMessage("Position", inverse);
                candy.SendMessage("Direction", xy);
            }

            candies = query.coords;
        }
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
