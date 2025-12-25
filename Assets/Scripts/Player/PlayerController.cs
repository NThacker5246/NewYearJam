using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float vel, jump, sens, ngle;
	private float rX, rY;
	[SerializeField] private Rigidbody rb;
	[SerializeField] private Transform cam, groundObject, armature;
	[SerializeField] private Animator anim;
	//[SerializeField] private Collider self;

	void FixedUpdate(){
		// rX -= Input.GetAxis("Mouse Y") * sens % 360;
		// rY += Input.GetAxis("Mouse X") * sens % 360;
		cam.eulerAngles = new Vector3(0, 0, 0);
		Vector3 movement = Quaternion.Euler(new Vector3(0, rY, 0)) * new Vector3(Input.GetAxis("Horizontal") * vel, Input.GetKeyDown(KeyCode.Space) && IsGrounded() ? jump : rb.linearVelocity.y, Input.GetAxis("Vertical") * vel);
		rb.linearVelocity = movement;
		anim.SetFloat("velocity", (Mathf.Abs(movement.x) + Mathf.Abs(movement.z)) / 2);
		anim.SetFloat("velocityY", movement.y);
		// print(movement);
		if(movement.x == 0 && movement.z == 0){
			Vector3 a = movement;
			a.Normalize();
			ngle += (Vector3.Angle(a, transform.forward) - ngle) * 0.1f;
			if(a.x < 0f) ngle *= -1f;
			armature.eulerAngles = new Vector3(-90, ngle, 0);
		}
	}

	private bool IsGrounded(){
		Collider[] cols = Physics.OverlapSphere(groundObject.position, 1);
		foreach(Collider col in cols){
			if(col.tag != "Player") return true;
		}
		return false;
	}
}
