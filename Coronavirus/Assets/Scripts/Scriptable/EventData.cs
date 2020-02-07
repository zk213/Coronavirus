using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Event", menuName = "Creat Event Asset")]
public class EventData : ScriptableObject
{
    [Header("事件管理")]
    //[SerializeField]
    public List<Event> eventGroup;
    [System.Serializable]
    public class Event
    {
        [InspectorShow("事件标题")]
        public string title;//事件的标题
        [InspectorShow("事件内容")]
        public string describe;//事件的内容
        [InspectorShow("事件配图")]
        public string picture;//事件的配图的地址（尺寸：194*193）
        [InspectorShow("子事件")]
        public bool subEvent = false;//事件是否为子事件
        [InspectorShow("重复事件")]
        public bool isRepeat = false;//事件是否可重复发生
        [InspectorShow("隐藏事件")]
        public bool isInvisible = false;//事件是否仅仅作为新闻的触发器
        [InspectorShow("重要事件")]
        public bool isImportant = false;//事件是否重要，有CG与选项
        [InspectorShow("优先级")]
        public int priority = 0;//事件的优先级，即当多个事件都需要同时发生时，各个事件的优先级，0为默认，越高越优先
        [InspectorShow("事件发生概率")]
        public float probability;//事件发生的概率，100为必然发生
        [InspectorShow("事件概率叠加")]
        public float probabilityAdd;//若事件条件满足但没有发生，下回合（下一天）增加的概率
        //触发机制
        //触发效果
    }

}
