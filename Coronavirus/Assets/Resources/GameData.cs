using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Event", menuName = "Creat Event Asset")]
public class GameData : ScriptableObject
{
    [Header("事件管理")]
    //[SerializeField]
    public List<Event> m_Event;
    [System.Serializable]
    public class Event
    {
        public string title;
        public string describe;
        public string picture;
        public bool isRepeat=false;
        public float probability;
        public float probabilityAdd;
        //触发机制
    }

}
