using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyborgGirlController : MonoBehaviour {

    public GameObject parent_obj;

	// Use this for initialization
	void Start () {
		
	}

    public void fireEvent()
    {
        parent_obj.BroadcastMessage("firePrimary");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
