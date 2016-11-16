using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour {

    public float rotationSpeed = 450;
    public float walkSpeed = 5;
    public float runSpeed = 8;

    private Quaternion targetRotation;
    private CharacterController controller;
	
	void Start () {
        controller = GetComponent<CharacterController>();
	}

    // Update is called once per frame
    void Update() {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (input != Vector3.zero) { 
        targetRotation = Quaternion.LookRotation(input);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
            
        }
        Vector3 motion = input;
        motion += Vector3.up * -8;

        controller.Move(motion * Time.deltaTime);
	}
}
