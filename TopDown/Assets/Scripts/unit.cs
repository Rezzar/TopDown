using UnityEngine;
using System.Collections;

public class unit : MonoBehaviour {

    // Use this for initialization
    public bool selected = false;

    private void Update () {
         
    var renderer = GetComponent<Renderer>();

        if (renderer.isVisible && Input.GetMouseButtonUp(2))
        {
            Vector3 camPos = Camera.main.WorldToScreenPoint (transform.position);
            camPos.y = GameCamera.InvertMouseY(camPos.y);
            selected = GameCamera.selection.Contains(camPos);

        }
        if (selected)
            renderer.material.color = Color.red;
        else
            renderer.material.color = Color.white;
       // 
        
	}
}
