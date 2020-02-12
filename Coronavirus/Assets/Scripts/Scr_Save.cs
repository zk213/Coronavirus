using UnityEngine;

public class Scr_Save : MonoBehaviour
{
    Scr_Tech tech;
    Scr_Num num;
    Scr_Event events;
    Scr_TimeControl time;

    void Awake()
    {
        tech = FindObjectOfType<Scr_Tech>();
        num = FindObjectOfType<Scr_Num>();
        events = FindObjectOfType<Scr_Event>();
        time = FindObjectOfType<Scr_TimeControl>();
    }
    public void SaveButton()
    {
        tech.LocalSave();
        num.LocalSave();
        time.LocalSave();
    }
}
