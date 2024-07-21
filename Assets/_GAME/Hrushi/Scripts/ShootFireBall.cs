using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFireBall : MonoBehaviour
{
    public GameObject fireBall;
    public float interval;

    float count = 0.0f;

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if(count >= interval)
        {
            Instantiate(fireBall, transform.position, transform.rotation);
            count = 0;
        }
    }
}
