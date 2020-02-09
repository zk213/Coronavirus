using UnityEngine;

public class Scr_TechButton : MonoBehaviour
{
    Scr_Tech Tech;
    public int LocalIndex;

    void Awake()
    {
        Tech = FindObjectOfType<Scr_Tech>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TechButtonClick()
    {
        Tech.TechIndex = LocalIndex;
        Tech.TextUpdate = true;
    }
}
