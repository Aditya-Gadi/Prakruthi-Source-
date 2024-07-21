using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public Vector3 endPos;

    bool isStanding = false;
    GameObject player;
    // Update is called once per frame
    void Update()
    {
        if(isStanding)
        {
            transform.position = Vector3.Lerp(transform.position, endPos, speed * Time.deltaTime);
            if(player != null)
                player.transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        }
    }

    IEnumerator makeItTrue()
    {
        yield return new WaitForSeconds(0.5f);
        isStanding = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            StartCoroutine(makeItTrue());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = null;
            isStanding = false;
        }
    }
}
