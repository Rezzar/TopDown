using UnityEngine;
using System.Collections;

public class drag : MonoBehaviour
{
    public GameObject otherObject;
    public float distance = 10;

    void Start()
    {
    }



    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = objPosition;

        //otherObject.GetComponent<unit>().enabled = false;
    }


}
