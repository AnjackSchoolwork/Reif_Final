using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float ballistic_force;
    public int damage_amount;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * ballistic_force, ForceMode.Force);
	}

    private void OnCollisionEnter(Collision collision)
    {
        EnemyController ec_temp = collision.gameObject.GetComponent<EnemyController>();
        PlayerController pc_temp = collision.gameObject.GetComponent<PlayerController>();
        
        if (ec_temp != null)
        {
            ec_temp.takeHit(damage_amount);
        }
        else if (pc_temp != null)
        {
            pc_temp.takeHit(damage_amount);
        }

        Destroy(gameObject);
    }
}
