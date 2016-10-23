using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// Rotates this transform according to given parameters
/// </summary>
public class RotateInPlace : MonoBehaviour
{
	public Vector3 TargetRotation;
	public float Time = 1f;

	// Use this for initialization
	void Start ()
	{
		transform.DOLocalRotate(TargetRotation, Time).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
	}

	// Update is called once per frame
	void Update ()
	{

	}
}
