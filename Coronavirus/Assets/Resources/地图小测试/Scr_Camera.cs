using UnityEngine;

public class Scr_Camera : MonoBehaviour
{
    public float speed = 5;

    void Update()
    {
        //Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {

            if (Camera.main.orthographicSize < 10)
                Camera.main.orthographicSize += 0.2F;
        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {

            if (Camera.main.orthographicSize > 1)
                Camera.main.orthographicSize -= 0.2F;
        }
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;//左右移动

        float y = Input.GetAxis("Vertical") * Time.deltaTime * speed;//前后移动

        transform.Translate(x, y, 0);//相机的移动

    }
}
