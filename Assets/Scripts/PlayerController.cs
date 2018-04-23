using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float rotation_speed;

    public GameObject body;

    public Camera player_cam;

    private float yaw;
    private float pitch;

    private KeyCode sprint_key;

    CharacterController my_controller;

    Animator body_animator;

	// Use this for initialization
	void Start () {
        my_controller = gameObject.GetComponent<CharacterController>();

        body_animator = body.GetComponent<Animator>();

        // Temporary config section (need to move to independent or player perfs)
        sprint_key = KeyCode.LeftShift;
	}
	
	// Update is called once per frame
	void Update () {
        float delta_h = Input.GetAxis("Horizontal");
        float delta_v = Input.GetAxis("Vertical");


        // Set camera rotation based upon mouse look (but only when right mouse button is held down)
        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * rotation_speed * Time.deltaTime;
            pitch += Input.GetAxis("Mouse Y") * rotation_speed * Time.deltaTime;

            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y + yaw, 0.0f);
            player_cam.transform.eulerAngles = new Vector3(player_cam.transform.rotation.x - pitch, player_cam.transform.rotation.y + yaw, 0.0f);

        }

        // Move set character speed and move them
        //Vector3 new_move = new Vector3(move_h, 0, move_v) * speed * Time.deltaTime;

        float adjusted_speed = speed;

        // Sprint key
        if(Input.GetKey(sprint_key))
        {
            adjusted_speed = speed * 2;
        }

        Vector3 move_h = transform.right * delta_h * adjusted_speed * Time.deltaTime;
        Vector3 move_v = transform.forward * delta_v * adjusted_speed * Time.deltaTime;

        Vector3 new_move = move_h + move_v;

        my_controller.Move(new_move);

        body_animator.SetFloat("Speed", my_controller.velocity.magnitude);
	}
}
