using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType
{
    Good,
    Bad
}

public class Food : MonoBehaviour
{
    [SerializeField] private FoodType type;

    public FoodType Type { get => type; }

    public void ConsumeFood(DuckBrain consumer)
    {
        if (type == FoodType.Good)
        {
            consumer.DuckHunger?.IncreaseHungryPoints();
        }
        else if (type == FoodType.Bad)
        {
            GameController.Instance.DuckAteBread();
        }

        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        DucksManager.Instance?.RemoveFoodReference(this);
    }
}
