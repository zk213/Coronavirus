using UnityEngine;

public class Scr_EventButton : MonoBehaviour
{
    public int index;
    Scr_Event Events;
    void Awake()
    {
        Events = FindObjectOfType<Scr_Event>();
    }

    public void CloseCGEvent()
    {
        Events.CGIndex = index;
        Events.EventEffectControl(Events.CGHappen);
        Events.CloseEvent();
    }
}
