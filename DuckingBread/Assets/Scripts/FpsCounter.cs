using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour
{
    // Start is called before the first frame update

   

        public float timer, refresh, avgFramerate;
        string display = "{0} FPS";
    public Text m_Text;

        private void Start()
        {
            m_Text = GetComponent<Text>();
        }


        private void Update()
        {
            //Change smoothDeltaTime to deltaTime or fixedDeltaTime to see the difference
            float timelapse = Time.deltaTime;
            timer = timer <= 0 ? refresh : timer -= timelapse;

            if (timer <= 0) avgFramerate = (int)(1f / timelapse);
            m_Text.text = string.Format(display, avgFramerate.ToString());
        }
    
}
