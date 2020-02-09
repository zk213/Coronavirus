using System.Collections.Generic;
using UnityEngine;

public enum TypeTechnology
{
    [EnumNameAttribute("政策")]
    gover,
    [EnumNameAttribute("医疗")]
    medicine,
    [EnumNameAttribute("媒体")]
    media
}

[CreateAssetMenu(fileName = "Technology", menuName = "Creat Technology Asset")]
public class TechnologyData : ScriptableObject
{
    [Header("技术管理")]
    //[SerializeField]
    public List<Technology> technologyGroup;
    [System.Serializable]
    public class Technology
    {
        [InspectorShow("技术标题")]
        public string title;//技术的标题
        [InspectorShow("技术介绍")]
        public string describe;//技术的介绍
        [InspectorShow("技术图标")]
        public string picture;//技术的图标
        [InspectorShow("技术尤里卡")]
        public string eureka;//技术的尤里卡
        [EnumNameAttribute("技术类型")]
        public TypeTechnology type = TypeTechnology.gover;
        [InspectorShow("技术前置")]
        public int father;//技术的前置技术
        [InspectorShow("技术花费")]
        public int cost;//技术的花费
        [InspectorShow("x坐标")]
        public float Posx;//事件的坐标
        [InspectorShow("y坐标")]
        public float Posy;
        //[InspectorShow("技术效果")]
        //public int effectTechnology;//技术的效果
    }
}
