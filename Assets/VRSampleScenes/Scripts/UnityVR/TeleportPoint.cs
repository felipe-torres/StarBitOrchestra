using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.VR;

namespace VRStandardAssets.Utils
{
[RequireComponent (typeof (VRInteractiveItem))]
public class TeleportPoint : MonoBehaviour
{

	public float dimmingSpeed = 1;
	public float fullIntensity = 1;
	public float lowIntensity = 0.5f;

	public Transform destTransform;

	private float lastLookAtTime = 0;

	private VRInteractiveItem m_InteractiveItem;
	private VRCameraFade m_CameraFade;

	private bool m_GazeOver;

	public GameObject PosIndicator;
	public bool DestroyOnTeleport = false;

	public UnityEvent OnTeleport;

	private void Awake()
	{
		m_InteractiveItem = GetComponent<VRInteractiveItem>();
		m_CameraFade = Camera.main.GetComponent<VRCameraFade>();
	}

	private void Start()
	{
		PosIndicator.SetActive(false);
	}

	private void OnEnable ()
	{
		m_InteractiveItem.OnOver += HandleOver;
		m_InteractiveItem.OnOut += HandleOut;
		//m_SelectionRadial.OnSelectionComplete += HandleSelectionComplete;
		m_InteractiveItem.OnClick += HandleClick;
	}


	private void OnDisable ()
	{
		m_InteractiveItem.OnOver -= HandleOver;
		m_InteractiveItem.OnOut -= HandleOut;
		//m_SelectionRadial.OnSelectionComplete -= HandleSelectionComplete;
	}

	private void HandleOver()
	{
		m_GazeOver = true;
		PosIndicator.SetActive(true);
		VRPlayerController.Instance.GazeOnTeleportPoint = true;
	}

	private void HandleOut()
	{
		m_GazeOver = false;
		PosIndicator.SetActive(false);
		VRPlayerController.Instance.GazeOnTeleportPoint = false;
	}

	private void HandleClick()
	{
		StartCoroutine (Teleport());
	}


	private void HandleSelectionComplete()
	{
		// If the user is looking at the rendering of the scene when the radial's selection finishes, activate the button.
		if (m_GazeOver)
			StartCoroutine (Teleport());
	}


	private IEnumerator Teleport()
	{
		// If the camera is already fading, ignore.
		//if (m_CameraFade.IsFading)
		//	yield break;

		m_CameraFade.FadeOut(0.5f, false);
		yield return new WaitForSeconds(0.5f);
		VRPlayerController.Instance.transform.position = destTransform.position;
		VRPlayerController.Instance.transform.rotation = PosIndicator.transform.rotation;

		TeleportPoint tp = VRPlayerController.Instance.CurrTeleportPoint;
		if(tp != null) tp.gameObject.SetActive(true);
		if(!DestroyOnTeleport) VRPlayerController.Instance.CurrTeleportPoint = this;
		gameObject.SetActive(false);
		InputTracking.Recenter();
		m_CameraFade.FadeIn(0.5f, false);

		if(OnTeleport!=null) OnTeleport.Invoke();
	}
}
}
