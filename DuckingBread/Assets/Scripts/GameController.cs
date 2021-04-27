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
    public float feedingTime = 30;
    public int seedsToSpawn;
    public Spawner breadSpawner;
    public Spawner seedSpawner;
    public float currentFeedingTime = 0;
    public bool feeding = false;

    private void Awake()
    {
        GameController.Instance = this;
        Application.targetFrameRate = 60;
        if (timeToGetMaxSeed != 0)
            seedPerSecond = maxSeedLevel / timeToGetMaxSeed;
    }

    void Start()
    {
        ducks = GameObject.FindGameObjectsWithTag("Duck");
    }

    void Update()
    {
        if(seedLevel < maxSeedLevel && !feeding)
        {
            seedLevel += seedPerSecond * Time.deltaTime;
            if (seedLevel >= maxSeedLevel)
            {
                seedLevel = maxSeedLevel;

                //FindAndDestroyAllBreads();
                StopBreadSpawner();
                UIManager.Instance.ToggleSeedButton(true);
            }    

            UIManager.Instance.UpdateUI();
        }

        // no bread spawning during feeding time
        if(feeding)
        {
            currentFeedingTime -= Time.deltaTime;

            UIManager.Instance.UpdateTimer();
            if (currentFeedingTime < 0)
            {
                TurnOffFeedingTime();
            }

        }
    }
    public void FindAndDestroyAllBreads()
    {
        Food[] spawnedObjects = FindObjectsOfType<Food>();
        foreach (var spawnedObject in spawnedObjects)
        {
            if (spawnedObject.Type == FoodType.Bad)
            {
                GameObject.Destroy(spawnedObject.gameObject);
            }
        }
    }
    public void TurnOffFeedingTime()
    {
        feeding = false;
        DucksManager.Instance.ToggleHungryMeters(false);
        StartBreadSpawner();
        UIManager.Instance.ToggleTimer(false);
    }

    public void SeedEaten(GameObject seed)
    {
        seedSpawner.DeleteFromList(seed);
    }

    public void StopBreadSpawner()
    {
        breadSpawner.onCommand = true;
    }

    public void StartBreadSpawner()
    {
        breadSpawner.onCommand = false;
    }
    public void ClearSeedLevel()
    {
        seedLevel = 0;
        UIManager.Instance.UpdateUI();
        feeding = true;
        currentFeedingTime = feedingTime;
        seedSpawner.SpawnOnCommand(seedsToSpawn);
        DucksManager.Instance.ToggleHungryMeters(true);
        UIManager.Instance.ToggleTimer(true);

    }

    public void DuckAteBread()
    {
        if (lives > 0)
        {
            lives--;
            UIManager.Instance.UpdateLives();

            if (lives == 0)
                GameLost();
        }
    }

    public void GameLost()
    {
        UIManager.Instance.ToggleLostGameScreen(true);
    }

    public void GameWon()
    {
        UIManager.Instance.ToggleWonGameScreen(true);
    }
}
