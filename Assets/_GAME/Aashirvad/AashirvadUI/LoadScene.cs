using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadSceneOnClick(string scenenumber)
    {
        SceneManager.LoadScene(scenenumber);
    }
    public void ExitSceneOnClick()
    {
        Application.Quit();
    }
    
}
