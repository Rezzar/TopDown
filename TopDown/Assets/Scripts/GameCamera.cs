using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCamera : MonoBehaviour {

    public float scrollZone = 30;
    public float scrollSpeed = 5;

    public float xMax = 8;
    public float xMin = 0;
    public float yMax = 10;
    public float yMin = 3;
    public float zMax = 8;
    public float zMin = 0;

    private Vector3 desiredPosition;
    private static Vector3 moveToDestination = Vector3.zero;
    private static List<string> passables = new List<string>() { "Plane" };


    public Texture2D selectionHighlight = null;
    public static Rect selection = new Rect(0, 0, 0, 0);
    private Vector3 startClick = -Vector3.one;

	// Use this for initialization
	void Start ()
    {
        desiredPosition = transform.position;
	}   
	
	// Update is called once per frame
	/// <summary>
    /// 
    /// </summary>
    void Update () {
        float x = 0, y = 0, z = 0;
        float speed = scrollZone * Time.deltaTime;

        if (Input.mousePosition.x < scrollZone)
            x -= speed;

        else if (Input.mousePosition.x > Screen.width - scrollZone)
            x += speed;

        if (Input.mousePosition.y < scrollZone)
            z -= speed;

        else if (Input.mousePosition.y > Screen.height - scrollZone)
            z += speed;

        y += Input.GetAxis("Mouse ScrollWheel");
        Vector3 move = new Vector3(x, y, z) + desiredPosition;
        move.x = Mathf.Clamp(move.x, xMin, xMax);
        move.y = Mathf.Clamp(move.y, yMin, yMax);
        move.z = Mathf.Clamp(move.z, zMin, zMax);
        desiredPosition = move;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.2f);
        CheckCamera();
        Cleanup();
        GetDestination();
    }
    void LateUpdate() {
  
    }

    private void CheckCamera() {
        if (Input.GetMouseButtonDown(0))
            startClick = Input.mousePosition;
        else if (Input.GetMouseButtonUp(0))
        {
            if (selection.width < 0)
            {
                selection.x += selection.width;
                selection.width = -selection.height;
            }
            if (selection.height < 0)
            {
                selection.y += selection.height;
                selection.height = -selection.height;
            }
            startClick = -Vector3.one;
        }

        if (Input.GetMouseButton(0))
            selection = new Rect(startClick.x, InvertMouseY(startClick.y), Input.mousePosition.x - startClick.x,
                InvertMouseY(Input.mousePosition.y) - InvertMouseY(startClick.y));
    }

    private void OnGUI()
    {
        if (startClick != -Vector3.one)
        {
            GUI.color = new Color(1, 1, 1, 0.5f);
            GUI.DrawTexture(selection, selectionHighlight);
        }
    }

    public static float InvertMouseY(float y)
    {
        return Screen.height - y;
    }

    private void Cleanup()
    {
        if (!Input.GetMouseButtonUp(1))
            moveToDestination = Vector3.zero;
    }

    public static Vector3 GetDestination()
    {
        if (moveToDestination == Vector3.zero)
        {
            RaycastHit hit;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(r, out hit))
            {
                while (!passables.Contains(hit.transform.gameObject.name))
                {
                    if (!Physics.Raycast(hit.transform.position, r.direction, out hit))
                        break;
                }
            }
            if(hit.transform != null)
            moveToDestination = hit.point;
        }
        return moveToDestination;
    }

}
