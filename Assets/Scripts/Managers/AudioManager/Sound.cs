using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
	public string name;
	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume = 0.5f;
	[Range(0f, 3f)]
	public float pitch = 1f;

	public bool loop;
	public bool shouldStopOnPause = true;
	public bool playIfAlreadyPlaying = true;

	[HideInInspector]
	public AudioSource source;
	[HideInInspector]
	public bool isPaused;
}
