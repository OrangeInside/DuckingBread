using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchSpawn : MonoBehaviour
{
    public GameObject prefab;
    public Camera MainCamera;
    // Start is called before the first frame update
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
            if(Physics.Raycast(ray,out hit))
            {
                Instantiate(prefab, new Vector3(hit.point.x, hit.point.y +prefab.transform.position.y+5, hit.point.z), Quaternion.identity);
            }
           
        }
    }
}
