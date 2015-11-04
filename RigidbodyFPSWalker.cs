	using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class RigidbodyFPSWalker : MonoBehaviour {
	
	public float speed = 7.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 12.0f;
	public bool canJump = true;
	public float jumpHeight = 1.0f;
	public bool grounded = false;
	private Transform tr;
	private float dist;
	public MouseLook ML;
	public bool gravityNorm = true;
	
	void Awake () {
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
		tr = GetComponent<Transform> ();
		dist = (tr.localScale.y) / 2;
	}
	
	void FixedUpdate () {

		float vScale = 1.0f;

		if (grounded) {
			// Calculate how fast we should be moving
			Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= speed;
			
			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rigidbody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
			
			if (canJump && Input.GetButtonDown("Gravity")){
				Gravity();
			}

			if (Input.GetKey("left shift"))
			{
				speed = 10.0f;            
			}
			else{
				speed = 7.0f;
			}

			if (Input.GetKey("c"))
			{ // press C to crouch
				vScale = 0.5f;
				speed = 3.0f; // slow down when crouching
			}
			else {
				speed = 7.0f;
			}
			float ultScale = tr.localScale.y; // crouch/stand up smoothly 
			
			Vector3 tmpScale = tr.localScale;
			Vector3 tmpPosition = tr.position;
			
			tmpScale.y = Mathf.Lerp(tr.localScale.y, vScale, 5 * Time.deltaTime);
			tr.localScale = tmpScale;
			
			tmpPosition.y += dist * (tr.localScale.y - ultScale); // fix vertical position        
			tr.position = tmpPosition;
			
			// Jump
			if (canJump && Input.GetButton("Jump")) {
				if (gravityNorm)
					rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
				else
					rigidbody.velocity = new Vector3(velocity.x, -5, velocity.z);
			}
		}
		
		// We apply gravity manually for more tuning control
		rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));
		
		grounded = false;
	}
	
	void OnCollisionStay (Collision coll) {
		if (coll.gameObject.tag == "Floor")
			grounded = true;
		//ML.enabled = true;
	}
	void OnCollisionEnter(){
		rigidbody.freezeRotation = true;
		ML.enabled = true;
	}

	void Gravity(){
		gravity *= -1;
		gravityNorm = !gravityNorm;
		rigidbody.freezeRotation = !rigidbody.freezeRotation;
		ML.enabled = false;
		tr.Rotate (0, 0, 180);
	}
	
	float CalculateJumpVerticalSpeed () {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		if (gravityNorm)
			return Mathf.Sqrt(2 * jumpHeight * gravity);
		else
			return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}