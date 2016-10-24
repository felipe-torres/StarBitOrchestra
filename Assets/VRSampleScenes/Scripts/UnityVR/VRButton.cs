using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Defines a button that uses VRInteractiveItem events
/// </summary>
namespace VRStandardAssets.Utils
{
[RequireComponent(typeof(VRInteractiveItem))]
public class VRButton : Button
{
	protected VRInteractiveItem m_InteractiveItem;       // The interactive item for where the user should click to load the level.
	protected bool m_GazeOver;

	protected void Awake()
	{
		m_InteractiveItem = GetComponent<VRInteractiveItem>();
	}

	private void OnEnable ()
	{
		m_InteractiveItem.OnOver += HandleOver;
		m_InteractiveItem.OnOut += HandleOut;
		m_InteractiveItem.OnClick += HandleClick;
		//m_SelectionRadial.OnSelectionComplete += HandleSelectionComplete;
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
		DoStateTransition(SelectionState.Highlighted, false);
	}


	private void HandleOut()
	{
		m_GazeOver = false;
		DoStateTransition(SelectionState.Normal, false);
	}

	private void HandleClick()
	{
		Click();
	}


	private void HandleSelectionComplete()
	{
		// If the user is looking at the rendering of the scene when the radial's selection finishes, activate the button.
		if (m_GazeOver)
			Click();
	}


	private void Click()
	{
		DoStateTransition(SelectionState.Pressed, false);
		onClick.Invoke();
	}

}
}
