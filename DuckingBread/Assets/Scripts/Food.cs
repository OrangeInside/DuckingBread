﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnDestroy()
    {
        DucksManager.Instance?.RemoveFoodReference(this.gameObject);
    }
}
