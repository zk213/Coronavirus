using UnityEngine;

public class Scr_Camera : MonoBehaviour
{
    public float speed = 5;
    public float iniX = 15;
    public float iniY = 10;
    public float iniS = 12;


    void Awake()
    {
        transform.position = new Vector2(iniX, iniY);
    }

    void Update()
    {
        //Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {

            if (Camera.main.orthographicSize < 12)
            {
                Camera.main.orthographicSize += 0.2F;
            }
            else
            {
                transform.position = new Vector2(iniX, iniY);
            }
        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {

            if (Camera.main.orthographicSize > 3)
                Camera.main.orthographicSize -= 0.2F;
        }
        float Tx = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float Ty = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(Tx, Ty, 0);//相机的移动

    }

}
