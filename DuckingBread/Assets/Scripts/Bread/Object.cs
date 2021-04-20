using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A enemy
/// </summary>
public class Object : MonoBehaviour
{
    #region Variables
    // death support
    const float EnemyLifespanSeconds = 10;
    Timer deathTimer;

    #endregion

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    { 
        // create and start timer
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = EnemyLifespanSeconds;
        deathTimer.Run(); 
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // destroy enemy if death timer finished
        if(deathTimer){
            if (deathTimer.Finished)
            {    
                Destroy(gameObject);       
            }
        }
        
    }
    
}
