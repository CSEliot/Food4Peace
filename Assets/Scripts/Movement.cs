using UnityEngine;
using System.Collections;


[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class Movement : MonoBehaviour {	

	private float rotUpDown;// = 0;
	public float rotUpDownSensitivity;
	public float upDownRange;
	//private Vector3 speed;
	private float verticalSpeed;
	private float rotLeftRight;
	public float rotLeftRightSensitivity;
	public float leftRightRange;
	private float maxVelocityChange = 10.0f;

	private float upMove;
	
	private Vector3 playerPos;
	//private Ray	ray;
	//private RaycastHit rayHitDown;
	private float moveSpeed;
	
	//ACTION STRINGS
	//==================================================================
	private string Haim_str = "Rotation";
	private string Vaim_str = "UpDown";
	//==================================================================
	
	//PERSONAL CHARACTER MODIFIERS
	public float flySpeedModifier;
	
	//For looking, we are assigning rotations, but we need original values
	//that aren't getting modified, so we can re-assign them.
	private Vector3 startingCameraRotation;
	private Vector3 newRotationAngle;
	
	private Vector3 targetVelocity;
	Vector3 velocity;
	Vector3 velocityChange;

	void Awake () {
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
	}
	
	
	// Use this for initialization
	void Start () {
		newRotationAngle = new Vector3();
		startingCameraRotation = transform.GetChild(0).transform.localRotation.eulerAngles;
		moveSpeed = flySpeedModifier;
		rotLeftRight = 0.0f;
		rotUpDown = 0.0f;
	}
	
	
	
	// Update is called once per frame
	void FixedUpdate () {
		
		//player rotation
		//left and right
		
		rotLeftRight = Input.GetAxis(Haim_str)*rotLeftRightSensitivity;
		rotLeftRight = Mathf.Clamp(rotLeftRight, -leftRightRange, leftRightRange);
		rotUpDown = -Input.GetAxis(Vaim_str)*rotUpDownSensitivity;
		rotUpDown = Mathf.Clamp(rotUpDown, -upDownRange, upDownRange);
		newRotationAngle.x = rotUpDown;
		newRotationAngle.y = rotLeftRight;
		newRotationAngle.z = startingCameraRotation.z;
		transform.localRotation = Quaternion.Euler(newRotationAngle);

		//Movement
		moveSpeed = flySpeedModifier;
		
		//velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
		//velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
		velocityChange.y = 0;

		if (transform.position.y >= 21) {
			upMove = 0;
		}else{
			upMove = Input.GetAxis(Vaim_str)*moveSpeed;
		}

		rigidbody.AddRelativeForce (Input.GetAxis(Haim_str)*moveSpeed,upMove, moveSpeed);
	}

	void OnCollisionEnter(Collision thing){
		Debug.Log ("COLLIDED WITH: " + thing.gameObject.name);
//		Vector3 tempVect;
//		// we want to prevent isGrounded from being true and totalJumpsMade = 0 until 2 seconds later
//		if(isGrounded == false && canCheckForJump){
//			for(int i = 0; i < floor.contacts.Length; i++){
//				tempVect = floor.contacts[i].normal;
//				if( tempVect.y > floorInclineThreshold){
//					isGrounded = true;
//					totalJumpsMade = 0;
//					return;
//					//Manager.say("Collision normal is: " + tempVect);
//				}
//			}
//		}
	}
}
