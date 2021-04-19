using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    public float power = 3.0f;
    public float radius = 5.0f;
    public float upforce = 1.0f;
    public int destroyTime = 1;
  

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<AudioManager>().PlayRandomSound();
        this.Detonate();
    }

    // Update is called once per frame
    void Update()
    {
        this.Detonate();
        Destroy(gameObject, destroyTime);
    }

    void Detonate()
    {
        Vector3 explosionPosition = this.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        
        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(power, explosionPosition, radius, upforce, ForceMode.VelocityChange);
            }
        }
    }

    private void OnDestroy()
    {
        DucksManager.Instance?.RemoveSplashReference(this);
    }
}
