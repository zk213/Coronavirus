using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Event : MonoBehaviour
{
    GameData gameData;
    Scr_TimeControl time;
    Scr_Num value;

    public GameObject EventGroup;
    public Text Title;
    public Text Describe;
    public Image Picture;

    [HideInInspector]
    public bool showEvent;

    TimeMode TempTimeMode;

    string path;

    List<int> UnActiveEvent;//未激活事件，未激活事件就像事件2那样，需要有前一个事件的完成才会激活，在游戏运行时，游戏不会检测未激活事件的触发条件
    List<int> ActiveEvent;//激活事件，已激活事件就是母事件（没有前置需求的事件）和已经激活的子事件，游戏会检测他们的条件
    List<int> ReadyEvent;//就绪事件，就绪事件就是所有条件都满足的事件，他会每回合过一次概率，如果概率过了就会触发事件，如果没有继续等待下一回合
    List<int> HappenEvent;//正在发生的事件,将发生的事件按照一定顺序排序，并依次发生
    List<int> FinishEvent;//已发生事件,无论如何也不会再检测了，即使触发条件都成立
    List<float> dynamicProbability;//事件的发生概率


    void Awake()
    {
        gameData = Resources.Load<GameData>("Event");
        time = FindObjectOfType<Scr_TimeControl>();
        value = FindObjectOfType<Scr_Num>();
        EventGroup.SetActive(false);

        //对事件的类别进行初始化，分出未激活的子事件与普通事件,并初始化事件发生的概率
        FinishEvent.Add(0);//把事件模板进去，让他不会发生
        for (int i = 0; i < gameData.eventGroup.Count; i++)
        {
            dynamicProbability.Add(gameData.eventGroup[i].probability);//初始化事件发生的概率

            if (!FinishEvent.Contains(i))//首先不检测已经结束的事件
            {
                if (gameData.eventGroup[i].subEvent)//如果这个事件是子事件
                {
                    UnActiveEvent.Add(i);
                }
                else
                {
                    ActiveEvent.Add(i);
                }
            }
        }
    }

    //以下在模拟事件触发机制
    public void EventCheck()//在Scr_TimeControl里的Update里，这样就可以游戏事件里每增加一天才会检查一次，不至于每帧都查
    {

        for (int i = 0; i < gameData.eventGroup.Count; i++)
        {

            if (!FinishEvent.Contains(i) && !UnActiveEvent.Contains(i))//首先不检测已经结束的事件和未激活事件
            {
                if (ActiveEvent.Contains(i))//如果这个事件是激活事件
                {
                    //在这里对事件进行判断看他是否符合条件
                    //在这里就模拟符合了
                    if (true)
                    {
                        ActiveEvent.Remove(i);//从激活事件的列表中移除该事件
                        ReadyEvent.Add(i);//再放在就绪事件列表里
                    }
                }
                else if (ReadyEvent.Contains(i))//如果这个事件是就绪事件
                {
                    //这里还需要进行检测，看他是否符合条件
                    if (true)
                    {
                        if (Random.Range(0, 100) <= dynamicProbability[i])//过一次概率
                        {
                            ReadyEvent.Remove(i);//从就绪事件的列表中移除该事件
                            HappenEvent.Add(i);//放在正在发生事件列表里
                        }
                        else
                        {
                            dynamicProbability[i] += gameData.eventGroup[i].probabilityAdd;//概率增加
                            dynamicProbability[i] = Mathf.Clamp(dynamicProbability[i], 0, 100);//限制概率的范围
                        }
                    }
                    else
                    {
                        //如果条件不符合，则从就绪列表返回到之前的激活列表里
                        ReadyEvent.Remove(i);
                        ActiveEvent.Add(i);
                    }
                }
            }
        }
    }

    void LateUpdate()//事件的发生
    {
        if (HappenEvent.Count == 1)
        {
            //发生事件
            /*
             * TempTimeMode = time.timeMode;
             * showEvent = true;
             * time.Pause();//发生事件时游戏暂停
             * Title.text=gameData.eventGroup[HappenEvent[0]].title;
             * Describe.text=gameData.eventGroup[HappenEvent[0]].describe;
             * path=gameData.eventGroup[HappenEvent[0]].picture;
             * Picture.sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
             * //执行事件的效果
             * EventGroup.SetActive(true);
             
            */
        }
        if (HappenEvent.Count > 1)
        {
            for (int i = 0; i < HappenEvent.Count; i++)
            {
                //把要发生的事件排序，先比较优先级，如果优先级相同再比较序号，优先级高的先发生，序号小的先发生
                //以此发生事件
            }
        }


    }

    public void CloseEvent()
    {

        showEvent = false;

        //以下是恢复游戏速度
        if (TempTimeMode == TimeMode.OneSpeed)
        {
            time.OneSpeed();
        }
        if (TempTimeMode == TimeMode.FastSpeed)
        {
            time.FastSpeed();
        }

        EventGroup.SetActive(false);

    }
}
