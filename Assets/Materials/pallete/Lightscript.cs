using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightscript : MonoBehaviour
{
    public Light LocaleLight;
    // Start is called before the first frame update
    void Start()
    {
        LocaleLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        LocaleLight.enabled = true;
    }
}
