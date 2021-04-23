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
    public GameObject fracturedFood = null;

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
        if (fracturedFood)
        {
            GameObject destroyedFood = Instantiate(fracturedFood, this.transform.position, fracturedFood.transform.rotation);
            Destroy(destroyedFood, 1.1f);
        }

        if(type == FoodType.Good)
        {
            GameController.Instance.SeedEaten(this.gameObject);
        }

        DucksManager.Instance?.RemoveFoodReference(this);
    }
}
