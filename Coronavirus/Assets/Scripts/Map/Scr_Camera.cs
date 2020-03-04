using UnityEngine;

public class Scr_Camera : MonoBehaviour
{
    public float speed = 5;
    float iniX = 15;
    float iniY = 10;
    float iniS = 14;

    float zoomSpeed = 0.2f;
    float minZoom = 4;
    float maxZoom = 14;

    float maxW = 21.5f;
    float maxH = 12.5f;

    Rigidbody2D rigid;
    public GameObject BackGround;

    void Awake()
    {



        BackGround.transform.position = new Vector2(iniX, iniY);
        maxZoom = iniS;

        transform.position = new Vector2(iniX, iniY);
        rigid = GetComponent<Rigidbody2D>();
        Camera.main.orthographicSize = iniS;

    }

    void Update()
    {

        /*
        //Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {

            if (Camera.main.orthographicSize < maxZoom)
            {
                Camera.main.orthographicSize += zoomSpeed;
            }
            else
            {
                transform.position = new Vector2(iniX, iniY);
            }
        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {

            if (Camera.main.orthographicSize > minZoom)
                Camera.main.orthographicSize -= zoomSpeed;
        }
        */
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit point;
        Physics.Raycast(ray, out point, 1000);
        Vector3 Scrolldirection = ray.GetPoint(5);

        float step = speed * 20 * Time.deltaTime;

        // Allows zooming in and out via the mouse wheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > minZoom)
        {
            transform.position = Vector3.MoveTowards(transform.position, Scrolldirection, Input.GetAxis("Mouse ScrollWheel") * step);
            Camera.main.orthographicSize -= zoomSpeed;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.orthographicSize < maxZoom)
        {
            transform.position = Vector3.MoveTowards(transform.position, Scrolldirection, Input.GetAxis("Mouse ScrollWheel") * step);
            Camera.main.orthographicSize += zoomSpeed;
        }

        float Tx = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float Ty = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(Tx, Ty, 0);//相机的移动
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, iniX - maxW + maxW * Camera.main.orthographicSize / iniS, iniX + maxW - maxW * Camera.main.orthographicSize / iniS), Mathf.Clamp(transform.position.y, iniY - maxH + maxH * Camera.main.orthographicSize / iniS, iniY + maxH - maxH * Camera.main.orthographicSize / iniS));
    }

}
