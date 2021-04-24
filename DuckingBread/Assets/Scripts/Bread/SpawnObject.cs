﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An object spawner
/// </summary>
public class SpawnObject : MonoBehaviour
{
	// needed for spawning
	[SerializeField]
	GameObject spawnObject;

	[SerializeField]
	GameObject plane;

	public int amountOfSpawns;

	private List<GameObject> spawnedObjects;

	// spawn control
	public bool onCommand = false;
	const float MinSpawnDelay = 1;
	const float MaxSpawnDelay = 5;
	Timer spawnTimer;
	public bool spawnedAll = false;
	Vector3 checker;
	#region Methods
	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
    {
		plane = GameObject.FindWithTag("Walkable");

		// create and start timer
		spawnTimer = gameObject.AddComponent<Timer>();
		spawnTimer.Duration = Random.Range(MinSpawnDelay, MaxSpawnDelay);
		spawnTimer.Run();
	}

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
		if(!onCommand)
		// check for time to spawn a new enemy
		if (spawnTimer.Finished)
        {
			ObjectSpawn();

			// change spawn timer duration and restart
			spawnTimer.Duration = Random.Range(MinSpawnDelay, MaxSpawnDelay);
			spawnTimer.Run();
		}
		
	}

	public void DeleteFromList(GameObject givenObject)
    {
		spawnedObjects.Remove(givenObject);
		if (GameController.Instance.feeding && spawnedObjects.Count < 2)
			SpawnNextObject();
		//if (spawnedAll && spawnedObjects.Count == 0)
		//	GameController.Instance.TurnOffFeedingTime();
    }

	/// <summary>
	/// Spawns number of objects at a random location on a plane
	/// </summary>
	public void SpawnOnCommand(int objectsToSpawn)
    {
		spawnedObjects = new List<GameObject>();
		spawnedAll = false;
		IEnumerator spawningObjects = Spawning(objectsToSpawn);
		StartCoroutine(spawningObjects);
	
	}

	IEnumerator Spawning(int objectsToSpawn)
    {
		while(objectsToSpawn > 0)
        {
			spawnedObjects.Add(ObjectSpawn());
			objectsToSpawn--;
			yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
		}
		//spawnedAll = true;
    }

	public void SpawnNextObject()
    {
		spawnedObjects.Add(ObjectSpawn());

	}
	/// <summary>
	/// Spawns an object at a random location on a plane
	/// </summary>
	public GameObject ObjectSpawn()
	{
		// generate random location and create new object
		Vector3 randomPosition = GetARandomPos(plane);
                                                                  
        return Instantiate<GameObject>(spawnObject, randomPosition, Quaternion.identity);   
	
	}

	/// <summary>
	/// Return random position on the plane
	/// </summary>
	public Vector3 GetARandomPos(GameObject plane)
	{

		Mesh planeMesh = plane.GetComponent<MeshFilter>().mesh;
		Bounds bounds = planeMesh.bounds;
		Vector3 newVec;
		
		float minX = plane.transform.position.x - plane.transform.localScale.x * bounds.size.x * 0.5f;
		float minZ = plane.transform.position.z - plane.transform.localScale.z * bounds.size.z * 0.5f;

		do
		{

			newVec = new Vector3(Random.Range(minX + 1, (-minX) - 1),
									plane.transform.position.y,
									Random.Range(minZ + 1, -minZ - 1));
			checker = newVec;
			checker.y += 2f;
		} while (!Checkforfreespace(checker));
		
		


		return newVec;
	}
	public bool Checkforfreespace(Vector3 v)
	{
		float maxDistance =5f;
		RaycastHit hit;

		bool isHit =
			Physics.BoxCast(v, spawnObject.transform.lossyScale , new Vector3(0,-1,0), out hit,
			spawnObject.transform.rotation, maxDistance);
		if (isHit)
		{
			Debug.Log("Trafiłem");
			if (hit.transform.tag == "Food" || hit.transform.tag == "Ground" || hit.transform.tag == "Duck")
			{
				Debug.Log("to");
				
				return false;
			}
			else
				return true;
			
		}
		
		return true;
	}
	#endregion
}
