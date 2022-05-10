using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingLampAnimation : MonoBehaviour
{
	private Animator _lampAnimator;

	[SerializeField] bool isSwinging = false;

	void Start()
	{
		_lampAnimator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		_lampAnimator.SetBool("isSwinging", isSwinging);
	}
}
