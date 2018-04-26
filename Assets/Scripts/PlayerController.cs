using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

    public GameObject body;

    CharacterController my_controller;

    Animator body_animator;

	// Use this for initialization
	void Start () {
        my_controller = gameObject.GetComponent<CharacterController>();

        body_animator = body.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float move_h = Input.GetAxis("Horizontal");
        float move_v = Input.GetAxis("Vertical");

        //transform.Translate(move_h * Time.deltaTime, 0, move_v * Time.deltaTime);

        Vector3 new_move = new Vector3(move_h, 0, move_v) * speed * Time.deltaTime;

        my_controller.Move(new_move);

        Debug.Log(my_controller.velocity.magnitude);

        body_animator.SetFloat("Speed", my_controller.velocity.magnitude);
	}
}
