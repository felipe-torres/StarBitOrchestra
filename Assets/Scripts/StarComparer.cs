using System;
using System.Collections.Generic;

/// <summary>
/// Custom comparer for stars
/// </summary>
public class StarComparer : IComparer<Star>
{
	public Star ReferenceStar { get; set; }

	public StarComparer(Star rs)
	{
		ReferenceStar = rs;
	}

	/// <summary>
	/// Compares distances to this star between two other stars
	/// </summary>
	public int Compare(Star a, Star b)
	{
		float distanceS1 = (a.transform.position - ReferenceStar.transform.position).sqrMagnitude;
		float distanceS2 = (b.transform.position - ReferenceStar.transform.position).sqrMagnitude;
		if (distanceS1 == distanceS2)
			return 0;
		else if (distanceS1 > distanceS2)
			return 1;
		else
			return -1;
	}
}
