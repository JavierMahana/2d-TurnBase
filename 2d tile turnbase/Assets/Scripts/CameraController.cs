//Se encarga del movimiento de la camara
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float xPanBorderThickness = 50f;
    public float yPanBorderThickness = 40f;
    public float panSpeed = 20f;
    public Vector2 panLimitOffSet;
    
    float minPanX;
    float maxPanX;
    float minPanY;
    float maxPanY;

    Map map;
    Vector2 resolution;


    //hay bugs
    private void Start()
    {
        resolution = new Vector2(Screen.width, Screen.height);

        map = GameObject.FindObjectOfType<Map>();

        minPanX = Camera.main.ScreenToWorldPoint(new Vector3(resolution.x / 2, 0, 0)).x;
        maxPanX = map.GetWidth() - Camera.main.ScreenToWorldPoint(new Vector3(resolution.x / 2, 0, 0)).x;
        minPanY = Camera.main.ScreenToWorldPoint(new Vector3(0, resolution.y / 2, 0)).y;
        maxPanY = map.GetHeigth() - Camera.main.ScreenToWorldPoint(new Vector3(0, resolution.y / 2, 0)).y;
    }

    // Update is called once per frame
    void Update ()
    {
        ScreenSizeChange();
        Vector3 pos = transform.position;
        

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= resolution.y - yPanBorderThickness)
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= yPanBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= resolution.x - xPanBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= xPanBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }




        pos.x = Mathf.Clamp(pos.x, minPanX - panLimitOffSet.x, maxPanX + panLimitOffSet.x);
        pos.y = Mathf.Clamp(pos.y, minPanY - panLimitOffSet.y, maxPanY+ panLimitOffSet.y);

        transform.position = pos;

    }


    void ScreenSizeChange()
    {
        if (resolution.x != Screen.width || resolution.y != Screen.height)
        {
            resolution.x = Screen.width;
            resolution.y = Screen.height;

            minPanX = Camera.main.ScreenToWorldPoint(new Vector3(resolution.x / 2, 0, 0)).x;
            maxPanX = map.GetWidth() - Camera.main.ScreenToWorldPoint(new Vector3(resolution.x / 2, 0, 0)).x;
            minPanY = Camera.main.ScreenToWorldPoint(new Vector3(0, resolution.y / 2, 0)).y;
            maxPanY = map.GetHeigth() - Camera.main.ScreenToWorldPoint(new Vector3(0, resolution.y / 2, 0)).y;
        }
    }
}
