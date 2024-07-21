using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered");
            other.gameObject.GetComponent<PlayerMovement>().ShowDamage();
            other.gameObject.GetComponent<PlayerMovement>().Health = 0;
        }

        if(other.gameObject.tag == "Body")
        {
            Debug.Log("Body Entered");
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
