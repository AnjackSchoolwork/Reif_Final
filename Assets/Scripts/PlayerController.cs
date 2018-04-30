using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float rotation_speed;
    public int health;

    public GameObject body;

    public Camera player_cam;
    public GameObject camera_pivot;

    public GameObject ammo_type;
    public GameObject bullet_spawn;

    public GameObject UI;

    private ScoreKeeper score_keeper;

    private float yaw;
    private float pitch;

    private Vector3 move_range;
    private bool is_aiming;

    Animator body_animator;
    Rigidbody rb;

	// Use this for initialization
	void Start () {

        body_animator = body.GetComponent<Animator>();

        is_aiming = body_animator.GetBool("Aiming");

        rb = gameObject.GetComponent<Rigidbody>();

        score_keeper = UI.GetComponent<ScoreKeeper>();
	}

    void setAimMode(bool should_be_aiming)
    {
        is_aiming = should_be_aiming;
        body_animator.SetBool("Aiming", is_aiming);
    }

    public void firePrimary()
    {
        Instantiate(ammo_type, bullet_spawn.transform.position, bullet_spawn.transform.rotation);
    }

    // Handle death
    void dieGracefully()
    {
        body_animator.SetTrigger("Death");
    }

    // Called by death animation
    void onDeath()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Called externally to handle damage
    public void takeHit(int dmg_amt)
    {
        health -= dmg_amt;
        score_keeper.update_health(health);

        if(health <= 0)
        {
            dieGracefully();
        }
        else
        {
            body_animator.SetTrigger("Damage");
        }
    }
	
	// Update is called once per frame
	void Update () {


        RaycastHit hit_info = new RaycastHit();
        Ray ground_check_ray = new Ray(transform.position, transform.TransformDirection(Vector3.down));

        gameObject.GetComponent<Collider>().Raycast(ground_check_ray, out hit_info, 10.0f);

        Debug.DrawRay(transform.position, new Vector3(0, -1, 0) * 100, Color.green, 5.0f, false);

        // Set camera rotation based upon mouse look (but only when right mouse button is held down)
        if (Input.GetMouseButtonDown(1) && !is_aiming)
        {
            setAimMode(true);
        }
        else if(Input.GetMouseButtonUp(1) && is_aiming)
        {
            setAimMode(false);
        }

        if(Input.GetMouseButtonDown(0))
        {
            setAimMode(true);
            body_animator.SetTrigger("Attack");
        }

        if(Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1))
        {
            setAimMode(false);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            body_animator.SetTrigger("Jump");
        }

        yaw += Input.GetAxis("Mouse X") * rotation_speed * Time.deltaTime;
        pitch += Input.GetAxis("Mouse Y") * rotation_speed * Time.deltaTime;

        transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y + yaw, 0.0f);
        camera_pivot.transform.eulerAngles = new Vector3(player_cam.transform.rotation.x - pitch, player_cam.transform.rotation.y + yaw, 0.0f);
            

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
