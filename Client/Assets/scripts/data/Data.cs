using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Data
{
    public static bool gameOver = false;

    public static string id = "";
    public static int player = 0;
    public static bool isLoser = false;

    public static Sprite spriteEarth;
    public static Sprite spriteProtector;

    private static string loserURL = "https://us-central1-game-jam-fall-2017.cloudfunctions.net/loser?q=";
    private static string checkLoserURL = "https://us-central1-game-jam-fall-2017.cloudfunctions.net/g?q=" + Data.id;

    public static void changeSprites()
    {
        spriteEarth = Resources.Load<Sprite>("sprites/earth" + player);
        spriteProtector = Resources.Load<Sprite>("sprites/shield" + player);
    }

    public static IEnumerator loser ()
    {
        var fullURL = loserURL + id + "&u=" + player;

        Debug.Log(fullURL);

        WWW www = new WWW(fullURL);
        yield return www;

        Debug.Log(www.text);

        WWW www2 = new WWW(checkLoserURL);
        yield return www2;

        Loser loser = JsonUtility.FromJson<Loser>(www.text);
        if (loser.loser == player)
        {
            isLoser = true;
        }
        else
        {
            isLoser = false;
        }

        SceneManager.LoadScene("End");
    }

    [Serializable]
    private class Loser
    {
        public int loser;
    }
}