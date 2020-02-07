using UnityEngine;

public enum GameMode
{
    [EnumNameAttribute("普通模式")]
    Normal,

    [EnumNameAttribute("测试模式")]
    Test,

    [EnumNameAttribute("教学模式")]
    Tutorial
}
public class Scr_Mode : MonoBehaviour
{
    [EnumNameAttribute("游戏模式")]
    public GameMode gameMode;
    [InspectorShow("当前语言")]
    public string Language = "SimpleChinese";

    void Awake()
    {
        gameMode = GameMode.Normal;
    }

}
