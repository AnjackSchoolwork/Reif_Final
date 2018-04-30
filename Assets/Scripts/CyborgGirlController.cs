using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyborgGirlController : MonoBehaviour {

    public GameObject parent_obj;
    
    public void fireEvent()
    {
        parent_obj.BroadcastMessage("firePrimary");
    }

    public void DeathEvent()
    {
        parent_obj.BroadcastMessage("onDeath");
    }
}
