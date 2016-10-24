using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using DG.Tweening;

namespace VRStandardAssets.Utils
{
public class AnimatedSelectionRadial : SelectionRadial
{
	private CanvasGroup canvasGroup;

	protected override void Awake()
	{
		Instance = this;
		canvasGroup = m_Selection.GetComponent<CanvasGroup>();
	}

	public override void Show()
	{
		//m_Selection.gameObject.SetActive(true);
		m_IsSelectionRadialActive = true;

		//print("OverReticle");
		DOTween.Kill("ReticleShowHide");
		canvasGroup.DOFade(1f, 0.5f).SetId("ReticleShowHide").SetUpdate(UpdateType.Normal, true);
		m_Selection.GetComponent<RectTransform>().DOSizeDelta(Vector2.one*2, 0.5f).SetId("ReticleShowHide").SetUpdate(UpdateType.Normal, true);
	}


	public override void Hide()
	{
		//m_Selection.gameObject.SetActive(false);
		m_IsSelectionRadialActive = false;
		
		//print("OutReticle");
		DOTween.Kill("ReticleShowHide");
		canvasGroup.DOFade(0, 0.5f).SetId("ReticleShowHide").SetUpdate(UpdateType.Normal, true);
		m_Selection.GetComponent<RectTransform>().DOSizeDelta(Vector2.one*-0.3f, 0.5f).SetId("ReticleShowHide").SetUpdate(UpdateType.Normal, true);

		// This effectively resets the radial for when it's shown again.
		m_Selection.fillAmount = 0f;
	}
}
}