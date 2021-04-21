using System.Collections;
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

	// spawn control
	public bool onCommand = false;
	const float MinSpawnDelay = 1;
	const float MaxSpawnDelay = 5;
	Timer spawnTimer;

	// spawn location support
	float randomX;
	float randomY;
	float randomZ;

	#region Methods
    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
		plane = GameObject.FindWithTag("Walkable");

		// save spawn boundaries for efficiency
		float randomX = Random.Range (plane.transform.position.x - plane.transform.localScale.x / 2 + plane.transform.localScale.x / 9, plane.transform.position.x + plane.transform.localScale.x / 2 - plane.transform.localScale.x / 9);
		float randomY = Random.Range (plane.transform.position.y - plane.transform.localScale.y / 2, plane.transform.position.y + plane.transform.localScale.y / 2);
		float randomZ = Random.Range (plane.transform.position.y - plane.transform.localScale.z / 2 + plane.transform.localScale.z / 9, plane.transform.position.y + plane.transform.localScale.z / 2- plane.transform.localScale.z / 9);

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

	/// <summary>
	/// Spawns number of objects at a random location on a plane
	/// </summary>
	public void SpawnOnCommand(int objectsToSpawn)
    {
		IEnumerator spawningObjects = Spawning(objectsToSpawn);
		StartCoroutine(spawningObjects);
	
	}

	IEnumerator Spawning(int objectsToSpawn)
    {
		while(objectsToSpawn > 0)
        {
			ObjectSpawn();
			objectsToSpawn--;
			yield return new WaitForSeconds(Random.Range(1, 3));
		}
    }
	/// <summary>
	/// Spawns an object at a random location on a plane
	/// </summary>
	public void ObjectSpawn()
	{
		// generate random location and create new object
		Vector3 randomPosition = GetARandomPos(plane);
                                                                  
        Instantiate<GameObject>(spawnObject, randomPosition, Quaternion.identity);   
	
	}

	/// <summary>
	/// Return random position on the plane
	/// </summary>
	public Vector3 GetARandomPos(GameObject plane)
	{

		Mesh planeMesh = plane.GetComponent<MeshFilter>().mesh;
		Bounds bounds = planeMesh.bounds;

		float minX = plane.transform.position.x - plane.transform.localScale.x * bounds.size.x * 0.5f;
		float minZ = plane.transform.position.z - plane.transform.localScale.z * bounds.size.z * 0.5f;

		Vector3 newVec = new Vector3(Random.Range (minX, -minX),
									 plane.transform.position.y,
									 Random.Range (minZ, -minZ));
		return newVec;
	}

	#endregion
}
