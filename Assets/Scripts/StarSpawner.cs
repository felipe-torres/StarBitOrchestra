using UnityEngine;
using System.Collections;

/// <summary>
/// Spawn star prefabs randomly in a spherical space
/// </summary>
public class StarSpawner : MonoBehaviour
{
	public static StarSpawner Instance;
	public GameObject StarPrefab;
	public int MaxStars = 30;
	public float SpawnRadius = 10f;
	public GameObject StarParent;

	public enum StarType { Melody, Bass, Harmony };


	public Color MelodyStarColor;
	public Color BassStarColor;
	public Color HarmonyStarColor;

	private void Awake()
	{
		Instance = this;
	}

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
			GameObject star = Instantiate(StarPrefab, Random.insideUnitSphere * SpawnRadius, Quaternion.identity) as GameObject;
			star.transform.parent = StarParent.transform;

			// Mutate stars
			star.GetComponent<Star>().MutateToType(GetRandomStarType());
		}

		// Traverse all the stars, to search for colliding stars
	}

	private StarType GetRandomStarType()
	{
		int r = Random.Range(0, 3);
		if (r == 0)
		{
			return StarType.Melody;
		}
		else if (r == 1)
		{
			return StarType.Bass;
		}
		else
		{
			return StarType.Harmony;
		}
	}
}
