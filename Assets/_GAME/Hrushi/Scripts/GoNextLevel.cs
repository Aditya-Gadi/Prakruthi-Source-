using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoNextLevel : MonoBehaviour
{

    public Animator fadeout;
    public int collect;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(other.gameObject.GetComponent<PlayerMovement>().collected == collect)
            {
                fadeout.enabled = true;
                StartCoroutine(GotoNext());
            }
            else
            {
                StartCoroutine(textAppear(other.gameObject));
            }
        }
    }

    IEnumerator textAppear(GameObject go)
    {
        go.GetComponent<PlayerMovement>().InfoText.text = "You need to collect all the Crystals first!";
        yield return new WaitForSeconds(2f);
        go.GetComponent<PlayerMovement>().InfoText.text = "";
    }

    IEnumerator GotoNext()
    {        
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
