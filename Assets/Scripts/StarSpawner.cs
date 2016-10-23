using UnityEngine;
using System.Collections;

/// <summary>
/// Spawn star prefabs randomly in a spherical space
/// </summary>
public class StarSpawner : MonoBehaviour
{
	public GameObject StarPrefab;
	public int MaxStars = 30;
	public float SpawnRadius = 10f;
	public GameObject StarParent;

	// Use this for initialization
	void Start ()
	{
		Spawn();
	}

	// Update is called once per frame
	void Update ()
	{

	}

	private void Spawn()
	{
		for (int i = 0; i < MaxStars; i++) 
		{
			GameObject star = Instantiate(StarPrefab, Random.insideUnitSphere*SpawnRadius, Quaternion.identity) as GameObject;
			star.transform.parent = StarParent.transform;
		}

		// Traverse all the stars, to search for colliding stars
	}
}
