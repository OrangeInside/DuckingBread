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
            GameController.Instance.SeedEaten(this.gameObject);
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

        DucksManager.Instance?.RemoveFoodReference(this);
    }
}
