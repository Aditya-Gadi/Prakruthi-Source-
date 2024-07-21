using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    GameObject player;
    public GameObject playerDeadBody;
    public float secondsToRevert;


    List<PlayerTransform> playerTransform;
    bool isRewinding = false;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = new List<PlayerTransform>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Pressed " + playerTransform[playerTransform.Count - 2]);
            PlayerTransform playerTrans = playerTransform[playerTransform.Count - 2];
            Vector3 pos = playerTrans.pos;
            instDead();
            player.transform.position = pos;
            player.transform.rotation = playerTrans.rot;
        }
    }

    void instDead()
    {
        Instantiate(playerDeadBody, player.transform.position, player.transform.rotation);
    }

    void Record()
    {
        if (playerTransform.Count > Mathf.RoundToInt(secondsToRevert / Time.fixedDeltaTime))
        {
            playerTransform.RemoveAt(playerTransform.Count - 1);
        }
        playerTransform.Insert(0, new PlayerTransform(player.transform.position, player.transform.rotation));
    }

    private void FixedUpdate()
    {
        Record();
    }
}
