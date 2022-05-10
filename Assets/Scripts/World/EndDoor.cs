using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndDoor : MonoBehaviour
{
	private bool isOpen = false;
	private bool isInteractable = false;

	[SerializeField] List<GoalLamp> goalLamps = new List<GoalLamp>();

	public bool IsOpen
	{
		get { return isOpen; }
	}

	void Update()
	{
		isOpen = goalLamps.TrueForAll(goalLamp => goalLamp.IsLit);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		SetInteractable(collision, true);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		SetInteractable(collision, true);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		SetInteractable(collision, false);
	}

	private void SetInteractable(Collider2D collision, bool interactable)
	{
		if (collision.tag == "Player")
		{
			isInteractable = interactable;
		}
	}

	void OnOpen(InputValue value)
	{
		if (value.isPressed && isOpen && isInteractable)
		{
			AudioManager.instance.PlaySound("Door Success");
			int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
			SceneManager.LoadScene(nextSceneIndex);
		}
		else if (value.isPressed && isInteractable && !isOpen)
		{
			AudioManager.instance.PlaySound("Door Fail");
		}
	}
}
