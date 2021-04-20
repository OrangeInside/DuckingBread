using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckHunger : MonoBehaviour
{
    [SerializeField] private int requiredHungryPoints = 3;

    private int currentHungryPoints = 0;

    public bool IsHungry() { return currentHungryPoints != requiredHungryPoints; }

    public void IncreaseHungryPoints()
    {
        currentHungryPoints++;

        if (currentHungryPoints == requiredHungryPoints)
        {
            //gain bonus life
        }

        if (currentHungryPoints > requiredHungryPoints)
            currentHungryPoints = requiredHungryPoints;
    }
}
