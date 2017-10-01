using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public class ID
{
    public string id;
}

[Serializable]
public class Query
{
    public Player p1;
    public Player p2;
}

[Serializable]
public class Player
{
    public List<XY> xy;
}

[Serializable]
public class XY
{
    public int x;
    public int y;
}
