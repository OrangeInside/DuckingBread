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
    [SerializeField] private float maxTimeToReachDestination = 10.0f;
    [Space(10)]
    [SerializeField] private float minSpeed = 0.8f;
    [SerializeField] private float maxSpeed = 1.1f;
    [SerializeField] private float minIdleStateTime = 0.25f;
    [SerializeField] private float maxIdleStateTime = 0.5f;

    private Vector3 destinationPoint = Vector3.zero;
    private NavMeshAgent navMeshAgent = null;
    private float speed = 3.0f;
    private float currentTimeToReachDestination = 0.0f;

    private GameObject foodTarget = null;

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
        if (customPathForced)
        {
            customPathCurrentTime += Time.deltaTime;
            navMeshAgent.SetDestination(this.transform.position + customPathDirection);
            navMeshAgent.speed = (speed * 3.0f) * (customPathDuration - customPathCurrentTime / customPathDuration);

            if (customPathCurrentTime >= customPathDuration)
            {
                customPathForced = false;
                ResetMovement();
            }
        }
        else
        {
            if (hasFoodAsDestinationPoint && foodTarget)
            {
                float distToFood = Vector3.Distance(foodTarget.transform.position, this.transform.position);

                if (distToFood < brakeDistance)
                {
                    if (navMeshAgent)
                    {
                        navMeshAgent.speed = speed * (distToFood / brakeDistance);
                    }
                }
                else if (navMeshAgent && navMeshAgent.speed < speed)
                {
                    navMeshAgent.speed += Time.deltaTime;
                    navMeshAgent.speed = Mathf.Min(navMeshAgent.speed, speed);
                }

                return;
            }

            currentTimeToReachDestination += Time.deltaTime;

            if (currentTimeToReachDestination > maxTimeToReachDestination)
            {
                destinationPoint = this.transform.position;
                SetDestinationPoint();
            }

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
    }

    private void SetDestinationPoint()
    {
        Vector3 previousDestinationPoint = destinationPoint;

        speed = Random.Range(minSpeed, maxSpeed);

        do
        {
            destinationPoint = GetDestinationPoint();

        } while (Vector3.Distance(previousDestinationPoint, destinationPoint) < minDistanceBetweenNextPoint);

        if (navMeshAgent)
        {
            navMeshAgent.SetDestination(destinationPoint);
        }

        currentTimeToReachDestination = 0.0f;
    }

    private void ResetMovement()
    {
        if (foodTarget)
            navMeshAgent.SetDestination(foodTarget.transform.position); 
        else
            navMeshAgent.SetDestination(destinationPoint);
    }

    private Vector3 GetDestinationPoint()
    {
        Vector3 point = Vector3.zero;
        point.x = Random.Range(minXZdestinationPoint.x, maxXZdestinationPoint.x);
        point.z = Random.Range(minXZdestinationPoint.y, maxXZdestinationPoint.y);
        point.y = this.transform.position.y;

        return point;
    }

    private bool customPathForced = false;
    private float customPathCurrentTime = 0.0f;
    private float customPathDuration = 0.0f;
    private Vector3 customPathDirection = Vector3.zero;
    public void ForcePathChange(Vector3 direction, float time)
    {
        direction.y = 0.0f;

        customPathForced = true;

        customPathCurrentTime = 0.0f;
        customPathDuration = time;
        customPathDirection = direction.normalized;

        navMeshAgent.SetDestination(this.transform.position + customPathDirection);
    }

    private bool hasFoodAsDestinationPoint = false;

    public void SetFoodAsDestinationPoint(GameObject food)
    {
        hasFoodAsDestinationPoint = true;
        foodTarget = food;
        navMeshAgent.SetDestination(foodTarget.transform.position);
    }

    public void StopFollowingToFood()
    {
        hasFoodAsDestinationPoint = false;
        foodTarget = null;
    }

    public void DisableMovement()
    {
        navMeshAgent.isStopped = true;
    }

    public void EnableMovement()
    {
        navMeshAgent.isStopped = false;
    }
}

