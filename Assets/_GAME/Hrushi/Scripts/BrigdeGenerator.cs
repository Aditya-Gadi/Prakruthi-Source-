using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigdeGenerator : MonoBehaviour
{
    public enum dir {x, Z, NegX, NegZ};

    public dir direction;
    public GameObject bridgeModel;
    public int numberOfSteps;
    List<GameObject> bridges = new List<GameObject>();

    bool canGenerate = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rock")
        {
            canGenerate = true;
        }

        if (other.gameObject.tag == "Player")
        {
            if (canGenerate)
            {
                if (transform.childCount == 0)
                {
                    StartCoroutine(GenerateBridge());
                }
            }
            else
            {
                other.gameObject.GetComponent<PlayerMovement>().ShowDamage();
                other.gameObject.GetComponent<PlayerMovement>().Health = 0f;
            }                
        }
    }

    IEnumerator GenerateBridge()
    {
        Vector3 pos = Vector3.zero;
        switch (direction)
        {
            case dir.x:
                pos = new Vector3(transform.position.x + 1f, transform.position.y - 2f, transform.position.z);
                break;
            case dir.Z:
                pos = new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z + 1f);
                break;
            case dir.NegX:
                pos = new Vector3(transform.position.x - 1f, transform.position.y - 2f, transform.position.z);
                break;
            case dir.NegZ:
                pos = new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z - 1f);
                break;
            default:
                pos = new Vector3(transform.position.x + 1f, transform.position.y - 2f, transform.position.z);
                break;
        }
        //Vector3 pos = new Vector3(transform.position.x + 1f, transform.position.y - 2f, transform.position.z);
        for (int i = 0; i < numberOfSteps; i++)
        {
            GameObject obj = Instantiate(bridgeModel, pos, Quaternion.identity);
            obj.transform.parent = gameObject.transform;
            bridges.Add(obj);
            switch (direction)
            {
                case dir.x:
                    pos.x += 1f;
                    break;
                case dir.Z:
                    pos.z += 1f;
                    break;
                case dir.NegX:
                    pos.x -= 1f;
                    break;
                case dir.NegZ:
                    pos.z -= 1f;
                    break;
                default:
                    pos.x += 1f;
                    break;
            }
        }
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(BridgeUp());
    }

    Vector3 switchAxis(Vector3 pos)
    {
        switch(direction)
        {
            case dir.x:
                pos.x += 1f;
                break;
            case dir.Z:
                pos.z += 1f;
                break;
            case dir.NegX:
                pos.x -= 1f;
                break;
            case dir.NegZ:
                pos.z -= 1f;
                break;
            default:
                pos.x += 1f;
                break;
        }
        return pos;
    }

    IEnumerator BridgeUp()
    {
        float count = 0f;
        //count += Time.deltaTime;
        for (int i = 0; i < bridges.Count; i++)
        {            
            Vector3 newPos = new Vector3(bridges[i].transform.position.x, 0.75f, bridges[i].transform.position.z);
            while(count < 0.5f)
            {
                bridges[i].transform.position = Vector3.Lerp(bridges[i].transform.position, newPos, count / 0.5f);
                count += Time.deltaTime;
                yield return null;
            }
            count = 0f;
        }
    }
}
