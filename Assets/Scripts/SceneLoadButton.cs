using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadButton : MonoBehaviour
{
    public string SceneToLoad;

    public void Load()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}
