using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFalls : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().ShowDamage();
            other.gameObject.GetComponent<PlayerMovement>().Health = 0;
        }
    }
}
