using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float drag;
	public float xSensitivity;
	public float ySensitivity;
	public GameObject flashlight;
	public GameObject holdingTarget;
	public float lerpSpeed;
	public GameObject crosshair;
	public Texture crosshairUnselected;
	public Texture crosshairSelected;
	public LayerMask itemsMask;
	public float reach;


	private GameObject holdingObj;
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
		flashlight.transform.position = holdingTarget.transform.position;
		flashlight.transform.rotation = Quaternion.Lerp(cam.transform.rotation, holdingTarget.transform.rotation, lerpSpeed * Time.deltaTime);

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


		if (Input.GetMouseButtonUp(0)) {
			holdingObj.transform.SetParent(null);
			holdingObj.GetComponent<Collider>().enabled = true;
			holdingObj.GetComponent<Rigidbody>().isKinematic = false;

		}


		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
		if (Physics.Raycast(ray, out hit, reach, itemsMask))
		{
			if (Input.GetMouseButtonDown(0)) {
				holdingObj = hit.collider.gameObject;
				holdingObj.GetComponent<Collider>().enabled = false;
				holdingObj.GetComponent<Rigidbody>().isKinematic = true;
				hit.collider.transform.SetParent(holdingTarget.transform);
			}
			
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
			crosshair.GetComponent<RawImage>().texture = crosshairSelected;
		}
		else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
			crosshair.GetComponent<RawImage>().texture = crosshairUnselected;
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
