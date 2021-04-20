using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDetector : MonoBehaviour
{
    private DuckBrain brain = null;

    private void Start()
    {
        brain = GetComponentInParent<DuckBrain>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            brain?.SetFoodInRange(other.gameObject);
        }
    }
}
