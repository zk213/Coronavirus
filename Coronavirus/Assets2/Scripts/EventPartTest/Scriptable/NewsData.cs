using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "News", menuName = "Creat News Asset")]
public class NewsData : ScriptableObject
{
    [Header("新闻管理")]
    //[SerializeField]
    public List<News> newsGroup;
    [System.Serializable]
    public class News
    {
        [InspectorShow("标题")]
        public string title;//新闻的标题
        [InspectorShow("新闻内容")]
        public string content;//新闻的内容
    }

}
