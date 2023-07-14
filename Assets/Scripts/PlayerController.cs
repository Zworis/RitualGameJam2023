using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float drag;
	public float xSensitivity;
	public float ySensitivity;
	public GameObject flashlight;
	public GameObject holdObject;
	public float lerpSpeed;



	private Rigidbody rb;
	private GameObject cam;
	private float yValue;




	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody>();
		LockCursor(true);
		cam = Camera.main.gameObject;
	}

    // Update is called once per frame
    void Update()
    {
		flashlight.transform.position = holdObject.transform.position;
		flashlight.transform.rotation = Quaternion.Lerp(flashlight.transform.rotation, holdObject.transform.rotation, lerpSpeed * Time.deltaTime);

		rb.AddRelativeForce(new Vector3(Input.GetAxisRaw("Horizontal")* speed, 0, Input.GetAxisRaw("Vertical") * speed), ForceMode.Impulse);
		rb.AddForce(new Vector3(rb.velocity.x  * -drag, 0, rb.velocity.z  * -drag), ForceMode.Impulse);
		transform.Rotate(new Vector3(0,xSensitivity * Time.timeScale * Input.GetAxis("Mouse X"),0));
        cam.transform.localRotation = Quaternion.Euler(yValue, 0, 0);
        yValue -= Input.GetAxis("Mouse Y") * ySensitivity * Time.timeScale;        
        if (yValue > 90)
        {
            yValue = 90;
        }
        else if (yValue < -90)
        {
            yValue = -90;
        }

	}



	void LockCursor(bool doLock) {
		if (doLock == true) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}
