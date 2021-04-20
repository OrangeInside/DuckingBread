using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBrain : MonoBehaviour
{
    [SerializeField] private float distanceToStartEatingBread = 0.5f;
    [SerializeField] private float timeToEat = 1.0f;


    private DuckMovement duckMovement = null;

    private List<GameObject> foodInRange = new List<GameObject>();
    private GameObject foodTarget = null;

    private void Start()
    {
        DucksManager.Instance?.AddDuckReference(this);

        duckMovement = GetComponent<DuckMovement>();
    }

    private List<Splash> usedSplashes = new List<Splash>();

    private void OnTriggerEnter(Collider other)
    {
        Splash splash = other.GetComponent<Splash>();

        if (splash)
        {
            if (!usedSplashes.Contains(splash))
            {
                usedSplashes.Add(splash);
                duckMovement.ForcePathChange(this.transform.position - other.transform.position, 1.0f);
            }
        }
    }

    private bool isEating = false;

    private float currentEatingTime = 0.0f;

    private void Update()
    {
        if (foodTarget)
        {
            if (isEating)
            {
                currentEatingTime += Time.deltaTime;

                if (currentEatingTime >= timeToEat)
                {
                    isEating = false;
                    RemoveFoodReference(foodTarget);
                    duckMovement.EnableMovement();
                }
            }    
            else if (Vector3.Distance(foodTarget.transform.position, this.transform.position) < distanceToStartEatingBread && !isEating)
            {
                isEating = true;
                currentEatingTime = 0.0f;
                duckMovement.DisableMovement();
            }
        }
    }

    public void RemoveSplashReference(Splash splash)
    {
        if (usedSplashes.Contains(splash))
            usedSplashes.Remove(splash);
    }

    public void SetFoodInRange(GameObject food)
    {
        if (!foodInRange.Contains(food))
        {
            foodInRange.Add(food);

            SetClosestFoodAsTarget();
        }
    }

    private void SetClosestFoodAsTarget()
    {
        GameObject[] foodArray = foodInRange.ToArray();

        foreach (GameObject food in foodArray)
        {
            if (food == null)
                continue;

            if (foodTarget != null)
            {
                if (Vector3.Distance(this.transform.position, food.transform.position) < Vector3.Distance(this.transform.position, foodTarget.transform.position))
                {
                    foodTarget = food;
                }
            }
            else
            {
                foodTarget = food;
            }
        }

        if (foodTarget)
        {
            duckMovement?.SetFoodAsDestinationPoint(foodTarget);
        }
        else
        {
            duckMovement?.StopFollowingToFood();
        }
    }

    public void RemoveFoodReference(GameObject food)
    {
        if (foodTarget == food)
        {
            foodTarget = null;

            if (isEating)
            {
                isEating = false;
                duckMovement.EnableMovement();
            }
        }

        if (foodInRange.Contains(food))
            foodInRange.Remove(food);

        SetClosestFoodAsTarget();
    }
}
