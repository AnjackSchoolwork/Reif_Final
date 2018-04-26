﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float rotation_speed;

    public GameObject body;

    public Camera player_cam;

    private float yaw;
    private float pitch;

    private Vector3 move_range;

    Animator body_animator;
    Rigidbody rb;

	// Use this for initialization
	void Start () {

        body_animator = body.GetComponent<Animator>();

        rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit_info = new RaycastHit();
        Ray ground_check_ray = new Ray(transform.position, transform.TransformDirection(Vector3.down));

        gameObject.GetComponent<Collider>().Raycast(ground_check_ray, out hit_info, 10.0f);

        Debug.DrawRay(transform.position, new Vector3(0, -1, 0) * 100, Color.green, 5.0f, false);

        // Set camera rotation based upon mouse look (but only when right mouse button is held down)
        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * rotation_speed * Time.deltaTime;
            pitch += Input.GetAxis("Mouse Y") * rotation_speed * Time.deltaTime;

            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y + yaw, 0.0f);
            player_cam.transform.eulerAngles = new Vector3(player_cam.transform.rotation.x - pitch, player_cam.transform.rotation.y + yaw, 0.0f);

        }

        // If we need to alter the speed for any reason (environment, buffs/debuffs, etc) we shouldn't change the default value
        float adjusted_speed = speed;

        // Get movement input
        float delta_right = Input.GetAxis("Horizontal");
        float delta_forward = Input.GetAxis("Vertical");
        
        // Get new position
        // TODO: Check if new spot collides with anything before moving there
        Vector3 new_move = new Vector3(delta_right, 0, delta_forward) * adjusted_speed * Time.deltaTime;
        
        // This is how fast we appear to be going in real time
        float observed_speed = new_move.magnitude / Time.deltaTime;

        // To prevent "skiing"
        // Need to clamp speed

        // Move us to the new position (need collision check)
        transform.Translate(new_move);

        // Let the animation controller know how fast we appear to be going
        body_animator.SetFloat("Speed", observed_speed);
	}

    private void OnCollisionEnter(Collision collision)
    {

    }
}
