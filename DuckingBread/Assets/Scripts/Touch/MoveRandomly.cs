using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MoveRandomly : MonoBehaviour
{

    NavMeshAgent navMeshAgent;
    NavMeshPath path;
    public float timeForNewPath;
    bool inCoRoutine;
    Vector3 target;
    bool validPath;
    public float Speed = 20f;
    float timer = 0;
    int seconds = 0;
    Vector3 oldPos;
    // Use this for initialization
    void Start()
    {
        oldPos = transform.position;
        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
      //  navMeshAgent.updateRotation = false;
 
    }

    // Update is called once per frame
    void Update()
    {
        //if (Mathf.Abs((transform.position - oldPos).x) < 0.01  && Mathf.Abs((transform.position - oldPos).y) < 0.01)
        //{
        //    timer += Time.deltaTime;
        //    seconds = (int)(timer % 60);
        //}

        //oldPos = transform.position;
        if (!inCoRoutine)
            StartCoroutine(DoSomething());

    }
  

    Vector3 getNewRandomPosition()
    // setting these ranges is vital larger seems better 
    {
        float x = Random.Range(this.transform.position.x-10, this.transform.position.x + 10) + Random.Range(0,100)/100;
        //   float y = Random.Range(-20, 20);
        float z = Random.Range(this.transform.position.z - 10, this.transform.position.z + 10) + Random.Range(0, 100) / 100;
        Vector3 pos = new Vector3(x, (float)0, z);
        return pos;

    }
    IEnumerator DoSomething()
    {
        //if (seconds > 5)
        //{
        //    this.transform.position = new Vector3(0, transform.position.y, 0);
        //    timer = 0;
        //}
            
        inCoRoutine = true;
        yield return new WaitForSeconds(timeForNewPath);
        GetNewPath();
        validPath = navMeshAgent.CalculatePath(target, path);
        if (navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
        }

        if (!validPath) Debug.Log("found invalid path");
        while (!validPath)
        {
            yield return new WaitForSeconds(0.01f);
            GetNewPath();
            validPath = navMeshAgent.CalculatePath(target, path);
        }

        inCoRoutine = false;
    }
    void GetNewPath()
    {
        target = getNewRandomPosition();
        navMeshAgent.SetDestination(target);
     
        //Quaternion rotTrget = Quaternion.LookRotation(target - this.transform.position);
        //this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotTrget, Speed * Time.deltaTime);

    }
}

