using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int lives = 3;
    public GameObject[] ducks;
    // Start is called before the first frame update
    void Start()
    {
        ducks = GameObject.FindGameObjectsWithTag("Duck");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
