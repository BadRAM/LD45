using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanagement : MonoBehaviour
{
    public bool Menuscreen;
    public Light TheLight;

    Ray MouseDetect;
    RaycastHit hit;

    private void Start()
    {
        TheLight = TheLight.GetComponent<Light>();
    }
    private void Update()
    {
        
        if (Menuscreen == true)
        {
            
            MouseDetect = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(MouseDetect,out hit))
            {
                //print(hit.collider.name);
                if(hit.collider.name == "BTN1")
                {
                    TheLight.enabled = true;
                    TheLight.color = Color.green;
                    if (Input.GetMouseButtonDown(0))
                    {
                        LoadScene(1);
                    }
                }
                else
                {
                    TheLight.enabled = false;
                }

                if (hit.collider.name == "BTN2")
                {
                    TheLight.enabled = true;
                    TheLight.color = Color.red;
                    if (Input.GetMouseButtonDown(0))
                    {
                        Application.Quit();
                    }
                }
            }
        }
    }
  
  

    void LoadScene(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber, LoadSceneMode.Single);
    }
}
