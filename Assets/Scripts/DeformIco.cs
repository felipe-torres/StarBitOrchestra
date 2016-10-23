using UnityEngine;
using System.Collections;

/// <summary>
/// Deforms the ico sphere mesh
/// </summary>
public class DeformIco : MonoBehaviour
{
	private Mesh mesh;
	// Use this for initialization
	void Start ()
	{
		mesh = GetComponent<MeshFilter>().mesh;
	}

	// Update is called once per frame
	void Update ()
	{
		Vector3 n = Quaternion.identity * Vector3.zero;
		Vector3[] vertices = mesh.vertices;
		Vector3[] normals = mesh.normals;
		int i = 0;
		int triIndex = 0;
		while (i < vertices.Length)
		{
			/*
			if(i % 3 == 0)
				vertices[i] += -normals[i] * Mathf.Cos(Time.time+vertices[i].x+vertices[i].z+vertices[i].y) * 0.000025f;
			else
				vertices[i] += -normals[i] * Mathf.Sin(Time.time+vertices[i].x+vertices[i].z+vertices[i].y) * 0.000025f;
			*/
			if (triIndex == 0)
				vertices[i] += -normals[i] * Mathf.Sin(Time.time+normals[i].x) * 0.000025f;
			else if (triIndex == 1)
				vertices[i] += -normals[i] * Mathf.Sin(Time.time+normals[i].y) * 0.000025f;
			else
				vertices[i] += -normals[i] * Mathf.Sin(Time.time+normals[i].z) * 0.000025f;
			triIndex++;
			if(triIndex > 2) triIndex = 0;

			i++;
		}
		mesh.vertices = vertices;
	}
}
