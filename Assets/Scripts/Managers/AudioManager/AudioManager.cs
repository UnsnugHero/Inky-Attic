using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	[SerializeField] private List<Sound> _sounds;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		foreach (Sound s in _sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	public void PlaySound(string name)
	{
		Sound s = _sounds.Find(sound => sound.name == name);

		if (s == null || s.source == null)
		{
			Debug.LogWarning($"Sound with name {name} not found");
			return;
		}

		if (!s.playIfAlreadyPlaying && s.source.isPlaying)
		{
			return;
		}

		s.source.Play();
	}

	public void StopSound(string name)
	{
		Sound s = _sounds.Find(sound => sound.name == name);

		if (s == null)
		{
			Debug.LogWarning($"Sound with name {name} not found");
			return;
		}

		s.source.Stop();
	}

	public void PauseAudio()
	{
		foreach (Sound s in _sounds)
		{
			if (s.source.isPlaying && s.shouldStopOnPause)
			{
				s.source.Pause();
				s.isPaused = true;
			}
		}
	}

	public void ResumeAudio()
	{
		foreach (Sound s in _sounds)
		{
			if (s.isPaused)
			{
				s.source.Play();
				s.isPaused = false;
			}
		}
	}

	public void StopAllAudio()
	{
		foreach (Sound s in _sounds)
		{
			s.source.Stop();
		}
	}

	public bool IsSoundPlaying(string name)
	{
		Sound s = _sounds.Find(sound => sound.name == name);

		if (s == null)
		{
			Debug.LogWarning($"Sound with name {name} not found");
			return false;
		}

		return s.source.isPlaying;
	}

	public void SetVolume(string name, float volume)
	{
		Sound s = _sounds.Find(sound => sound.name == name);

		if (s == null)
		{
			Debug.LogWarning($"Sound with name {name} not found");
			return;
		}

		if (volume < 0f || volume > 1f)
		{
			Debug.LogWarning("Volumne out of valid range");
			return;
		}

		s.source.volume = volume;
	}

	public IEnumerator FadeOutSound(string name, float fadeTime)
	{
		Sound s = _sounds.Find(sound => sound.name == name);

		if (s == null)
		{
			Debug.LogWarning($"Sound with name {name} not found");
			yield break;
		}

		if (!s.source.isPlaying)
		{
			Debug.LogWarning("Sound not currently playing");
			yield break;
		}

		float startVolume = s.source.volume;

		while (s.source.volume > 0)
		{
			s.source.volume -= startVolume * Time.deltaTime / fadeTime;

			yield return null;
		}

		s.source.Stop();
		s.source.volume = startVolume;
	}

	public Sound GetSound(string name)
	{
		Sound s = _sounds.Find(sound => sound.name == name);

		if (s == null)
		{
			Debug.LogWarning($"Sound with name {name} not found");
			return null;
		}

		return s;
	}
}
