using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
			{
				Debug.Log("Null GameManager?!");
			}

			return _instance;
		}
	}

	void Awake()
	{
		InitSingleton();

		InputSystem.DisableDevice(Mouse.current);
	}

	private void InitSingleton()
	{
		if (_instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
}
