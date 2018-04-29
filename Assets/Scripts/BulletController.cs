using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float ballistic_force;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(Vector3.forward * ballistic_force, ForceMode.Force);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
