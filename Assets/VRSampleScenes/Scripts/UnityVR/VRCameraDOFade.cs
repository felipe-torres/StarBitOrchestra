using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace VRStandardAssets.Utils
{
public class VRCameraDOFade : VRCameraFade
{
	protected override IEnumerator BeginFade(Color startCol, Color endCol, float duration)
	{
		// Fading is now happening.  This ensures it won't be interupted by non-coroutine calls.
		m_IsFading = true;

		m_FadeImage.DOColor(endCol, duration).SetUpdate(UpdateType.Normal, true);

		yield return new WaitForRealSeconds(duration);

		// Fading is finished so allow other fading calls again.
		m_IsFading = false;

		// If anything is subscribed to OnFadeComplete call it.
		OnFadeComplete();
	}


}

public sealed class WaitForRealSeconds : CustomYieldInstruction
{
	private readonly float _endTime;

	public override bool keepWaiting
	{
		get { return _endTime > Time.realtimeSinceStartup; }
	}

	public WaitForRealSeconds(float seconds)
	{
		_endTime = Time.realtimeSinceStartup + seconds;
	}
}
}
