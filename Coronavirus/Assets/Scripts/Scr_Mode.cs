using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    Normal, Test, Tutorial
}
public class Scr_Mode : MonoBehaviour
{
    
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
