using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepTrigger : MonoBehaviour
{
	private GameObject _creep;

	private void Awake()
	{
		_creep = GameObject.Find("Creep");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			_creep.SetActive(true);
		}
	}
}
