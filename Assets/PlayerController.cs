using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float drag;
	private Rigidbody rb;




	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody>();
		
	}

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector3(Input.GetAxisRaw("Horizontal")* speed, 0, Input.GetAxisRaw("Horizontal") * speed), ForceMode.Impulse);
		rb.AddForce(new Vector3(rb.velocity.x  * -drag, 0, rb.velocity.z  * -drag), ForceMode.Impulse);
    }
}
