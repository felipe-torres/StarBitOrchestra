using UnityEngine;
using System.Collections;

/// <summary>
/// Controls sound loop
/// </summary>
public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance { get; set; }
	private AudioSource audioSource;
	public enum Key { Fm, Cm, Bbm }
	public Key CurrentKey = Key.Fm;
	public float KeyChangeTime = 4f;

	public AudioClip[] FmClips;
	public AudioClip[] CmClips;
	public AudioClip[] BbmClips;

	public AudioClip[] Fm_HClips;
	public AudioClip[] Cm_HClips;
	public AudioClip[] Bbm_HClips;


	void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(PlayLoopSequence());
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public AudioClip GetNote(StarSpawner.StarType noteType)
	{
		AudioClip[] CurrentKeyNotes;
		switch (CurrentKey)
		{
		case Key.Fm:
			if(noteType == StarSpawner.StarType.Melody)
				CurrentKeyNotes = FmClips;
			else if(noteType == StarSpawner.StarType.Bass)
				CurrentKeyNotes = FmClips;
			else if(noteType == StarSpawner.StarType.Harmony)
				CurrentKeyNotes = Fm_HClips;
			else
				CurrentKeyNotes = FmClips;
			break;
		case Key.Cm:
			if(noteType == StarSpawner.StarType.Melody)
				CurrentKeyNotes = CmClips;
			else if(noteType == StarSpawner.StarType.Bass)
				CurrentKeyNotes = CmClips;
			else if(noteType == StarSpawner.StarType.Harmony)
				CurrentKeyNotes = Cm_HClips;
			else
				CurrentKeyNotes = CmClips;
			break;
		case Key.Bbm:
			if(noteType == StarSpawner.StarType.Melody)
				CurrentKeyNotes = BbmClips;
			else if(noteType == StarSpawner.StarType.Bass)
				CurrentKeyNotes = BbmClips;
			else if(noteType == StarSpawner.StarType.Harmony)
				CurrentKeyNotes = Bbm_HClips;
			else
				CurrentKeyNotes = BbmClips;
			break;
		default:
			CurrentKeyNotes = FmClips;
			break;
		}

		int RandomIndex = Random.Range(0, CurrentKeyNotes.Length);

		return CurrentKeyNotes[RandomIndex];
	}

	private IEnumerator PlayLoopSequence()
	{
		while (true)
		{
			// for each sound in current connected stars, play their sound split by how far apart they are
			yield return new WaitForSeconds(KeyChangeTime);

			// Change key
			ChangeKey(Key.Cm);

			yield return new WaitForSeconds(KeyChangeTime);

			// Change key
			ChangeKey(Key.Bbm);

			yield return new WaitForSeconds(KeyChangeTime);

			// Change key
			ChangeKey(Key.Cm);

			yield return new WaitForSeconds(KeyChangeTime);

			// Change key
			ChangeKey(Key.Fm);
		}

	}

	public void ChangeKey(Key key)
	{
		CurrentKey = key;
	}
}
