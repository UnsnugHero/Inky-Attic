using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CreepIntroduction : MonoBehaviour
{
	private readonly string _animTriggerSlosh = "slosh";
	private readonly string _animTriggerRise = "rise";
	private readonly string _animTriggerSmile = "smile";
	private readonly string _animTriggerLaugh = "laugh";
	private readonly string _animTriggerFade = "fade";

	private float _originalCameraZoom;
	private float _currentCameraZoom;
	private float _currentCameraTargetZoom;

	private Animator _creepAnimator;
	private PlayerInput _playerInput;
	private CreepIntroState _introState;

	[SerializeField] private float _zoomVelocity;
	[SerializeField] private float _zoomInTime;
	[SerializeField] private GameObject _creep;
	[SerializeField] private GameObject _player;
	[SerializeField] private CinemachineVirtualCamera _cinemachineCam;
	[SerializeField] private CinemachineBrain _cinemachineBrain;

	[Header("Move Camera To Creep")]
	[SerializeField] private float _waitSecondsMoveToCreep;
	[SerializeField] private float _waitSecondsAfterMoveToCreep;
	[SerializeField] private float _creepTargetZoom;

	[Header("Creep Slosh")]
	[SerializeField] private float _waitSecondsCreepSlosh;

	void Start()
	{
		_creepAnimator = _creep.GetComponent<Animator>();
		_originalCameraZoom = _cinemachineCam.m_Lens.OrthographicSize;
		_currentCameraZoom = _cinemachineCam.m_Lens.OrthographicSize;
		_currentCameraTargetZoom = _cinemachineCam.m_Lens.OrthographicSize;
		_playerInput = _player.GetComponent<PlayerInput>();
		_introState = GameObject.Find("Creep Intro State").GetComponent<CreepIntroState>();
	}

	private void FixedUpdate()
	{
		_currentCameraZoom = Mathf.SmoothDamp(_currentCameraZoom, _currentCameraTargetZoom, ref _zoomVelocity, _zoomInTime);
		_cinemachineCam.m_Lens.OrthographicSize = _currentCameraZoom;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (!_introState.WasCreepIntroduced && collision.tag == "Player")
		{
			_introState.WasCreepIntroduced = true;
			StartCoroutine(IntroduceCreep());
		}
	}

	private IEnumerator IntroduceCreep()
	{
		_playerInput.actions.Disable();

		yield return StartCoroutine(FocusCreep());
		yield return StartCoroutine(CreepSlosh());
		yield return StartCoroutine(CreepRise());
		yield return StartCoroutine(CreepSmile());
		yield return StartCoroutine(CreepLaugh());
		yield return StartCoroutine(CreepFade());
		yield return StartCoroutine(FocusPlayer());

		_playerInput.actions.Enable();
	}

	private IEnumerator FocusCreep()
	{
		yield return new WaitForSeconds(_waitSecondsMoveToCreep);
		_cinemachineCam.Follow = _creep.transform;
		_currentCameraTargetZoom = _creepTargetZoom;

		yield return new WaitForSeconds(_waitSecondsAfterMoveToCreep);
	}

	private IEnumerator CreepSlosh()
	{
		_creepAnimator.SetTrigger(_animTriggerSlosh);
		AudioManager.instance.PlaySound("Creep Slosh");
		yield return new WaitForSeconds(_creepAnimator.GetCurrentAnimatorStateInfo(0).length);

		yield return new WaitForSeconds(_waitSecondsCreepSlosh);
	}

	private IEnumerator CreepRise()
	{
		_creepAnimator.SetTrigger(_animTriggerRise);
		AudioManager.instance.PlaySound("Creep Rise");
		yield return new WaitForSeconds(2);
	}

	private IEnumerator CreepSmile()
	{
		_creepAnimator.SetTrigger(_animTriggerSmile);
		yield return new WaitForSeconds(2);
	}

	private IEnumerator CreepLaugh()
	{
		_creepAnimator.SetTrigger(_animTriggerLaugh);
		AudioManager.instance.PlaySound("Creep Laugh");
		yield return new WaitForSeconds(1.75f);
	}

	private IEnumerator CreepFade()
	{
		_creepAnimator.SetTrigger(_animTriggerFade);
		yield return new WaitForSeconds(2);
	}

	private IEnumerator FocusPlayer()
	{
		_cinemachineCam.Follow = _player.transform;
		_currentCameraTargetZoom = _originalCameraZoom;

		yield return null;
	}
}
