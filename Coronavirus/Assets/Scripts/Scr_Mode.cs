using UnityEngine;

public enum GameMode
{
    Normal, Test, Tutorial
}
public class Scr_Mode : MonoBehaviour
{

    public GameMode gameMode;
    public string Language = "SimpleChinese";

    void Awake()
    {
        gameMode = GameMode.Normal;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
