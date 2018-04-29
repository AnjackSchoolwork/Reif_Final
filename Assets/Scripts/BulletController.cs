using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float ballistic_force;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * ballistic_force, ForceMode.Force);
	}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
