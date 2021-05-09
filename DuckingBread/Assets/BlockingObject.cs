using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingObject : MonoBehaviour
{
    public float speed=0.5f;
    public Vector3 posA;
    public Vector3 posB;
    public Vector3 nexPos;
    Splash splash;
    // Start is called before the first frame update
    void Start()
    {
        posA = transform.position;
        posB = transform.position;
        posB.y -= 2.5f;
       
        nexPos = posB;
        splash = this.GetComponent<Splash>();
    }

    // Update is called once per frame
    void Update()
    {
            StartCoroutine(Move());
          
       
    }
    public IEnumerator Move()
    {
        //Vector3 x = block.transform.position ;
        //x.y = Mathf.MoveTowards(x.y, nexPos.y, Time.deltaTime* speed);

        //block.transform.position = x; IEnumerator
        if (Vector3.Distance(transform.position, posA) <= 1)
        {
            splash.Detonate();
        }
        if (Vector3.Distance(transform.position, nexPos) <=0.0001)
        {
          
            yield return new WaitForSeconds(5);
            nexPos = nexPos != posA ? posA : posB;
          
           
            Debug.Log("Zrobione");
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position, nexPos, Time.deltaTime * speed);
        }
        
    }
 
}
