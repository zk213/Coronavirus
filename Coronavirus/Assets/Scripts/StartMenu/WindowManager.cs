using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour
{
    #region Attributes



    #region Player Pref Key Constants
    private const string RESOLUTION_PREF_KEY = "resolution";

    #endregion

    #region Resolution
    [SerializeField]
    private Text resolutionText;
    private Resolution[] resolutions;

    private Resolution[] newResolutions = new Resolution[6];//忽略超低分辨率，只保留最后的6个从1368到1920*1080
    
    private int currResolutionIndex = 0;

    #endregion 

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;//获取当前屏幕所有分辨率存到resolutions这个array里
        
        currResolutionIndex = PlayerPrefs.GetInt(RESOLUTION_PREF_KEY, 0);//获取当前在array的index
        print(currResolutionIndex);
        //默认1920*1080，array的最后一位的分辨率
        for(int i = 16; i <= 21; i++)
        {
            newResolutions[i - 16] = resolutions[i];
        }
        for(int i = 0;i < 6; i++)
        {
            print(newResolutions[i]);
        }
        SetAndApplyResolution(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Resolution Cycling
    //显示可选项
    private void SetResolutionText(Resolution resolution)
    {
        resolutionText.text = resolution.width + "x" + resolution.height;

    }
    //按钮右边
    public void SetNextResolution()
    {
        currResolutionIndex = GetNextWrappedIndex(newResolutions, currResolutionIndex);//gai 
        SetResolutionText(newResolutions[currResolutionIndex]);//gai
        print(currResolutionIndex);
    }
    //按钮左边
    public void SetPreviousResolution()
    {
        currResolutionIndex = GetPreviousWrappedIndex(newResolutions, currResolutionIndex);//gai
        SetResolutionText(newResolutions[currResolutionIndex]);//gai
        print(currResolutionIndex);
    }
    #endregion

    #region Apply Resolution
    //按照当前的arrayindex设置分辨率
    private void SetAndApplyResolution(int newResolutionIndex)
    {
        print(newResolutionIndex);
        currResolutionIndex = newResolutionIndex;
        ApplyCurrentResolution();
    }

    private void ApplyCurrentResolution()
    {
        ApplyResolution(newResolutions[currResolutionIndex]);//gai
    }


    private void ApplyResolution(Resolution resolution)
    {
        SetResolutionText(resolution);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt(RESOLUTION_PREF_KEY, currResolutionIndex);
    }
    #endregion

    #region Misc Helpers

    #region Index Wrap Helpers
    //获取分辨率text，不是很重要
    private int GetNextWrappedIndex<T>(IList<T> collection, int currentIndex)
    {
        if (collection.Count < 1) return 0;
        return (currentIndex + 1)%collection.Count; 
    }

    private int GetPreviousWrappedIndex<T>(IList<T> collection, int currentIndex)
    {
        if (collection.Count < 1) return 0;
        if ((currentIndex - 1) < 0) return collection.Count - 1;
        return (currentIndex - 1) % collection.Count;
    }
    #endregion 
    #endregion  

    //apply 按钮
    public void ApplyChanges()
    {
        SetAndApplyResolution(currResolutionIndex);
        print(currResolutionIndex + "is applied");
    }

}
