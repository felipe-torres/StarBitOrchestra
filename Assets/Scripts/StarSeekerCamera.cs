using UnityEngine;
using System.Collections;

/// <summary>
/// Uses a ray to search for star objects. Manages user input
/// </summary>
public class StarSeekerCamera : MonoBehaviour
{
	private Transform currentSelectedStar;
	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown(0) && currentSelectedStar != null)
		{
			currentSelectedStar.GetComponent<Star>().Activate();
		}
	}

	/// <summary>
	/// Physics loop
	/// </summary>
	private void FixedUpdate()
	{
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Star")))
		{
			if (currentSelectedStar != hitInfo.transform)
			{
				currentSelectedStar = hitInfo.transform;

			}
		}
		else
		{
			currentSelectedStar = null;
		}
	}
}
