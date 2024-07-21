using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public enum toDo { play, cerdits, controls, exit };
    public toDo func;

    public GameObject cerdits;
    public GameObject controls;

    private void OnMouseDown()
    {
        switch(func)
        {
            case toDo.play:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case toDo.cerdits:
                cerdits.SetActive(true);
                break;
            case toDo.controls:
                controls.SetActive(true);
                break;
            case toDo.exit:
                Application.Quit();
                break;
        }
    }
}
