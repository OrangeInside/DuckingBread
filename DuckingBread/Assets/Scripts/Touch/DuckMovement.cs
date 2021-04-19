using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DuckMovement : MonoBehaviour
{
    [Header("Movement parameters:")]
    [SerializeField] private Vector2 minXZdestinationPoint;
    [SerializeField] private Vector2 maxXZdestinationPoint;
    [SerializeField] private float minDistanceToReachPoint = 0.5f;
    [SerializeField] private float minDistanceBetweenNextPoint = 10.0f;
    [SerializeField] private float brakeDistance = 2.0f;
    [Space(10)]
    [SerializeField] private float minSpeed = 0.8f;
    [SerializeField] private float maxSpeed = 1.1f;
    [SerializeField] private float minIdleStateTime = 0.25f;
    [SerializeField] private float maxIdleStateTime = 0.5f;

    private Vector3 destinationPoint = Vector3.zero;
    private NavMeshAgent navMeshAgent = null;
    private float speed = 3.0f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetDestinationPoint();
    }

    private bool destPointReached = false;

    private void Update()
    {
        float distanceToDestPoint = Vector3.Distance(destinationPoint, this.transform.position);

        if (distanceToDestPoint < minDistanceToReachPoint)
        {
            SetDestinationPoint();
        }
        else if (distanceToDestPoint < brakeDistance)
        {
            if (navMeshAgent)
            {
                navMeshAgent.speed = speed * (distanceToDestPoint / brakeDistance);
            }
        }
        else if (navMeshAgent && navMeshAgent.speed < speed)
        {
            navMeshAgent.speed += Time.deltaTime;
            navMeshAgent.speed = Mathf.Min(navMeshAgent.speed, speed);
        }
    }

    private void SetDestinationPoint()
    {
        Vector3 previousDestinationPoint = destinationPoint;

        do
        {
            destinationPoint = GetDestinationPoint();

        } while (Vector3.Distance(previousDestinationPoint, destinationPoint) < minDistanceBetweenNextPoint);

        if (navMeshAgent)
        {
            navMeshAgent.SetDestination(destinationPoint);
        }
    }

    private Vector3 GetDestinationPoint()
    {
        Vector3 point = Vector3.zero;
        point.x = Random.Range(minXZdestinationPoint.x, maxXZdestinationPoint.x);
        point.z = Random.Range(minXZdestinationPoint.y, maxXZdestinationPoint.y);
        point.y = this.transform.position.y;

        return point;
    }
}

