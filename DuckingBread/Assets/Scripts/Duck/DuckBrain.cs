using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBrain : MonoBehaviour
{
    private DuckMovement duckMovement = null;

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

    public void RemoveSplashReference(Splash splash)
    {
        if (usedSplashes.Contains(splash))
            usedSplashes.Remove(splash);
    }
}
