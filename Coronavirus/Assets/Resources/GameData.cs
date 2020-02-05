using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Event", menuName = "Creat Event Asset")]
public class GameData : ScriptableObject
{
    [Header("事件管理")]
    //[SerializeField]
    public List<Event> eventGroup;
    [System.Serializable]
    public class Event
    {
        public string title;//事件的标题
        public string describe;//事件的内容
        public string picture;//事件的配图的地址（尺寸：194*193）
        public bool subEvent = false;//事件是否为子事件
        public bool isRepeat = false;//事件是否可重复发生
        public int priority = 0;//事件的优先级，即当多个事件都需要同时发生时，各个事件的优先级，0为默认，越高越优先
        public float probability;//事件发生的概率，100为必然发生
        public float probabilityAdd;//若事件条件满足但没有发生，下回合（下一天）增加的概率
        //触发机制
        //触发效果
    }

}
