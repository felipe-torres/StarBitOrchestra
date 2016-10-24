using UnityEngine;
using System.Collections;

/// <summary>
/// Holds the player root
/// </summary>
namespace VRStandardAssets.Utils
{
public class VRPlayerController : MonoBehaviour
{
	public static VRPlayerController Instance { get; set; }

	public TeleportPoint CurrTeleportPoint { get ; set; }
	public bool GazeOnTeleportPoint { get; set; }

	private void Awake()
	{
		Instance = this;
	}


}
}
