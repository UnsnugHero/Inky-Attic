using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepPhysicalBehavior : MonoBehaviour
{
	void Start()
	{
		GameObject.Find("Creep Intro State").TryGetComponent<CreepIntroState>(out var introState);

		if (introState.WasCreepIntroduced)
		{
			Destroy(gameObject);
		}
	}
}
