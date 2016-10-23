using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// Base star class
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class Star : MonoBehaviour
{
	public AudioClip Sound;
	public Color Color;
	public GameObject ExpansiveSpherePrefab;
	private int linkedStars = 0;
	private Vector3 originalScale;
	public float ConstellationCreationRadius = 10f;

	private LineRenderer lr;

	public bool useMethod1 = false;

	void Awake()
	{
		lr = GetComponent<LineRenderer>();
	}

	// Use this for initialization
	void Start ()
	{
		originalScale = transform.localScale;
		transform.DOScale(originalScale * 0.5f, 1f).SetLoops(-1, LoopType.Yoyo).SetDelay(Random.Range(0.0f, 5.0f));
	}

	// Update is called once per frame
	void Update ()
	{
	}

	public void Activate()
	{		
		Sound = AudioManager.Instance.GetNote();
		CreateConstellation(ConstellationCreationRadius);
		GetComponent<AudioSource>().PlayOneShot(Sound);
		//GetComponent<Renderer>().material.DOColor(Color.red, 1f);
	}

	/// <summary>
	/// Projects a sphere of a given radius, to find nearby stars and links them through a line
	/// </summary>
	private void CreateConstellation(float radius)
	{
		StartCoroutine(ConstellationEffect(radius, transform.position));

		if (useMethod1)
		{
			Star ClosestStar = GetClosestStar(radius);
			if(ClosestStar != null) StartCoroutine(CreateConstellationSeq(ClosestStar));
			//if(ClosestStar != null && linkedStars == 0) StartCoroutine(CreateConstellationSeq(ClosestStar));
		}
		else
		{
			List<Star> closestStars = GetClosestStars(radius);
			StartCoroutine(CreateConstellationSeq(closestStars));
		}

	}

	private IEnumerator CreateConstellationSeq(Collider[] neighborStars)
	{
		linkedStars = 0;
		lr.SetVertexCount(linkedStars);
		Vector3 lastStar = neighborStars[neighborStars.Length - 1].transform.position;
		for (int i = neighborStars.Length - 2; i >= 0; i--)
		{
			StartCoroutine(CreateLinkAnimated(lastStar, neighborStars[i].transform.position));
			lastStar = neighborStars[i].transform.position;
			yield return new WaitForSeconds(2f);
		}
	}

	private IEnumerator CreateConstellationSeq(List<Star> stars)
	{
		linkedStars = 0;
		lr.SetVertexCount(linkedStars);
		Vector3 lastStar = stars[0].transform.position;
		for (int i = 1; i < stars.Count; i++)
		{
			StartCoroutine(CreateLinkAnimated(lastStar, stars[i].transform.position));
			lastStar = stars[i].transform.position;
			yield return new WaitForSeconds(2f);
		}
	}

	/// <summary>
	/// Creates a link between this and another star and calls constellation building method in second star
	/// </summary>
	private IEnumerator CreateConstellationSeq(Star s)
	{
		Vector3 a = transform.position;
		Vector3 b = s.transform.position;

		linkedStars = 2;
		lr.SetVertexCount(linkedStars);
		lr.SetPosition(0, transform.position);
		lr.SetPosition(1, s.transform.position);

		float distance = (s.transform.position - transform.position).sqrMagnitude * 0.01f;

		// Calculate vector direction
		Vector3 temp = a;
		DOTween.To(() => temp, x => temp = x, b, distance).SetEase(Ease.Linear);

		while (temp != b)
		{
			lr.SetPosition(linkedStars - 1, temp);
			yield return null;
		}

		s.PreviousStar = this;
		s.Activate();


		/// Other method.
		// Calculate the ith closest star based on how many stars are already connected to this star
		// i.e. if there are 2 linked stars, get the 2nd closest, if there are 3, get the third closest, and so on

	}

	private IEnumerator ConstellationEffect(float radius, Vector3 position)
	{
		GameObject s = Instantiate(ExpansiveSpherePrefab, position, Quaternion.identity) as GameObject;
		s.transform.DOScale(radius * 2, 1f);
		s.GetComponent<Renderer>().material.DOFade(0, 1f);
		yield return new WaitForSeconds(1f);
		Destroy(s);
	}


/// Method 2
	// Get list of stars in ascending order of distance?
	private List<Star> GetClosestStars(float radius)
	{
		List<Star> closestStars = new List<Star>();

		Collider[] neighborStars = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Star"));

		//SortedSet<float>
		// First, get all distances and map them to the original indexes
		for (int i = 0; i < neighborStars.Length; i++)
		{
			closestStars.Add(neighborStars[i].GetComponent<Star>());
		}

		StarComparer sc = new StarComparer(this);
		closestStars.Sort(sc);

		return closestStars;

	}

	/// <summary>
	/// Gets closest star to this one by getting all stars in a given radius by closeness. 
	/// </summary>
	private Star GetClosestStar(float radius)
	{
		List<Star> closestStars = GetClosestStars(radius);
		if(closestStars.Count <= 1) return null; // No stars found in given radius

		int i = 1;
		while(i < closestStars.Count && closestStars[i] == PreviousStar) // Closest star must not be equal to previous star
		{
			i++;
		}
		if(i < closestStars.Count)
			return closestStars[i];
		else
			return null;
	}

	/// <summary>
	/// Creates a line renderer between a and b
	/// </summary>
	private void CreateLink(Vector3 a, Vector3 b)
	{
		linkedStars += 2;
		lr.SetVertexCount(linkedStars);
		lr.SetPosition(linkedStars - 2, a);
		lr.SetPosition(linkedStars - 1, b);
	}

	/// <summary>
	/// Creates an animated link between a pos a and a pos b
	/// </summary>
	private IEnumerator CreateLinkAnimated(Vector3 a, Vector3 b)
	{
		print("started");
		linkedStars += 2;
		lr.SetVertexCount(linkedStars);
		lr.SetPosition(linkedStars - 2, a);

		// Calculate vector direction
		Vector3 temp = a;
		DOTween.To(() => temp, x => temp = x, b, 2f).SetEase(Ease.Linear);

		while (temp != b)
		{
			lr.SetPosition(linkedStars - 1, temp);
			yield return null;
		}

		StartCoroutine(ConstellationEffect(5f, temp));
		print("Finished");
	}

	public Star PreviousStar { get; set; }

}
