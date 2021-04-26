using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchSpawn : MonoBehaviour
{
    public GameObject prefab;

    [SerializeField] private LayerMask touchDetectionLayer;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, touchDetectionLayer))
            {
                Instantiate(prefab, hit.point, Quaternion.identity);
            }
           
        }
    }
}
