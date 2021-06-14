using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchSpawn : MonoBehaviour
{
    public GameObject prefab;

    [SerializeField] private LayerMask touchDetectionLayer;
    [SerializeField] private LayerMask blocktouchDetectionLayer;
   
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            RaycastHit hit1;
            RaycastHit hit2;
            Physics.Raycast(ray, out hit1, Mathf.Infinity, blocktouchDetectionLayer);
          
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, touchDetectionLayer) && hit1.point == Vector3.zero)
            {
               
                Instantiate(prefab, hit.point, Quaternion.identity);
            }
   
           
        }
    }
}
