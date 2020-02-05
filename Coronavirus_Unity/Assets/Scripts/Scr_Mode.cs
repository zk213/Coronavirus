using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Mode : MonoBehaviour
{
    public enum GameMode
    {
        Normal, Test, Tutorial
    }
    public GameMode gameMode;

    void Awake()
    {
        gameMode = GameMode.Normal;
}

    // Update is called once per frame
    void Update()
    {

    }
}
