using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FpsCounter : MonoBehaviour
{
    private float timer, refresh, avgFramerate;
    private TextMeshProUGUI fpsText;

    private void Start()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        //Change smoothDeltaTime to deltaTime or fixedDeltaTime to see the difference
        float timelapse = Time.deltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if (timer <= 0) 
            avgFramerate = (int)(1f / timelapse);

        fpsText.text = avgFramerate.ToString();
    }
    
}
