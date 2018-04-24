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

        gameObject.GetComponent<Collider>().Raycast(new Ray(transform.position, transform.TransformDirection(Vector3.down)), out hit_info, 10.0f);

        Debug.Log(hit_info.distance);

        // Set camera rotation based upon mouse look (but only when right mouse button is held down)
        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * rotation_speed * Time.deltaTime;
            pitch += Input.GetAxis("Mouse Y") * rotation_speed * Time.deltaTime;

            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y + yaw, 0.0f);
            player_cam.transform.eulerAngles = new Vector3(player_cam.transform.rotation.x - pitch, player_cam.transform.rotation.y + yaw, 0.0f);

        }

        // Set character speed and move them
        float adjusted_speed = speed;

        float delta_right = Input.GetAxis("Horizontal");
        float delta_forward = Input.GetAxis("Vertical");

        Vector3 new_move = new Vector3(delta_right, 0, delta_forward) * adjusted_speed * Time.deltaTime;

        // To prevent "skiing"

        float observed_speed = new_move.magnitude / Time.deltaTime;

        Debug.Log(observed_speed);

        // Move us to the new position (need collision check)
        transform.Translate(new_move);

        body_animator.SetFloat("Speed", observed_speed);
	}

    private void OnCollisionEnter(Collision collision)
    {

    }
}
