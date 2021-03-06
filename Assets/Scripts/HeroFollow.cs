﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollow : MonoBehaviour
{
	public Transform lowerBound;
	public Transform upperBound;

	private HeroRabit player;

	void Start ()
	{
		player = GameObject.FindWithTag("Player").GetComponent<HeroRabit>();

		if (player == null)	Debug.LogError("No gameObject with the HeroRabit component found in the scene! [HERO_FOLLOW.CS]");
		if (lowerBound == null) Debug.LogError("No lowerBound gameObject found attached to this gameObject! [HERO_FOLLOW.CS]");
		if (upperBound == null) Debug.LogError("No upperBound gameObject found attached to this gameObject! [HERO_FOLLOW.CS]");
	}

	void LateUpdate ()
	{
		Vector3 newCameraPosition = new Vector3(player.transform.position.x, player.transform.position.y, -10);

		if (newCameraPosition.y > upperBound.position.y)
			newCameraPosition.y = transform.position.y;
		else if (newCameraPosition.y < lowerBound.position.y)
			newCameraPosition.y = transform.position.y;

		transform.position = newCameraPosition;
	}
}
