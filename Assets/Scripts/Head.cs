using System;
using UnityEngine;

public class Head : MonoBehaviour
{
	public event Action OnFoodEaten = delegate { };
	public event Action OnSelfCollided = delegate { };
	public event Action OnWallCollided = delegate { };


	private void OnTriggerEnter(Collider trigger)
	{
		if (trigger.CompareTag("food"))
		{
			OnFoodEaten.Invoke();
		}

		else if (trigger.CompareTag("body"))
		{
			OnSelfCollided.Invoke();
		}

		else if (trigger.CompareTag("wall"))
		{
			OnWallCollided.Invoke();
		}
	}
}
