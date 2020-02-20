using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scr_LoadScene : MonoBehaviour
{
    //public Slider loadingSlider;     //显示进度的滑动条

    public Text loadingText;          //显示进度的文本

    public GameObject trunButton;

    float targetValue;

    AsyncOperation async;

    bool LoadOver = false;

    int step = 0;

    private void Start()

    {
        trunButton.SetActive(false);
        //loadingSlider.value = 0.0f;


    }

    IEnumerator AsyncLoading()

    {
        yield return new WaitForEndOfFrame();
        //异步加载场景

        async = SceneManager.LoadSceneAsync("Game");

        //阻止当加载完成自动切换
        Debug.Log("Pro :" + async.progress);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            //Output the current progress
            loadingText.text = "Loading progress: " + (async.progress * 100) + "%";

            // Check if the load has finished
            if (async.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                loadingText.text = "Press the space bar to continue";
                //Wait to you press the space key to activate the Scene
                if (Input.GetKeyDown(KeyCode.Space))
                    //Activate the Scene
                    async.allowSceneActivation = true;
            }

            yield return null;
        }
        //读取完毕后返回，系统会自动进入C场景

        yield return async;

    }

    void Update()

    {
        step += 1;
        if (step == 5)
        {
            StartCoroutine(AsyncLoading());

        }
        if (async == null) { return; }

        targetValue = async.progress;

        if (async.progress >= 0.9f)

        {

            //值最大为0.9

            targetValue = 0.9f;
            LoadOver = true;
        }



        loadingText.text = ((targetValue / 9 * 10 * 100)).ToString() + "%";

        if (LoadOver)
        {
            trunButton.SetActive(true);

            //async.allowSceneActivation = true;
        }

    }
    public void LoadScene()
    {
        async.allowSceneActivation = true;
    }
}
/*
 if (targetValue != loadingSlider.value)

        {

            loadingSlider.value = Mathf.Lerp(loadingSlider.value, targetValue, Time.deltaTime * loadingSpeed);

            if (Mathf.Abs(loadingSlider.value - targetValue) < 0.01f)

            {

                loadingSlider.value = targetValue;

            }

        }

        loadingText.text = ((int)(loadingSlider.value * 100)).ToString() + "%";

        if ((int)(loadingSlider.value * 100) == 100)

        {

            //允许异步加载完毕后自动切换场景

            async.allowSceneActivation = true;

        }
     */
