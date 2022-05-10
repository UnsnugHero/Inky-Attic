using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ManageLookingCamera : MonoBehaviour
{
	public enum SelectedCamera
	{
		High,
		Normal,
		Low
	}
	private float _elapsedTime = 0f;
	private float _totalTransitionTime = 1f;
	private int _selectedCamera = 0;
	private int _lastCamera = 0;
	private int _totalCameras;
	private CinemachineMixingCamera _mixer;

	// Start is called before the first frame update
	void Start()
	{
		_mixer = GetComponent<CinemachineMixingCamera>();
		_totalCameras = _mixer.ChildCameras.Length;
	}

	// Update is called once per frame
	void Update()
	{
		if (_elapsedTime < _totalTransitionTime)
		{
			for (int i = 0; i < _totalCameras; i++)
			{
				float desiredWeight = (i == _selectedCamera) ? 1f : 0f;
				float fromWeight = _mixer.GetWeight(i);
				float currWeight = Mathf.Lerp(fromWeight, desiredWeight, _elapsedTime);
				_mixer.SetWeight(i, currWeight);
			}

			_elapsedTime += Time.deltaTime;
		}

		if (_lastCamera != _selectedCamera)
		{
			_elapsedTime = 0;
			_lastCamera = _selectedCamera;
		}
	}

	public void ChangeLookCamera(SelectedCamera camera)
	{
		_selectedCamera = (int)camera;
	}
}
