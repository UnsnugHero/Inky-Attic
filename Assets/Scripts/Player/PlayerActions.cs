using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static ManageLookingCamera;

public class PlayerActions : MonoBehaviour
{
	private enum LookDirection
	{
		Up,
		Down,
		None
	}
	private float _currLookPressTime = 0f;
	private PlayerFlashlightState _playerFlashlightState;
	private PlayerMovement _playerMovement;
	private LookDirection _currLookDirection = LookDirection.None;

	[SerializeField] private float _lookMoveCameraTimeThreshold = 2f;
	[SerializeField] private ManageLookingCamera _cameraManager;

	private void Start()
	{
		_playerFlashlightState = GetComponentInChildren<PlayerFlashlightState>();
		_playerMovement = GetComponent<PlayerMovement>();
	}

	private void Update()
	{
		Look();
		ManageActiveKeyboardEvents();
	}

	private void OnFlashLight(InputValue value)
	{
		if (value.isPressed)
		{
			_playerFlashlightState.SetIsOn(!_playerFlashlightState.GetIsOn());
			AudioManager.instance.PlaySound("Turn On Light");
		}
	}

	private void ManageActiveKeyboardEvents()
	{
		Keyboard keyboard = Keyboard.current;
		if (keyboard == null) return;

		ManageKeyPressForLooking(keyboard);
	}

	private void Look()
	{
		// Move Camera up or down depending on current status
		// only if the press key threshold has been crossed
		if (_currLookPressTime > _lookMoveCameraTimeThreshold)
		{
			SelectedCamera lookCamera = _currLookDirection == LookDirection.Up ? SelectedCamera.High : SelectedCamera.Low;
			_cameraManager.ChangeLookCamera(lookCamera);
		}
		else
		{
			_cameraManager.ChangeLookCamera(SelectedCamera.Normal);
		}
	}

	private void ManageKeyPressForLooking(Keyboard keyboard)
	{
		List<KeyControl> upKeys = new List<KeyControl>() { keyboard.wKey, keyboard.upArrowKey };
		List<KeyControl> downKeys = new List<KeyControl>() { keyboard.sKey, keyboard.downArrowKey };
		List<KeyControl> leftKeys = new List<KeyControl>() { keyboard.aKey, keyboard.leftArrowKey };
		List<KeyControl> rightKeys = new List<KeyControl>() { keyboard.dKey, keyboard.rightArrowKey };

		if (upKeys.Exists(key => key.isPressed) || downKeys.Exists(key => key.isPressed))
		{
			_currLookPressTime += Time.deltaTime;
		}

		if (upKeys.Exists(key => key.wasPressedThisFrame) || downKeys.Exists(key => key.wasPressedThisFrame))
		{
			bool upPressed = upKeys.Exists(key => key.wasPressedThisFrame);
			_currLookDirection = upPressed ? LookDirection.Up : LookDirection.Down;
			_currLookPressTime = 0f;
		}

		bool upOrDownNotPressed = upKeys.TrueForAll(key => !key.isPressed) && downKeys.TrueForAll(key => !key.isPressed);
		bool walkInputPressed = leftKeys.Exists(key => key.isPressed) || rightKeys.Exists(key => key.isPressed);
		if (upOrDownNotPressed || walkInputPressed || _playerMovement.IsActivelyClimbing || keyboard.spaceKey.isPressed)
		{
			_currLookDirection = LookDirection.None;
			_currLookPressTime = 0f;
		}
	}
}
