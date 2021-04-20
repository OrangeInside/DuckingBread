using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance = null;
    public int lives = 3;
    public GameObject[] ducks;

    public float timeToGetMaxSeed;
    public float maxSeedLevel = 100;
    public float seedPerSecond = 0.5f;
    public float seedLevel = 0f;

    private void Awake()
    {
        GameController.Instance = this;
        if (timeToGetMaxSeed != 0)
            seedPerSecond = maxSeedLevel / timeToGetMaxSeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        ducks = GameObject.FindGameObjectsWithTag("Duck");
    }

    // Update is called once per frame
    void Update()
    {
        if(seedLevel < maxSeedLevel)
        {
            seedLevel += seedPerSecond * Time.deltaTime;
            if (seedLevel >= maxSeedLevel)
                seedLevel = maxSeedLevel;

            UIManager.Instance.UpdateUI();
        }
    }
}
