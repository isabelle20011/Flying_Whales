using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{

	public EventSound3D eventSound3DPrefab;

	public AudioClip attackAudio;
	public AudioClip damageAudio;
	public AudioClip sheepAudio;
	public AudioClip jumpAudio;

	private UnityAction<Vector3> attackSoundEventListener;
	private UnityAction<Vector3> damageSoundEventListener;
	private UnityAction<Vector3> sheepSoundEventListener;
	private UnityAction<Vector3> jumpSoundEventListener;

	void Awake()
	{
		attackSoundEventListener = new UnityAction<Vector3>(attackSoundEventHandler);
		damageSoundEventListener = new UnityAction<Vector3>(damageSoundEventHandler);
		sheepSoundEventListener = new UnityAction<Vector3>(sheepSoundEventHandler);
		jumpSoundEventListener = new UnityAction<Vector3>(jumpSoundEventHandler);
	}


	// Use this for initialization
	void Start()
	{
	}


	void OnEnable()
	{
		EventManager.StartListening<attackSoundEvent, Vector3>(attackSoundEventListener);
		EventManager.StartListening<damageSoundEvent, Vector3>(damageSoundEventListener);
		EventManager.StartListening<sheepSoundEvent, Vector3>(sheepSoundEventListener);
		EventManager.StartListening<jumpSoundEvent, Vector3>(jumpSoundEventListener);

	}

	void OnDisable()
	{
		EventManager.StopListening<attackSoundEvent, Vector3>(attackSoundEventListener);
		EventManager.StartListening<damageSoundEvent, Vector3>(damageSoundEventListener);
		EventManager.StartListening<sheepSoundEvent, Vector3>(sheepSoundEventListener);
		EventManager.StartListening<jumpSoundEvent, Vector3>(jumpSoundEventListener);
	}

	void attackSoundEventHandler(Vector3 pos)
	{

		if (attackAudio)
		{

			EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);

			snd.audioSrc.clip = this.attackAudio;

			snd.audioSrc.minDistance = 5f;
			snd.audioSrc.maxDistance = 100f;

			snd.audioSrc.Play();
		}
	}

	void damageSoundEventHandler(Vector3 pos)
	{

		if (damageAudio)
		{

			EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);

			snd.audioSrc.clip = this.damageAudio;

			snd.audioSrc.minDistance = 5f;
			snd.audioSrc.maxDistance = 100f;

			snd.audioSrc.Play();
		}
	}

	void sheepSoundEventHandler(Vector3 pos)
	{

		if (sheepAudio)
		{

			EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);
			snd.audioSrc.clip = this.sheepAudio;

			snd.audioSrc.minDistance = 5f;
			snd.audioSrc.maxDistance = 50f;

			snd.audioSrc.Play();
		}
	}

	void jumpSoundEventHandler(Vector3 pos)
	{

		if (jumpAudio)
		{

			EventSound3D snd = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);

			snd.audioSrc.clip = this.jumpAudio;

			snd.audioSrc.minDistance = 5f;
			snd.audioSrc.maxDistance = 100f;

			snd.audioSrc.Play();
		}
	}
}
