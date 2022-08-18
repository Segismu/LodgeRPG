using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProInput : MonoBehaviour
{
    public PostProcessVolume volume;
    ColorGrading colorGrading;
    bool itsCold = true;

    void Start()
    {

        volume.profile.TryGetSettings(out colorGrading);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && itsCold == true)
        {
            colorGrading.temperature.value = 0;
            itsCold = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && itsCold == false)
        {
            colorGrading.temperature.value = -55;
            itsCold = true;
        }
    }
}
