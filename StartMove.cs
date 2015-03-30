using UnityEngine;
using System.Collections;



public class StartMove : MonoBehaviour {

	// Use this for initialization



		
	private Vector3 mouse_pos;
	private Vector3 object_pos;
	private float angle;
	//private float bulletSpeed = 400;
	private float bulletSpeed = 9000;

	public Transform lineSelectorTransform;
	bool didPress = false;
	LineRenderer line;

	Vector2 fingerOffset;
	
	// Use this for initialization

	public void AllowLaunches(bool yes){
		didPress = !yes;

	}
	void Start () {
		fingerOffset = new Vector2 (0, 10);
		line = gameObject.GetComponent<LineRenderer> ();
		line.useWorldSpace = true;
		line.material =  new Material (Shader.Find("Sprites/Default"));
		line.SetWidth (.2f, .2f);
		line.SetVertexCount (2);
	}
	
	void Update(){ 
		// Point the cannon at the mouse.
		//mouse_pos = Input.GetTouch (0).position;
		mouse_pos = Input.mousePosition;
		mouse_pos.z = 0.0f; 
		object_pos = Camera.main.WorldToScreenPoint (transform.position);
		mouse_pos.x = mouse_pos.x - object_pos.x;
		mouse_pos.y = mouse_pos.y - object_pos.y;
		angle = Mathf.Atan2 (mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg - 90;
		Vector3 rotationVector = new Vector3 (0, 0, angle);
		transform.rotation = Quaternion.Euler (rotationVector);
		transform.LookAt (mouse_pos);
		
		
		//ray.direction = mouse_pos;
		// Fire a bullet.
		
		
		foreach (Touch touch in Input.touches) {

			if (touch.phase == TouchPhase.Ended) {
				line.SetWidth(0,0);
				FireBall ();
			} 

		}

		if (Input.touchCount > 0) {
			mouse_pos = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position + fingerOffset);
			line.SetPosition (0, transform.position);
			mouse_pos.z = 0;
			line.SetPosition (1, mouse_pos);
		}

		
		
		if (Input.GetKey(KeyCode.Space) && didPress == false) {
			
			line.SetPosition (0, transform.position);
			line.SetPosition (1, mouse_pos);
			line.SetWidth(.2f,.2f);
		}
		
		if (Input.GetKeyUp(KeyCode.Space)) {
			line.SetWidth(0,0);
			FireBall();
		}
	

	}

	void FireBall(){
		if (didPress == false) {
					didPress = true;
					rigidbody2D.AddForce (transform.forward * bulletSpeed);					
			} 
	}
	
	void FixedUpdate () {    

	}
	
}
