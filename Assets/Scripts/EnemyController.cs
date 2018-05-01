using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public int health;
    public float perception_range;
    public GameObject ammo_type;
    public GameObject bullet_spawn;
    public GameObject patrol_dest;

    public int score_value;

    public GameObject UI;

    protected ScoreKeeper score_keeper;

    protected bool engaging_target = false;
    protected bool dead = false;

    NavMeshAgent nav_agent;
    Animator my_animator;
    GameObject player;

	// Use this for initialization
	void Start () {
        my_animator = GetComponent<Animator>();

        nav_agent = GetComponent<NavMeshAgent>();

        nav_agent.destination = patrol_dest.transform.position;

        nav_agent.stoppingDistance = 1;

        player = GameObject.FindGameObjectWithTag("player");

        score_keeper = UI.GetComponent<ScoreKeeper>();
	}

    // Called internally when character dies
    void dieGracefully()
    {
        // Stop navigating

        nav_agent.enabled = false;
        // Call death animation
        my_animator.SetTrigger("Death");
        GetComponent<CapsuleCollider>().enabled = false;

        // Increment score
        score_keeper.increment_score(score_value);

        dead = true;
    }

    // Event called from death animation (for cleanup if necessary)
    public void onDeath()
    {
    }

    // Called externally to attempt to apply damage
    public void takeHit(int dmg_amt)
    {
        health -= dmg_amt;
        if (health <= 0)
        {
            dieGracefully();
        }
        else
        {
            my_animator.SetTrigger("Damage");
        }
    }

    // Rotate toward the target
    // TODO: Check line-of-sight to target
    // TODO: Determine attack type based on circumstances
    void acquireTarget(GameObject target)
    {
        transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y - 0.5f, player.transform.position.z));
        engaging_target = true;
        if(!my_animator.GetBool("Aiming"))
        {
            my_animator.SetBool("Aiming", true);

        }
    }

    void loseTarget()
    {
        engaging_target = false;

        if(my_animator.GetBool("Aiming"))
        {
            my_animator.SetBool("Aiming", false);
        }
    }

    // Fire bullet from primary weapon spawn point, we should already be aimed at our target.
    // TODO: Add scatter to reduce accuracy
    void fireWeapon()
    {
        Debug.Log("FIRING!");
        Instantiate(ammo_type, bullet_spawn.transform.position, bullet_spawn.transform.rotation);
    }

    // Called from animation to change attack state
    public void attackEnd()
    {
        Debug.Log("ATTACK END");
        if(engaging_target)
        {
            my_animator.ResetTrigger("Attack");
            my_animator.SetTrigger("Attack");
        }
    }

    private void Update()
    {
        if (!dead)
        {
            my_animator.SetFloat("Speed", nav_agent.velocity.magnitude);

            if (Vector3.Distance(transform.position, player.transform.position) <= perception_range)
            {
                if (nav_agent.isActiveAndEnabled && !nav_agent.isStopped)
                {
                    nav_agent.isStopped = true;
                }
                if (!engaging_target)
                {
                    acquireTarget(player);
                    Invoke("attackEnd", 0.75f);
                }
                else
                {
                    acquireTarget(player);
                }
            }
            else if (engaging_target)
            {
                loseTarget();
            }
        }
    }
}
