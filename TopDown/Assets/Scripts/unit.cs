using UnityEngine;
using System.Collections;

public class unit : MonoBehaviour {

    // Use this for initialization
    public bool selected = false;
    public float floorOffset = 1;
    public float speed = 10;
    public float stopDistanceOffset = 0.5f;

    private Vector3 moveToDest = Vector3.zero;
    


    /// <summary>
    /// 
    /// </summary>
    private void Update () {

        var renderer = GetComponent<Renderer>();

        if (renderer.isVisible && Input.GetMouseButtonUp(0))
        {
            Vector3 camPos = Camera.main.WorldToScreenPoint (transform.position);
            camPos.y = GameCamera.InvertMouseY(camPos.y);
            selected = GameCamera.selection.Contains(camPos);
            if (selected)
                renderer.material.color = Color.red;
            else
                renderer.material.color = Color.white;
        }
        if (selected && Input.GetMouseButtonUp(1))
        {
            Vector3 destination = GameCamera.GetDestination();

            if (destination != Vector3.zero)
            {
                // gameObject.GetComponent<NavMeshAgent>().SetDestination(destination);

                moveToDest = destination;
                moveToDest.y = floorOffset;

            }
        }
        UpdateMove();

        
	}

    private void UpdateMove()
    {

        var rigidbody = GetComponent<Rigidbody>();
        if (moveToDest != Vector3.zero && transform.position != moveToDest)
        {
            Vector3 direction = (moveToDest - transform.position).normalized;
            direction.y = 0;
            rigidbody.velocity = direction * speed;

            if (Vector3.Distance(transform.position, moveToDest) < stopDistanceOffset)
                moveToDest = Vector3.zero;
        }
        else rigidbody.velocity = Vector3.zero;
    }
}
