using UnityEngine;
using UnityEngine.SceneManagement;


public class Scr_Load : MonoBehaviour
{
    Scr_Mode mode;
    void Awake()
    {
        mode = FindObjectOfType<Scr_Mode>();
    }

    public void Load()
    {
        mode.isLoad = true;
        Debug.Log(2);
        SceneManager.LoadScene("Game");
    }
}
