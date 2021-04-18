using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchSpawn : MonoBehaviour
{
    public GameObject prefab;
    public Camera MainCamera;

    [SerializeField] private LayerMask touchDetectionLayer;

    void Start()
    {
        MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, touchDetectionLayer))
            {
                Instantiate(prefab, hit.point, Quaternion.identity);
            }
           
        }
    }
}
